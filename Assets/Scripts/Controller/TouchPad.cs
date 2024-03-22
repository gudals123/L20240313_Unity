using System.Xml.Serialization;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TouchPad: MonoBehaviour 
{
    //UI에서 사용하는 트랜스폼
    private RectTransform touchPad;

    //터치 입력 중에 방향 컨트롤러의 영역 안에 있는 입력을 구분하기 위한 고유 식별 코드(아이디)
    private int touchId = -1;

    //입력이 시작되는 좌표
    private Vector3 startPos = Vector3.zero;    //0

    //방향 컨트롤러가 원으로 움직이는 반지름
    private float dragRadius = 0.0f;

    //플레이어의 움직임을 관리하는 PlayerMovement와 연결해
    //방향키가 전달되면 캐릭터에게 신호를 보내는 역할
    public PlayerMovement player;

    //버튼 눌렸느ㅜㄴ지 체크하는 변수
    private bool buttonPressed = false;


    private void Start()
    {
        touchPad = GetComponent<RectTransform>();

        startPos = transform.position;

        dragRadius = 60.0f;
    }

    //버튼 눌렀을 때
    public void ButtonDown()
    {
        buttonPressed = true;
    }
    //버튼 뗏을 때
    public void ButtonUp()
    {
        buttonPressed = false;
        HandleInput(startPos);
    }



    private void FixedUpdate()
    {
        //일반적인 경우에는 터치패드로 작업합니다.
        HandleTouchInput();

    //#if는 조건부 컴파일을 구현하기 위한 전처리기
    //유니티 에디터/ 웹/ 인게임에서 마우스 클릭으로작업합니다.
#if UNITY_EDITOR || UNITY_STANSDALONE_OSX ||UNITY_STANSDALONE_WIN||UNITY_WEBGL
        HandleInput(Input.mousePosition);
#endif
    }

    void HandleTouchInput()
    {
        int i = 0;  //터치 아이디를 매기기 위한 변수

        //터치가 1번이라도 들어오면 실행하도록
        if(Input.touchCount > 0)
        {
            foreach(Touch touch in Input.touches)
            {
                i++;    //터치 번호 증가
                Vector3 touchPos = new Vector3(touch.position.x, touch.position.y);

                //터치 입력이 방금 시작되었다면 
                if(touch.phase == TouchPhase.Began)
                {
                    //그 터치가 현재의 방향키 번위 내에 존재하는 경우
                    if(touch.position.x <= (startPos.x + dragRadius))
                    {
                        //이 터치 값을 기준으로 컨트롤러를 조작합니다.
                        touchId = i;    
                    }
                }
                //터치 입력이 움직였거나, 가만히 있는 상황일 경우
                if(touch.phase == TouchPhase.Moved||touch.phase == TouchPhase.Stationary)
                {
                    //터치 아이디로 지정된 상태일 때
                    if(touchId == i)
                    {
                        HandleInput(touchPos);
                    }
                }

                //터치 입력이 끝난 경우
                if(touch.phase == TouchPhase.Ended)
                {
                    //아이디 ㅎ제
                    if(touchId == i)
                    {
                        touchId = -1;
                    }
                }
            }
        }
    }

    void HandleInput(Vector3 input)
    {
        //버튼이 눌려져있는 상황일 경우
        if (buttonPressed)
        {
            //방향 컨트롤러의 기준 좌표부터 입력 받은 좌표가 얼마나 떨어져있는지를 구합니다.
            Vector3 diffVector = (input - startPos);

            //sqrMagnitude는 두 점간의 거리의 제곱에 루트를 한 값
            //비슷한 개념 Vector3.distance (연산속도가 느린 편). 대신 건축물 같은 정교한 값 구할 때 사용
            //sqrMagnitude는 단순하게 두 점 사이의 거리를 구할 때 사용
            //정확한 거기를 체크하는 것이 아닌 값이 크고 작은지만 판단함
            if (diffVector.sqrMagnitude > dragRadius*dragRadius)
            {
                diffVector.Normalize();//방향 벡터의 거리를 1로 설정합니다.(정규화)
                //방향 벡터

                //방량 컨트롤ㄹ러는 최대치만큼 이동합니다
                touchPad.position = startPos + diffVector*dragRadius;
            }
            else
            {
                //입력 지점과 기준 좌표가 최대치보다 크지 않을 경우 현재 입력 위치로 방향키를 옮겨줍니다.
                touchPad.position = input;
            }
        }
        //버튼을 누르지 않을 경우
        else
        {
            //버튼을 제자리로 변경
            touchPad.position = startPos;
        }

        //방향키와 기준점의 차이를 계산합니다.
        Vector3 diff = touchPad.position - startPos;

        //거리만 나누어 방향을 계산합니다.
        Vector2 normDiff = new Vector3(diff.x / dragRadius, diff.y / dragRadius);

        //플레이어 연결 여부 체크
        if (player != null)
        {
            //플레이어에게 변경된 좌표를 전달합니다.
            player.OnStickChanged(normDiff);
        }
    }
}

