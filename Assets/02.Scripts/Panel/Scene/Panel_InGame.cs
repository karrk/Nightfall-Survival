using System.Collections;
using UnityEngine;

public class Panel_InGame : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.Event.RegisterEvent(eEventType.ActGameReady, TempGameStart);
    }


    /// <summary>
    /// TODO :: 임시로 만들어진 게임 진행 함수입니다. 
    /// </summary>
    public void TempGameStart()
    {
        StartCoroutine(ManagerInitProcedure());
    }

    /// <summary>
    /// 해당 코루틴 안에 게임 진행에대한 절차를 담으시면됩니다. 게임시작은 별도로 OnGameComplete가 호출받은 쪽에서
    /// 진행하면되는데, 임시로 이안에다가 전부 구현하셔도됩니다.  
    /// </summary>
    private IEnumerator ManagerInitProcedure()
    {
        // TODO:: 스테이지 필요한거 준비. (풀이라던가)
        StageManager.Instance.CreateStage(Global_Data._stageNum);
        yield return null;

        // TODO::캐릭터 배치
        yield return null;

        // TODO:: 기타 필요한거있으면 이런식으로 추가..
        yield return null;

        yield return null;

        // TODO :: 팝업창을 만들어서, 실제 시작으로 넘어가도되고, 그이후부터는 몬스터가 배치되고 시간이 진행되어야합니다.
        GameManager.Instance.Event.CallEvent(eEventType.OnGameComplete);
        // TODO:: 프로토타입이닌깐, 거기까지준비할 필요없이, 딜레이 이후 바로 시작하셔도됩니다. 자유롭게..
    }
}
