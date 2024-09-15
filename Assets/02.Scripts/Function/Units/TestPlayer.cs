using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : Base_Unit
{
    public override BaseStat UnitStat => _stat;
    private BaseStat _stat = new BaseStat();

    protected override float ImmunityTime => 0.2f;
}
