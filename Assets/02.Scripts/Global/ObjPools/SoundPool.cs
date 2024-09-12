using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPool : ObjectPool
{
    protected override int InitCount => 20;

    protected override ePoolingType Type => ePoolingType.Sound;
}
