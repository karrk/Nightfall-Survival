using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Sound_Temp : PoolingObj
{
    [SerializeField]
    AudioClip _clip;

    public void Play()
    {

    }

    public void SetClip(AudioClip clip)
    {
        this._clip = clip;
    }
}
