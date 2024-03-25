using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    Vector2 CamDistance 
    { 
        get 
        {
            return new Vector2(
                Camera.main.transform.position.x - _currentCenterPos.x,
                Camera.main.transform.position.y - _currentCenterPos.y
                );
        } 
    }

    static float DetectErrorMargin = 0.25f;

    Vector2 _currentCenterPos
    {
        // global Position
        get { return this.transform.position + new Vector3(_tileSize / 2, -_tileSize / 2); }
    }

    public static float TileSize
    {
        set
        {
            if (_tileSize == int.MinValue)
                _tileSize = value;
        }
    }

    static float _tileSize = int.MinValue;


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
            Move();

    }

    void Move()
    {
        // 양수라면 x 축 이동이 필요
        float axisComparison = Mathf.Abs(CamDistance.x) - Mathf.Abs(CamDistance.y);
        float moveDistance = SettingManager.Instance.MapTextureSize * 0.01f;

        if( -DetectErrorMargin <= axisComparison && axisComparison <= DetectErrorMargin) // 대각 이동이 필요 <보정 필요>
        {
            //Debug.Log("대각");

            Vector3 dir = new Vector3(Mathf.Sign(CamDistance.x),
                                    Mathf.Sign(CamDistance.y));

            this.transform.position += dir * moveDistance;

        }

        else if (axisComparison > 0) // x축으로 이동반경이 더 크다면
        {
            //Debug.Log("x 축이동");
            //Debug.Log($"{gameObject.name} {axisComparison}");
            //Debug.Log($"{CamDistance.x} : {CamDistance.y}");

            if (CamDistance.x < 0) // 타일을 오른쪽에서 왼쪽으로 이동
            {
                this.transform.position += Vector3.left * moveDistance;
            }
            else  // 타일을 왼쪽에서 오른쪽으로 이동
            {
                this.transform.position += Vector3.right * moveDistance;
            }
        }

        else if(axisComparison < 0)
        {
            //Debug.Log("y 축이동");

            if(CamDistance.y > 0) // 위에서 아래로 내리기
            {
                this.transform.position += Vector3.up * moveDistance;
            }
            else // 아래서 위로 올리기
            {
                this.transform.position += Vector3.down * moveDistance;
            }
        }
        
    }
}
