using System.Collections.Generic;

/// <summary>
/// 다이얼로그의 종류를 구분하는 enum 변수
/// </summary>
public enum DialogType
{
    Alert,
    Confirm,
    Ranking
}

//상속 못받게 제한 거는 키워드 sealed
//런 타임 시 특정 함수 호출할 경우 보모, 자식 클래스를 전체 조사해
//최종적으로 사용할 함수를 찾게 됩니다.
//sealed 처리된 클래스는 이 과정을 생략할 수 있게 해줍니다.
public sealed class DialogManager
{
    List<DialogData> _dialogQueue;
    Dictionary<DialogType, DialogController> _dialogMap;
    DialogController _currentDialog;



    #region Singleton
    //자기 자신에 대한  static 변수를 생성
    private static DialogManager instance = new DialogManager();

    //프로퍼티를 통해 접근
    public static DialogManager Instance
    {
        get
        {
            return instance;
        }
    }

    private DialogManager()
    {
        _dialogQueue = new List<DialogData>();
        _dialogMap = new Dictionary<DialogType, DialogController>();
    }
    #endregion


    public void Regist(DialogType type, DialogController controller)
    {

        _dialogMap[type] = controller;
    }

    // 데이터를 최 상단에 저장하는 행위
    // 다일얼로그 리스트를 저장하는 다이럴로그 큐에 새로운 다이얼로그 데이터를 추가하는 행위
    public void Push(DialogData data) 
    {
        _dialogQueue.Add(data);

        if(_currentDialog == null)
        {
            ShowNext();
        }
    }

    // 데이터 최상단의 값을 삭제하는 행위 
    // 리스트에서 마지막으로 열린 다이얼로그를 닫는 기능
    public void Pop()
    {
        //다이얼로그가 존재할 때
        if (_currentDialog != null)
        {
            //익명 delegate
            //delegate(매개변수 목록){실행하고자 하는 코드};
            //함수 이름 없이 기능만 델리게이트에 할당하기 위한 수단.
            _currentDialog.Close(
                delegate
                {
                    _currentDialog = null;
                    if(_dialogQueue.Count > 0)
                    {
                        ShowNext();
                    }
                });
        }
    }

    public void ShowNext()
    {
        //다일러로그를 리스트에서 첫번째 값을 가져오겠습니다.
        var next = _dialogQueue[0];

        //가져온 값의 형태를 확인해 어떤 컨트롤러인지를 확인합니다.
        var controller = _dialogMap[next.Type].GetComponent<DialogController>();

        //조회한 다이얼로그 컨트롤러를 현재의 다이얼로그 컨트롤러로 지정합니다.
        _currentDialog = controller;

        //현재의 다이얼로그를 빌드하겠습니다.
        _currentDialog.Build(next);

        //다이얼로그를 화면에 보여주겠습니다.
        _currentDialog.Show(delegate { });

        //다이얼로그 리스트에서 꺼내온 데이터를 제거하겠습니다.
        _dialogQueue.RemoveAt(0);
    }
    
    //현재 팝업 창이 표시되고 있는지를 확인하는 변수
    //_current가 비어있는 경우는 없다고 판단하겠습니다.
    public bool InShowing()
    {
        return _currentDialog != null;
    }


}

