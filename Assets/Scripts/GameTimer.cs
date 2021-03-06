﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    float currentTime = 0f;
    public float startingTime;

    [SerializeField] Text countdownText;

    // Start is called before the first frame update

    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        int tempTime = (int)currentTime;
        //countdownText.text = temptime.ToString();
        countdownText.text = tempTime + "";


        if(currentTime <= 0)
        {
            GameObject.Find("LocalGameManager").GetComponent<LocalGameManager>().GameOver();
            this.enabled = false;
            //GameOver()
        }
    }
}
