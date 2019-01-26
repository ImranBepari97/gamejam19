using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castbar : MonoBehaviour
{
    private SpriteRenderer MySprite;
    private float MaxSize;
    float RemainingTime;
    private GameObject Background;
    private float MaxTime;
 
    // Start is called before the first frame update
    void Start()
    {
        MaxSize = transform.localScale.x;
        MySprite = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector2(0, transform.localScale.y);
        Background = transform.parent.GetChild(2).gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RemainingTime > 0)
        {
            RemainingTime -= Time.deltaTime;
            float timegone = (MaxTime - RemainingTime);
            float percentage = Mathf.Clamp((timegone / MaxTime), 0, 100);
            transform.localScale = new Vector2(MaxSize * percentage, transform.localScale.y);
        }
        else
        {
            Background.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    public void Activated(float time)
    {
        RemainingTime = time;
        MaxTime = time;
        Background.SetActive(true);
    }


}
