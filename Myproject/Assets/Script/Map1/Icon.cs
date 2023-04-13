using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    [SerializeField] private GameObject H_icon; //주민집 g키
    [SerializeField] private GameObject Map2_icon; //하수구 g키


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Map2"))
        {
            H_icon.SetActive(true);
        }
        if (collision.gameObject.CompareTag("Map3"))
        {
            Map2_icon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Map2"))
        {
            H_icon.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Map3"))
        {
            Map2_icon.SetActive(false);
        }
    }

}