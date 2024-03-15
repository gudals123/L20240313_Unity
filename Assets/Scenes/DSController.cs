using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


//�̺�Ʈ Ʈ���Ÿ� ���� ����
public class DSController : MonoBehaviour
{
    public Text ResultRext;

    //�迭 ���
    public void DSArray()
    {
        //�ڷ���[] �迭�� = new �ڷ���[�迭�� ����]
        int[] exp = new int[10];

        for (int i = 0; i < exp.Length; i++)
        {
            exp[i] = i * 100 + (i * 50);
            ResultRext.text += $"[DSArray] ���� ���� {i}���� �䱸 ����ġ = {exp[i]} �Դϴ�.\n";
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
            ResultRext.text += $"[DSList] ���� ���� {i}���� �䱸 ����ġ = {exp[i]} �Դϴ�.\n";
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
            Debug.Log("�ش� ���� �����մϴ�.");
        }


        //��ųʸ��� �ٽ�
        //1. Ű�� �̿��� ���� ���� ����
        //2. �ش� Ű�� �����ϴ°��� ���� ����
        //3. Ű, ���� ���� ������ ������ �� �ִ°�?(����Ʈ ��ȯ)
        //4. ��ųʸ��� Ű�� ��쿡�� �ߺ��� ������� �ʰ�, ���� �ߺ��� ����մϴ�.
        //���� Add ������ ��, ������ �ִ� Ű�� �ٽ� Add�ϴ� ��� �� Ű�� ���� ���� ����


        //��ųʸ��� Ű -> ����Ʈ�� �ٲٴ� ���
        var KeyList = new List<string>(items.Keys);

        //��ųʸ��� �� -> ����Ʈ�� �ٲٴ� ���
        var ValueList = new List<int>(items.Values);


        //����Ʈ -> ��ųʸ��� �ٲٴ� ��
        //1. Ű�� �� ����Ʈ�� ���� �� ����Ʈ�� �غ��մϴ�.
        var KtoD = new List<string>() { "a", "b", "c", "d", "e" };
        var VtoD = new List<int>() { 1, 2, 3, 4, 5 };


        //��ųʸ��� �����ϰ� Zip �Լ��� ���� �۾��� �����մϴ�.
        //Ű.Zip(��, (k,v) => new {k,v}) Ű�� �� �ϳ��ϳ��� {Ű,��}�� ���·� ���̰� �˴ϴ�.
        //ToDictionary�� ���� Ű�� ���� �����մϴ�. �׸��� ��ųʸ��� ���·� ��ȯ�մϴ�.
        var NewDictionary = KtoD.Zip(VtoD, (k, v) => new { k, v }).ToDictionary(a => a.k, a => a.v);
    }


    public void DsResultReset()
    {
        ResultRext.text = "";
    }
}
