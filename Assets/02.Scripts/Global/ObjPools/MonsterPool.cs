using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : ObjectPool
{
    protected override int InitCount => 50;

    protected override ePoolingType Type => ePoolingType.Monster;

}
