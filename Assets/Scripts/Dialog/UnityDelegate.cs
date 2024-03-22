using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 유니티에서사용할 수 있는 대리자 유형
// 1. Action : 유니티에서 void 형태의 대리자
// 2. Func : 유니티에서 반환 값이 있는 형태의 대리자
// 3. UnityEvent : 인스펙터에서 이벤트를 노출 시켜, 할당할 수 있게 해주는 도구
// 4. event
// 5. delegate




public class UnityDelegate : MonoBehaviour
{
    public UnityEvent onDead;

    public void Awake()
    {
        onDead.AddListener(Dead);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            onDead?.Invoke();
        }
    }





    Action testAction01;
    void Method01() { }
    void Method02() { }
    void Method03() { }


    Action<int> testAction02;   //액션의 <>안에 넣는 값은 delegate로 호출할 함수의 매개변수입니다.
    void Method04(int a) { }
    void Method05(int a) { }
    void Method06(int a) { }
    void Method07(int a) { }


    Func<bool> testFunc01;
    bool Method08() { return true; }
    bool Method09() { return false; }



    Func<int, int, int> testFunc02;
    //맨 마지막에 적어놓은 타임 int는 return타입
    //그 앞의 값들은 전부 매개변수 처리합니다.

    int Method10(int a, int b) { return a + b; }
    int Method11(int a, int b) { return a - b; }


    void ActionMethod(Action<bool> callvack)
    {

    }

    void Dead()
    {
        Debug.Log("나 죽음");
    }


    // Start is called before the first frame update
    void Start()
    {
        testAction01 += Method01;
        testAction01 += Method02;
        testAction01 += Method03;
        testAction01();


        testAction02 += Method04;
        testAction02 += Method05;
        testAction02 += Method06;
        testAction02 += Method07;

        testAction02(10);           //대리자 호출
        testAction02?.Invoke(10);   //대리자의 Invoke 기능 실행

        testFunc01 += Method08;
        testFunc01 += Method09;

        if (testFunc01.Invoke() == true)
        {
            Debug.Log("작업 성공");
        }

        if (testFunc01())
        {
            Debug.Log("작업 성공2");
        }



        testFunc02 += Method10;
        testFunc02 += Method11;

        Debug.Log( testFunc02?.Invoke(10, 5));
    }

   

}
