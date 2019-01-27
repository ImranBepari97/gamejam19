using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour
{
    GameObject[] Numbers;
    // Start is called before the first frame update
    void Start()
    {

        Numbers = new GameObject[transform.childCount];
        for (int i = 0; i < Numbers.Length; i++)
        {
            Numbers[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject GO in Numbers) GO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RequestNumber (int number, GameObject player)
    {
        print("Request");
        for (int i = 0; i < Numbers.Length; i++)
        {
            if (!Numbers[i].activeInHierarchy)
            {
                print("Grab");
                Numbers[i].SetActive(true);
                Numbers[i].transform.position = new Vector3(player.transform.position.x, Numbers[i].transform.position.y, player.transform.position.z);
                Numbers[i].GetComponent<Text>().text = "+" + number;
                if (player.GetComponent<PlayerMovement>().playerNum == 1) Numbers[i].GetComponent<Text>().color = Color.blue;
                if (player.GetComponent<PlayerMovement>().playerNum == 2) Numbers[i].GetComponent<Text>().color = Color.yellow;
                if (player.GetComponent<PlayerMovement>().playerNum == 3) Numbers[i].GetComponent<Text>().color = Color.green;
                if (player.GetComponent<PlayerMovement>().playerNum == 4) Numbers[i].GetComponent<Text>().color = Color.red;
                StartCoroutine(NumberFade(i));
                return;
            }
        }
    }

    IEnumerator NumberFade(int number)
    {
        Color newcolor = Numbers[number].GetComponent<Text>().color;
        newcolor.a = 1;
        Numbers[number].GetComponent<Text>().color = newcolor;
        //for (int i = 0; i < 20; i++)
        //{
        //    yield return new WaitForSeconds(0.00025f);
        //    newcolor.a += 0.05f;
        //    Numbers[number].GetComponent<Text>().color = newcolor;
        //}
        yield return new WaitForSeconds(1f);
        for (int f = 0; f < 20; f++)
        {
            //print(f);
            yield return new WaitForSeconds(0.025f);
            newcolor.a -= 0.05f;
           // print(newcolor.a);
            Numbers[number].GetComponent<Text>().color = newcolor;
        }
        Numbers[number].SetActive(false);

    }
}
