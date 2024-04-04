using UnityEngine;

public class TileMovement : MonoBehaviour
{
    readonly static float DetectErrorMargin = 0.25f;

    /// <summary>
    /// 대상 타일 오브젝트와 카메라(중앙)의 거리를 확인,
    /// 해당 거리를 가로, 세로 비율로 변환한 거리를 반환합니다.
    /// </summary>
    private Vector2 CamDistance
    {
        get
        {
            Vector2 tempPos = _currentCenterPos;

            return new Vector2(
                (Camera.main.transform.position.x - tempPos.x) * _yRatio,
                (Camera.main.transform.position.y - tempPos.y) * _xRatio
                );
        }
    }

    public static float _xRatio;
    public static float _yRatio;

    /// <summary>
    /// 타일의 피벗이 (0,1) 값으로 지정되어있기때문에
    /// 정확한 거리계산을 위해 내부 임시 센터피벗을 잡기위한 프로퍼티
    /// </summary>
    private Vector2 _currentCenterPos
    {
        get { return this.transform.position + new Vector3(_tileSize / 2, -_tileSize / 2); }
    }

    public static float _tileSize = 0;


    /// <summary>
    /// 카메라에 부착된 트리거범위를 벗어날경우 타일을 이동시킵니다.
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
            Move();

    }

    private static Vector3 _moveDistance = Vector3.zero;

    /// <summary>
    /// 타일들이 이동되어야할 값을 저장합니다.
    /// </summary>
    public static void SetMoveDistance(float x, float y)
    {
        _moveDistance.x = x;
        _moveDistance.y = y;

        _xRatio = _moveDistance.x * 0.01f;
        _yRatio = _moveDistance.y * 0.01f;
    }

    private void Move()
    {
        // x, y 축중 더 많이 이동된 거리를 확인
        float axisComparison = Mathf.Abs(CamDistance.x) - Mathf.Abs(CamDistance.y);

        // 확인된 값이 대각보정값 범위에 들어올경우 대각으로 이동
        if (-DetectErrorMargin <= axisComparison && axisComparison <= DetectErrorMargin)
        {
            Vector3 dir = new Vector3(Mathf.Sign(CamDistance.x),
                                    Mathf.Sign(CamDistance.y));

            this.transform.position +=
                new Vector3(dir.x * _moveDistance.x,
                            dir.y * _moveDistance.y);

        }

        // x축으로 이동된 거리가 더 크다면
        else if (axisComparison > 0)
        {
            if (CamDistance.x < 0)
            {
                this.transform.position += Vector3.left * _moveDistance.x;
            }
            else
            {
                this.transform.position += Vector3.right * _moveDistance.x;
            }
        }

        // y축으로 이동된 거리가 더 크다면
        else if (axisComparison < 0)
        {
            if (CamDistance.y > 0)
            {
                this.transform.position += Vector3.up * _moveDistance.y;
            }
            else
            {
                this.transform.position += Vector3.down * _moveDistance.y;
            }
        }

    }
}
