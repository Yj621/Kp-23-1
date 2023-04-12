using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MapSwitching : MonoBehaviour
{
public Image imageToFadeOut;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Map1"))
        {
            if (Input.GetKey(KeyCode.G))//GetKey사용하면 누를때 바로 이동됨
            {
                StartCoroutine(FadeOut());
                Invoke("LoadMap1", 0.97f);
            }
        }
        if (collision.gameObject.CompareTag("Map3"))
        {
            if (Input.GetKey(KeyCode.G))
            {
                StartCoroutine(FadeOut());
                Invoke("LoadMap4", 0.97f);
            }
        }
    }

    private void LoadMap1()
    {
        GameObject.Find("MissionController").GetComponent<MissonContorller>().missonNum++;
        SceneManager.LoadScene(2);
    }

    private void LoadMap4()
    {
        SceneManager.LoadScene(4);
    }


    private IEnumerator FadeOut()
    {
        float duration = 1f;
        float startTime = Time.time;
        Vector3 startScale = new Vector3(imageToFadeOut.rectTransform.localScale.x, imageToFadeOut.rectTransform.localScale.y, 1f);

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            float alpha = Mathf.Lerp(1f, 0f, t);
            Vector3 scale = Vector3.Lerp(startScale, Vector3.zero, t);


            imageToFadeOut.transform.localScale = scale;
            yield return null;
        }
        Destroy(imageToFadeOut);
        Destroy(gameObject);
    }
}
