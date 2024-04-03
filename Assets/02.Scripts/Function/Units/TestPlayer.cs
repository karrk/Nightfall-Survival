using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : Base_Unit
{
    public override Stat UnitStat => _stat;
    private Stat _stat = new Stat();

    protected override float ImmunityTime => 0.2f;
}
