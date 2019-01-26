using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castbar : MonoBehaviour
{
    private SpriteRenderer mySprite;
    private float maxSize;
    float remainingTime;
    private GameObject background;
    private float maxTime;
 
    // Start is called before the first frame update
    void Start()
    {
        maxSize = transform.localScale.x;
        mySprite = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector2(0, transform.localScale.y);
        background = transform.parent.GetChild(2).gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            float timegone = (maxTime - remainingTime);
            float percentage = Mathf.Clamp((timegone / maxTime), 0.01f, 100);
            transform.localScale = new Vector2(maxSize * percentage, transform.localScale.y);
        }
        else
        {
            background.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    public void Activated(float time)
    {
        time = Mathf.Clamp(time, 0.01f, 25);
        remainingTime = time;
        maxTime = time;
        print(time);
        background.SetActive(true);
    }


}
