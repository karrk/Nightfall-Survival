using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextPool : ObjectPool
{
    protected override int InitCount => 50;

    protected override ePoolingType Type => ePoolingType.DamageText;
}
