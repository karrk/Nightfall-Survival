
/// <summary>
/// [종류] 이벤트 타입의 종류를 나타냅니다.
/// <br> 추후 이벤트 타입을 추가로 사용하실경우 해당 열거형을 추가하여 사용하시면 됩니다. </br>
/// <br> 기존 이벤트 시스템이 해당 eEventType 으로만 작동하겠금 폐쇄적으로 설계되어있기때문에  </br>
/// <br> 해당 타입 이외의 열거형으로는 작동하지 않습니다.</br>
/// </summary>
public enum eEventType
{
    Test,
    StartGame,
    EndGame,
    StageSetupCompleted,
    StageReady,
}
