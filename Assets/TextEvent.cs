using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

class ConnectionEvent
{
    public event EventHandler connection;

    public void Connection()
    {
        if(connection != null)
        {
            connection(this, EventArgs.Empty);
        }
    }

}


public class TextEvent : MonoBehaviour
{
    public Text text;
    public Text connectionCount;
    private int num = 90;

    ConnectionEvent connectionEvent;

    // Start is called before the first frame update
    void Start()
    {
        connectionEvent = new ConnectionEvent();
        connectionEvent.connection += new EventHandler(TextOutput);
        connectionCount.text = $"{num}번째 접속";
    }
        

    private void TextOutput(object sender, EventArgs e)
    {
        string eventComment = "축하합니다! 100번째 접속 유저입니다!\n이벤트에 당첨되셨습니다.";

        
        StartCoroutine(TypeTextEffect(eventComment));
        
    }
    IEnumerator TypeTextEffect(string str)
    {
        text.text = string.Empty;


        for (int i = 0; i < str.Length; i++)
        {
            text.text += str[i];
            //Thread.Sleep(2000);
            yield return new WaitForSeconds(0.1f);
        }
    }



    void Update()
    {
        if (num ==100)
        {
            connectionEvent.Connection();
            num++;
        }

    }


    public void CountUp()
    {
        num++;
        connectionCount.text = $"{num}번째 접속";
    }
}
