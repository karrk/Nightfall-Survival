using UnityEngine;

public class Panel_LoadScene : MonoBehaviour
{
    private void Start()
    {
        //임시 
        CompleteLoad();
    }


    /// <summary>
    /// [기능] 로드가 어느정도 진행된 시점에서 인트로씬으로 전환합니다.
    /// </summary>
    private void CompleteLoad()
    {
        GameManager.Instance.TryChangeScene(eSceneKind.Intro);
    }
}
