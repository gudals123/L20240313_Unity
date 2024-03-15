using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


//이벤트 트리거를 통해 전달
public class DSController : MonoBehaviour
{
    public Text ResultRext;

    //배열 사용
    public void DSArray()
    {
        //자료형[] 배열명 = new 자료형[배열의 길이]
        int[] exp = new int[10];

        for (int i = 0; i < exp.Length; i++)
        {
            exp[i] = i * 100 + (i * 50);
            ResultRext.text += $"[DSArray] 다음 레벨 {i}까지 요구 경험치 = {exp[i]} 입니다.\n";
        }
    }

    public void DSList()
    {
        List<int> exp = new List<int>();

        for (int i = 0; i < 10; ++i)
        {
            exp.Add(i * 100 + (i * 50));
        }

        //exp.RemoveAll(x => (x % 4) == 0);
        exp.Sort((a, b) => b.CompareTo(a));

        for (int i = 0; i < exp.Count; ++i)
        {
            ResultRext.text += $"[DSList] 다음 레벨 {i}까지 요구 경험치 = {exp[i]} 입니다.\n";
        }
    }

    public void DSDictionary()
    {
        //Dictionary<K, V>
        Dictionary<string, int> items = new Dictionary<string, int>()
        {
            {"red apple", 10 },
            {"meet", 100 }
        };

        items.Add("cake", 50);

        if (items.ContainsKey("cake"))
        {
            items.Remove("cake");
        }

        if (items.ContainsValue(100))
        {
            Debug.Log("해당 값은 존재합니다.");
        }


        //딕셔너리의 핵심
        //1. 키를 이용한 값에 대한 접근
        //2. 해당 키가 존재하는가에 대한 여부
        //3. 키, 값을 각각 분할해 보관할 수 있는가?(리스트 변환)
        //4. 딕셔너리는 키의 경우에는 중복을 허용하지 않고, 값은 중복을 허용합니다.
        //따라서 Add 진행할 때, 기존에 있는 키를 다시 Add하는 경우 그 키가 가진 값만 변경


        //딕셔너리의 키 -> 리스트로 바꾸는 기능
        var KeyList = new List<string>(items.Keys);

        //딕셔너리의 값 -> 리스트로 바꾸는 기능
        var ValueList = new List<int>(items.Values);


        //리스트 -> 딕셔너리로 바꾸는 법
        //1. 키가 될 리스트와 값이 될 리스트를 준비합니다.
        var KtoD = new List<string>() { "a", "b", "c", "d", "e" };
        var VtoD = new List<int>() { 1, 2, 3, 4, 5 };


        //딕셔너리를 생성하고 Zip 함수를 통해 작업을 진행합니다.
        //키.Zip(값, (k,v) => new {k,v}) 키와 값 하나하나가 {키,값}의 형태로 묶이게 됩니다.
        //ToDictionary에 의해 키와 값을 설정합니다. 그리고 딕셔너리의 형태로 반환합니다.
        var NewDictionary = KtoD.Zip(VtoD, (k, v) => new { k, v }).ToDictionary(a => a.k, a => a.v);
    }


    public void DsResultReset()
    {
        ResultRext.text = "";
    }
}
