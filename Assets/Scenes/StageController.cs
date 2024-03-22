using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    //스테이지에서 쌓은 포인트를 저장할 변수
    public int StagePoint = 0;
    //포인트 표시용 텍스트
    public Text PointText;

    //스테이지 컨트롤러의 인스턴스를 저장하는 static 변수
     public static StageController instance;

    //다른 코드 내에서 StageController.intstansce.AddPoint(10)과 같은 형태로 사용할 수 있게 됩니다.
    //따로 연결해서 쓸 필요가 없어서 편리합니다.

    private void Start()
    {
        instance = this;
        //안내창 값 설정
        DialogDataAlert alert = new DialogDataAlert("게임 시작", "소환되는 슬라임들을 모두 처치하세요", delegate () { Debug.Log("OK를 눌렀습니다!"); });

        //매니저에 등록
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
        DialogDataConfirm confirm = new DialogDataConfirm("재시작", "재시작 하시겠습니까?",
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
