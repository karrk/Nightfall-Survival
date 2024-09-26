using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float _tilesInterval;

    private void Awake()
    {
        Global_Data.SetResolution();
        _tilesInterval = SetInterval();
        SetCollider();
    }

    private float SetInterval()
    {
        return Global_Data.GetTextureSize() / Global_Data.GetRxC_Count();
    }

    [SerializeField]
    private float _moveSpeed = 5f;

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
        int textureSize = Global_Data.GetTextureSize();

        float size = _tilesInterval;

        while (true)
        {
            if (size >= textureSize)
                break;

            size += _tilesInterval;
        }

        size *= 0.01f;
        this.GetComponent<BoxCollider2D>().size = new Vector2(size + 0.1f, size + 0.1f);
    }
}
