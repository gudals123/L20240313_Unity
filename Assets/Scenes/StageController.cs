using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    //������������ ���� ����Ʈ�� ������ ����
    public int StagePoint = 0;
    //����Ʈ ǥ�ÿ� �ؽ�Ʈ
    public Text PointText;

    //�������� ��Ʈ�ѷ��� �ν��Ͻ��� �����ϴ� static ����
     public static StageController instance;

    //�ٸ� �ڵ� ������ StageController.intstansce.AddPoint(10)�� ���� ���·� ����� �� �ְ� �˴ϴ�.
    //���� �����ؼ� �� �ʿ䰡 ��� ���մϴ�.

    private void Start()
    {
        instance = this;
        //�ȳ�â �� ����
        DialogDataAlert alert = new DialogDataAlert("���� ����", "��ȯ�Ǵ� �����ӵ��� ��� óġ�ϼ���", delegate () { Debug.Log("OK�� �������ϴ�!"); });

        //�Ŵ����� ���
        DialogManager.Instance.Push(alert);
    }

    public void AddPoint(int point)
    {
        StagePoint += point;
        PointText.text = StagePoint.ToString();
    }


    public void FinishGame()
    {
        //Application.LoadLevel(Application.loadedLevel);
        DialogDataConfirm confirm = new DialogDataConfirm("�����", "����� �Ͻðڽ��ϱ�?",
        delegate (bool yn) 
        {
            if (yn)
            {
                SceneManager.LoadScene("Game");
            }
            else
            {
                Application.Quit();
            }
            
        });

        DialogManager.Instance.Push(confirm);
      

    }
}
