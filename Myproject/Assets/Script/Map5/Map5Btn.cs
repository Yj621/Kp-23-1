using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map5Btn : MonoBehaviour
{
    private GameObject btn1;
    private GameObject btn2;
    //private GameObject TransFloor1;
    private GameObject TransFloor2;
    private GameObject sprite;
    private GameObject airTurnOn;
    // Start is called before the first frame update
    void Start()
    {
        btn1 = GameObject.Find("Btn1");
        btn2 = GameObject.Find("Btn2");
        //TransFloor1 = GameObject.Find("TransparencyFloor");
        TransFloor2 = GameObject.Find("TransparencyFloor2");
        sprite = GameObject.Find("Sprite");
        airTurnOn = GameObject.Find("AirTurnOn");


        if (TransFloor2 != null)
        {
            //TransFloor1.GetComponent<SpriteRenderer>().sprite = null;
            //TransFloor1.GetComponent<BoxCollider2D>().isTrigger = true;
            TransFloor2.GetComponent<SpriteRenderer>().sprite = null;
            TransFloor2.GetComponent<BoxCollider2D>().isTrigger = true;
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            if (gameObject == btn1)
            {
                //TransFloor1.GetComponent<SpriteRenderer>().sprite = sprite.GetComponent<SpriteRenderer>().sprite;
                //TransFloor1.GetComponent<BoxCollider2D>().isTrigger = false;
                TransFloor2.GetComponent<SpriteRenderer>().sprite = sprite.GetComponent<SpriteRenderer>().sprite;
                TransFloor2.GetComponent<BoxCollider2D>().isTrigger = false;
     
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject == btn2)
            {
                airTurnOn.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject == btn1)
            {
                //TransFloor1.GetComponent<SpriteRenderer>().sprite = null;
                //TransFloor1.GetComponent<BoxCollider2D>().isTrigger = true;
                TransFloor2.GetComponent<SpriteRenderer>().sprite = null;
                TransFloor2.GetComponent<BoxCollider2D>().isTrigger = true;
            
            }
        }
    }
}
