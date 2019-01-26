﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static bool Created;
    private Image fader;
    private int CurrentScene;
    private bool ChangingScenes;
    public SfxPlayer sfx;
    public MusicPlayer mp;
    private int MessScore;
    private int CleanScore;
    
    // Start is called before the first frame update
    void Awake()
    {
        sfx = GetComponentInChildren<SfxPlayer>();
        mp = GetComponentInChildren<MusicPlayer>();

        if (!Created) //ensures there is only ever one game manager in scene
        {
            DontDestroyOnLoad(this.gameObject);
            fader = transform.GetChild(0).GetChild(0).GetComponent<Image>();
            fader.gameObject.SetActive(false);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void AddMessScore(int score)
    {
        MessScore += score;
    }

    public void AddCleanScore(int score)
    {
        CleanScore += score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// load the next scene. Runs off scene int numbers
    /// </summary>
    public void LoadNextScene()
    {
        if (CurrentScene == 2) //should be set to max number of scenes in build settings
        {
            CurrentScene = -1; //loops back round to menu
        }
        StartCoroutine(LoadScene(CurrentScene + 1));
    }

    /// <summary>
    /// make fader disappear
    /// </summary>
    private void FadeOut()
    {
        Color newcolor = fader.color;
        newcolor.a -= 0.05f;
        fader.color = newcolor;
    }

    /// <summary>
    /// make fader appear
    /// </summary>
    private void FadeIn()
    {
        Color newcolor = fader.color;
        newcolor.a += 0.05f;
        fader.color = newcolor;
    }

    /// <summary>
    /// load the scene. Based off Unity build settings numbers
    /// Also controls the main fader.
    /// </summary>
    /// <param name="scene"></param>
    /// <returns></returns>
    IEnumerator LoadScene(int scene)
    {
        if (!ChangingScenes)
        {

            //fade out current scene
            fader.gameObject.SetActive(true);
            ChangingScenes = true;
            for (int i = 0; i < 20; i++)
            {
                FadeIn();
                yield return new WaitForSeconds(0.0025f);
            }
            //load next scene
            SceneManager.LoadScene(scene);
            yield return new WaitForSeconds(0);
            CurrentScene = scene;
            if (scene == 2)
            {
                StartCoroutine(EndScreen());
            }
            yield return new WaitForSeconds(0.1f);

            //fade in newly loaded scene
            for (int i = 0; i < 20; i++)
            {
                FadeOut();
                yield return new WaitForSeconds(0.0025f);
            }
            ChangingScenes = false;
            fader.gameObject.SetActive(false) ;

            //start game here (May want a 3 2 1 counter?
        }

    }


private IEnumerator EndScreen()
    {
        GameObject.Find("CleanerScore").GetComponent<Text>().text = "" + CleanScore;
        GameObject.Find("MessScore").GetComponent<Text>().text = "" + MessScore;
        if (CleanScore > MessScore)
        {
            GameObject.Find("WinnersText").GetComponent<Text>().text = "Cleaners Win";
        }else if (CleanScore == MessScore)
        {
            GameObject.Find("WinnersText").GetComponent<Text>().text = "Draw!";
            GameObject.Find("WinnersText").GetComponent<Text>().color = Color.white;
        }
        else
        {
            GameObject.Find("WinnersText").GetComponent<Text>().text = "Messers Win";
            GameObject.Find("WinnersText").GetComponent<Text>().color = Color.red;
        }
        yield return new WaitForSeconds(10);
        LoadNextScene();
    }
    
    
}
