using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : BaseStat
{
    private float _recoverHp;
    private float _lucky;
    private float _throwCount;
    private float _throwSpeed;
    private float _attackRange;
    private float _attackDelay;
    private float _attackDuration;
    private float _alphaExp;
    private float _alphaGold;
    private float _delay;
    private float _duration;
    private float _avoidRate;

    public float RecoverHp => _recoverHp;
    public float Lucky => _lucky;
    public float ThrowCount => _throwCount;
    public float ThrowSpeed => _throwSpeed;
    public float AttackRange => _attackRange;
    public float AttackDelay => _attackDelay;
    public float AttackDuration => _attackDuration;
    public float AlphaExp => _alphaExp;
    public float AlphaGold => _alphaGold;
    public float Delay => _delay;
    public float Duration => _duration;
    public float AvoidRate => _avoidRate;

    #region 캐릭터 스탯설정

    public CharacterStat SetRecoverHp(float recoverHp)
    {
        this._recoverHp = recoverHp;
        return this;
    }
    public CharacterStat SetLucky(float lucky)
    {
        this._recoverHp = lucky;
        return this;
    }
    public CharacterStat SetThrowCount(float count)
    {
        this._throwCount = count;
        return this;
    }
    public CharacterStat SetThrowSpeed(float speed)
    {
        this._throwSpeed = speed;
        return this;
    }
    public CharacterStat SetAttackRange(float range)
    {
        this._throwSpeed = range;
        return this;
    }
    public CharacterStat SetAttackDelay(float delay)
    {
        this._throwSpeed = delay;
        return this;
    }
    public CharacterStat SetAttackDuration(float duration)
    {
        this._throwSpeed = duration;
        return this;
    }
    public CharacterStat SetBounsExp(float alphaPercent)
    {
        this._alphaExp = alphaPercent;
        return this;
    }
    public CharacterStat SetBounsGold(float alphaPercent)
    {
        this._alphaGold = alphaPercent;
        return this;
    }
    public CharacterStat SetDelay(float delay)
    {
        this._delay = delay;
        return this;
    }
    public CharacterStat SetDuration(float duration)
    {
        this._duration = duration;
        return this;
    }
    public CharacterStat SetAvoidRate(float rate)
    {
        this._avoidRate = rate;
        return this;
    }

    #endregion

    #region 추가 스탯부여

    public CharacterStat AddRecoverHp(float recoverHp)
    {
        this._recoverHp += recoverHp;
        return this;
    }
    public CharacterStat AddLucky(float lucky)
    {
        this._recoverHp += lucky;
        return this;
    }
    public CharacterStat AddThrowCount(float count)
    {
        this._throwCount += count;
        return this;
    }
    public CharacterStat AddThrowSpeed(float speed)
    {
        this._throwSpeed += speed;
        return this;
    }
    public CharacterStat AddAttackRange(float range)
    {
        this._throwSpeed += range;
        return this;
    }
    public CharacterStat AddAttackDelay(float delay)
    {
        this._attackDelay += delay;
        return this;
    }
    public CharacterStat AddAttackDuration(float duration)
    {
        this._duration += duration;
        return this;
    }
    public CharacterStat AddBounsExp(float alphaPercent)
    {
        this._alphaExp += alphaPercent;
        return this;
    }
    public CharacterStat AddBounsGold(float alphaPercent)
    {
        this._alphaGold += alphaPercent;
        return this;
    }
    public CharacterStat AddAvoidRate(float rate)
    {
        this._avoidRate += rate;
        return this;
    }

    #endregion

    public BaseStat SetStats
        (CharacterStat stats, Data_Character characterData)
    {
        stats = (CharacterStat)base.SetStats(stats, characterData);

        stats
            .SetRecoverHp(characterData.recoverHp)
            .SetLucky(characterData.lucky)
            .SetThrowCount(characterData.throwCount)
            .SetThrowSpeed(characterData.throwSpeed)
            .SetAttackRange(characterData.attackRange)
            .SetAttackDelay(characterData.attackDelay)
            .SetAttackDuration(characterData.attackDuration)
            .SetBounsExp(characterData.bonusExp)
            .SetBounsGold(characterData.bonusGold)
            .SetAvoidRate(characterData.avoidRate);

        return stats;
    }
}
