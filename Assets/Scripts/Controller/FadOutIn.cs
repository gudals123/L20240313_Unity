using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FadOutIn : MonoBehaviour
{

   public Image image;

    public void StartFadeOut()
    {
        StartCoroutine("Fade");


    }
    public void Start()
    {
        StartFadeOut();
        //버튼 컴포넌트에 온 클릭 이벤트 리스너 연결
    }


    IEnumerator Fade()
    {
        
        float startAlpha = 1.0f;

        while (startAlpha > 0)
        {
            Debug.Log("Fade while 들어감");
            startAlpha -= 0.05f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, startAlpha);

        }

    }
}
