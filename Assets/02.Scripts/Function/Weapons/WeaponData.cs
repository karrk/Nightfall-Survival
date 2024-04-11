public class WeaponData
{
    private eCollectionType _collectType;

    private Character _user = null;
    private CharacterStat _userStat = null;

    private WeaponStat _weaponStat = null; // 인벤토리의 무기 정보를 가져온다.
    private Data_Weapon_Stats _statTable;
    private Data_Weapon_Properties _properties; // 데이터 로드 필요

    private int _maxLevel = 0;
    private int _currentLevel = 0;

    public Character User => _user;
    public eCollectionType CollectionType => _collectType;

    #region 스탯 프로퍼티
    public float MoveSpeed => _weaponStat.MoveSpeed;
    public float Delay => _weaponStat.Delay;
    public float Duration => _weaponStat.Duration;
    public float ThrowCount => _weaponStat.ThrowCount;
    public eWeaponType CombineWeapon => (eWeaponType)_weaponStat.CombineID;
    public int PassCount => (int)_weaponStat.PassCount;
    public int MaxLevel => _weaponStat.MaxLevel;
    #endregion

    #region 특성 프로퍼티
    public bool IsTargeting => _properties.isTargeting;
    public bool IsContinuous => _properties.isContinuous;
    public bool HasPostProcess => _properties.hasPostProcess;
    public bool IsCollisionMob => _properties.isCollisionMonster;
    public bool IsCollisionScn => _properties.isCollisionScreen;
    public bool HasReflection => _properties.hasReflection;
    public bool IsNeedDir => _properties.isNeedDir;
    public bool IsCtrlDirable => _properties.isControllableDir;
    public bool HasFelxiblePath => _properties.hasFlexiblePath;
    public bool IsRotate => _properties.isRotate;
    public bool IsFalling => _properties.isFalling;
    public bool HasSpStartPos => _properties.hasSpecificStartPos;
    #endregion

    public WeaponData(eWeaponType type)
    {
        this._user = Global_Data._character;
        _userStat = (CharacterStat)_user.UnitStat;

        _statTable = Global_Data.weaponTable[(int)type];

        _weaponStat = new WeaponStat();
        _weaponStat.SetStats(_statTable);

        _collectType = _statTable.collectType;

        _properties = Global_Data.weaponPropertyTable[(int)type]; // 데이터 로드 필요

        _maxLevel = _statTable.maxLevel;
        _currentLevel = 1;
    }

    public float GetDamageWithUnit()
    {
        return _userStat.Damage + _weaponStat.Damage;
    }

    /// <summary>
    /// 현재 데이터를 매개변수 데이터로 변경합니다.
    /// </summary>
    public void UpdateData(WeaponData data)
    {
        data._weaponStat.CopyStats(this._weaponStat);

        eWeaponType type = data._weaponStat.WeaponType;
        this._properties = data._properties;
    }

    public void LevelUp()
    {
        if (_currentLevel < _maxLevel)
            _currentLevel++;

        _weaponStat.AddDamage(_statTable.addtionalDamages[_currentLevel]);
        _weaponStat.AddDuration(_statTable.addtionalDurations[_currentLevel]);
        _weaponStat.AddPassCount(_statTable.addtionalPassCount[_currentLevel]);
        _weaponStat.AddSpeed(_statTable.addtionalMoveSpeeds[_currentLevel]);
        _weaponStat.AddThrowCount(_statTable.addtionalThrowCount[_currentLevel]);

        _weaponStat.AddDelay(_statTable.reducionDelays[_currentLevel] * -1);
    }
}