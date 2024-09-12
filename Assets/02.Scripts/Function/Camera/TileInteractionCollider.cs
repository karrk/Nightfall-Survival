using UnityEngine;

public class TileInteractionCollider : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.Event.RegisterEvent(eEventType.StageSetupCompleted, SetCollider);
    }

    //[SerializeField]
    //private float _testMoveSpeed = 5f;

    private void Update()
    {   // 임시용 코드
        //if (Input.GetKey(KeyCode.UpArrow))
        //    Camera.main.transform.position += Vector3.up * _moveSpeed * Time.deltaTime;
        //if (Input.GetKey(KeyCode.DownArrow))
        //    Camera.main.transform.position += Vector3.down * _moveSpeed * Time.deltaTime;
        //if (Input.GetKey(KeyCode.LeftArrow))
        //    Camera.main.transform.position += Vector3.left * _moveSpeed * Time.deltaTime;
        //if (Input.GetKey(KeyCode.RightArrow))
        //    Camera.main.transform.position += Vector3.right * _moveSpeed * Time.deltaTime;
    }

    private void SetCollider()
    {
        Vector3 mapSize = Global_Data._MapSize;
        float padding = 0f; // 임시용

        GetComponent<BoxCollider2D>().size =
            new Vector2(mapSize.x + padding, mapSize.y + padding);
    }
}
