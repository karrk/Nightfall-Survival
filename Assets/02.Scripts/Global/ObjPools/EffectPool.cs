using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : ObjectPool
{
    protected override int InitCount => 20;

    protected override ePoolingType Type => ePoolingType.Effect;
}
