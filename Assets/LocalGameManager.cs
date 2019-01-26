using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalGameManager : MonoBehaviour
{
    private GameManager GameManager;
    private SfxPlayer Sfx;
    public Text CountDownText;
    private PlayerMovement[] Players;
    // Start is called before the first frame update
    void Start()
    {
        Players = FindObjectsOfType<PlayerMovement>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Sfx = GameObject.Find("SfxPlayer").GetComponent<SfxPlayer>();
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CountDown()
    {
        foreach (PlayerMovement player in Players) player.enabled = false;
        FindObjectOfType<GameTimer>().enabled = false;
        CountDownText.text = "Get Ready!";
        yield return new WaitForSeconds(3);
        for (int i = 3; i > 0; i--)
        {
            CountDownText.text = "" + i;
            yield return new WaitForSeconds(1);
        }
        CountDownText.text = "GO!";
        foreach (PlayerMovement player in Players) player.enabled = true;
        //FindObjectOfType<GameTimer>().enabled = true;
        yield return new WaitForSeconds(1);
        CountDownText.text = "";
        FindObjectOfType<GameTimer>().enabled = true;
    }
}
