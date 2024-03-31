using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private void Start()
    {
        SetCollider();
    }

    [SerializeField]
    float _moveSpeed = 5f;

    private void Update()
    {   // 임시용 코드
        if (Input.GetKey(KeyCode.UpArrow))
            this.transform.position += Vector3.up * _moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.DownArrow))
            this.transform.position += Vector3.down * _moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
            this.transform.position += Vector3.left * _moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow))
            this.transform.position += Vector3.right * _moveSpeed * Time.deltaTime;
    }

    private void SetCollider()
    {
        SettingManager set_M = SettingManager.Instance;

        int tileInterval = set_M.MapTextureSize / set_M.RxC_Count;
        float size = tileInterval;

        while (true)
        {
            if (size >= set_M.MapTextureSize)
                break;

            size += tileInterval;
        }

        size *= 0.01f;
        this.GetComponent<BoxCollider2D>().size = new Vector2(size + 0.1f, size + 0.1f);
    }
}
