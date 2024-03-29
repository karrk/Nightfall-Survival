using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour , IStageParts
{
    Vector3 resolution;

    float _max_CircleX;
    float _max_CircleY;

    float circleRate;
    float padding = 0f;

    GameObject temp;
    public Sprite sp;

    public GameObject center;
    public GameObject ellipse_EdgePoint;

    private void Awake()
    {
        //GameManager.Instance.Event.RegisterEvent(eEventType.StageReady, CheckResolution);
    }

    private void Start()
    {
        temp = new GameObject();
        temp.AddComponent<SpriteRenderer>().sprite = sp;
        temp.transform.localScale = new Vector3(0.5f, 0.5f);

        CheckResolution();
    }

    public void AddPartsList()
    {
        StageManager.Instance._stageBuilder.AddPart(this);
    }

    public void SendPart()
    {
        StageManager.Instance._stageBuilder.SetSpawner(this);
    }

    void CheckResolution()
    {
        this.resolution = SettingManager.Instance.ScreenSize;
        circleRate = Camera.main.orthographicSize * 0.1f + 0.03f + padding;
        this._max_CircleX = resolution.x * 0.01f * 0.5f * circleRate;
        this._max_CircleY = resolution.y * 0.01f * 0.5f * circleRate;
    }

    public Vector3 SpawnCenter()
    {
        return this.transform.position;
    }

    void SetPointRotate()
    {
        float rand = Random.Range(0, 360);
        center.transform.rotation = Quaternion.Euler(new Vector3(0,0, rand));

        Transform edgePointTr = ellipse_EdgePoint.transform;
        float y = Mathf.Clamp(edgePointTr.position.y, -1 * _max_CircleY, _max_CircleY);
        int sign = Random.Range(0, 2) * 2 - 1;

        edgePointTr.position = new Vector3(sign*GetXvalue_CircleEdge(y,_max_CircleX,_max_CircleY), y);
    }

    float GetXvalue_CircleEdge(float y,float maxX, float maxY) // y값을 주면 타원형에 맞는 x값을 배출
    {
        return Mathf.Sqrt((1 - Mathf.Pow(y, 2)
            / Mathf.Pow(maxY, 2)) * Mathf.Pow(maxX, 2));
    }

    void RoundRotate(GameObject origin, float interval)
    {
        StartCoroutine(RotateCreate(origin, interval));
    }

    IEnumerator RotateCreate(GameObject obj, float intervalRot)
    {
        float minSize = Mathf.Min(_max_CircleX, _max_CircleY);
        
        Transform edgePointTr = ellipse_EdgePoint.transform;
        edgePointTr.position = Vector3.down * minSize;

        float prevRot = 0;

        while (true)
        {
            if (prevRot > 360)
                break;

            center.transform.rotation = Quaternion.Euler(Vector3.forward * (prevRot + intervalRot));

            edgePointTr.position = new Vector3(GetXvalue_CircleEdge(edgePointTr.position.y, minSize, minSize),
                Mathf.Clamp(edgePointTr.position.y, -1 * _max_CircleY, _max_CircleY));

            prevRot += intervalRot;

            //Instantiate(obj).transform.position = edgePointTr.position;

            yield return null;
        }
    }



    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            SetPointRotate();
        }

        if (Input.GetMouseButtonDown(1))
            RoundRotate(temp, 0.2f);
    }

    //public Vector3 SpawnCircleEdge()
    //{
    //    float randY = Random.Range(-1 * _max_CircleY, _max_CircleY);
    //    int sign = Random.Range(0, 2) * 2 - 1;

    //    //return new Vector3(randX, sign * GetYvalue_CircleEdge(randX));
    //    return new Vector3(sign * GetXvalue_CircleEdge(randY), randY);
    //}

    /// <summary>
    /// 랜덤위치생성
    /// </summary>
    /// <param name="obj"></param>
    //public Vector3 SpawnCircleEdge()
    //{
    //    float randY = Random.Range(-1 * _max_CircleY, _max_CircleY);
    //    int sign = Random.Range(0, 2) * 2 - 1;

    //    //return new Vector3(randX, sign * GetYvalue_CircleEdge(randX));
    //    return new Vector3(sign * GetXvalue_CircleEdge(randY), randY);
    //}

    /// <summary>
    /// 동일 몬스터 원형으로 배치
    /// </summary>
    /// <param name="interval">몹 사이 간격</param>
    //public void SpawnCircleEdge(GameObject obj, float interval)
    //{
    //    StartCoroutine(CircleRoutiune(obj,interval));
    //}

    //IEnumerator CircleRoutiune(GameObject obj, float interval)
    //{
    //    float min = Mathf.Min(_max_CircleX, _max_CircleY);

    //    float yPos = -1 * min;
    //    int sign = 1;

    //    GameObject clone = Instantiate(obj);
    //    clone.transform.position = new Vector3(0, yPos);

    //    while (true)
    //    {
    //        yPos += interval;

    //        if (yPos <= -1 * min)
    //            break;

    //        if (yPos >= min)
    //        {
    //            clone.transform.position = new Vector3(0, yPos);
    //            sign *= -1;
    //            interval *= -1;
    //            continue;
    //        }

    //        clone = Instantiate(obj);
    //        clone.transform.position = new Vector3(sign * GetXvalue_RoundCircleEdge(yPos,min),yPos);

    //        yield return null;
    //    }
    //}


    //float GetXvalue_RoundCircleEdge(float y,float r)
    //{
    //    return Mathf.Sqrt((1 - Mathf.Pow(y, 2)
    //        / Mathf.Pow(r, 2)) * Mathf.Pow(r, 2));
    //}


}
