using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update

    public void StartGame()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().LoadNextScene();
    }


public void MuteSfx()
{
    GameObject.Find("GameManager").GetComponent<GameManager>().sfx.ToggleSfx();
}

public void MuteMusic()
{
    GameObject.Find("GameManager").GetComponent<GameManager>().mp.ToggleMusic();
}

    public void Quit()
    {
        Application.Quit();
    }
}

