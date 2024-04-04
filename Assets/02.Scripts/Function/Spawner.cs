using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    float _camSize;
    readonly float _roundPadding = 0.5f;
    readonly float _rectPadding = 1f;

    Vector3 _screenScale;

    public GameObject _center;
    public GameObject _edgePoint;

    private void Awake()
    {
        Global_Data.SetSpawner(this);
    }

    private void Start()
    {
        _camSize = Camera.main.orthographicSize;
        _screenScale = GetScreenViewScale() / 2;
    }

    Vector3 GetScreenViewScale()
    {
        return new Vector3(_camSize * 2 * Camera.main.aspect, _camSize * 2);
    }

    /// <summary>
    /// 카메라 뷰 테두리에 오브젝트를 배치
    /// </summary>
    public void RandomSpawn(GameObject obj)
    {
        Vector3 screenScale = _screenScale;

        float randRot = Random.Range(0, 360);
        _center.transform.rotation = Quaternion.Euler(new Vector3(0, 0, randRot));

        Transform edgePoint = _edgePoint.transform;
        Transform centerPoint = _center.transform;

        Vector3 dir = Vector3.Normalize(edgePoint.position - centerPoint.position);
        float distance;

        if (Mathf.Abs(dir.x) * screenScale.y > Mathf.Abs(dir.y) * screenScale.x)
            distance = screenScale.x / Mathf.Abs(dir.x);
        else
            distance = screenScale.y / Mathf.Abs(dir.y);

        obj.transform.position = dir * (distance + _rectPadding);
    }

    /// <summary>
    /// 카메라 뷰 테두리의 모서리에 외접한 원형태로 몹생성
    /// </summary>
    /// <param name="limitDegree">회전 시작지점은 마지막으로 스폰된 지점의 방향과 외접한 원의 반지름에서 시작</param>
    public void CircleSpawn(Queue<GameObject> objs, float limitDegree)
    {
        float rot = limitDegree / objs.Count;

        StartCoroutine(RotateCreate(objs, rot, limitDegree));
    }

    IEnumerator RotateCreate(Queue<GameObject> objs, float intervalRot, float limitDegree = 360)
    {
        Vector3 size = _screenScale;

        float cross = Mathf.Sqrt(Mathf.Pow(size.x, 2) + Mathf.Pow(size.y, 2));
        float radius = cross + _roundPadding;

        Transform edgeTr = _edgePoint.transform;

        edgeTr.transform.position
            = Vector3.Normalize(edgeTr.position - _center.transform.position) * radius;

        float rot = _center.transform.rotation.z;
        Quaternion prevRot = Quaternion.Euler(0, 0, _center.transform.rotation.z);
        Vector3 prevPos = edgeTr.position;

        while (true)
        {
            if (objs.Count <= 0)
                break;

            _center.transform.rotation = prevRot;
            edgeTr.position = prevPos;

            _center.transform.rotation =
                Quaternion.Euler(0, 0, rot);

            prevRot = _center.transform.rotation;
            prevPos = edgeTr.position;

            rot += intervalRot;
            objs.Dequeue().transform.position = edgeTr.position;

            yield return null;
        }
    }

    public void SpawnCenter(GameObject obj)
    {
        obj.transform.position = this.transform.position;
    }
}