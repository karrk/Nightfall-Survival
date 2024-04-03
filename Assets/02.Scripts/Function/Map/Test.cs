using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = GetScreenViewScale()/2 + Offset;
    }

    Vector3 GetScreenViewScale()
    {
        return new Vector3(-1*Camera.main.orthographicSize * 2 * Camera.main.aspect, Camera.main.orthographicSize * 2);
    }

    Vector3 Offset = new Vector3(-3, 3);
}
