<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLancher : MonoBehaviour
{
    #region 스프레드시트에 등록된 이벤트목록
<<<<<<< HEAD

    /// <summary>
    /// 해당 이벤트는 시간과 발생시킬 이벤트를 한쌍을 이루는 구성요소
=======
    /// <summary>
    /// 해당 이벤트는 시간과 발생시킬 이벤트를 한쌍을 이루는 형태만 등록합니다.
>>>>>>> 6b9376b (#1.3)
    /// </summary>
    enum TimeWithEventTypes
    {
        None = 0,
        MainEvent = 1,
        SubEvent = 2,
        SpectialEvent = 3,
        MobChange = 4,
        ReduceFrequency = 5,
    }

<<<<<<< HEAD
    /// <summary>
    /// Special 이벤트 목록
    /// </summary>
=======
>>>>>>> 6b9376b (#1.3)
    enum SpecialEventTypes
    {
        None = 0,
        ReduceSpawnTime = 1,
        CircleSpawn = 2,
        BunningSpawn = 3,
    }

    #endregion

    #region 스테이지 구성요소
    private Data_Stage _stageDataTable;
    private Stage _stage;
    private ObjPoolManager _poolManager;
    #endregion

    private int PlayMinute
    {
        set
        {
            this._playMinute = value;

            if (value == _nextEventTime)
            {
                PlayEvent(_nextEventTime);
                _nextEventTime = GetNextEventTime();
            }
        }
    }

    [SerializeField]
    private int _playMinute;
    [SerializeField]
    private float _playSecond;
    [SerializeField]
<<<<<<< HEAD
    private int _nextEventTime;
    [SerializeField]
    private float _spawnDelay;
    [SerializeField]
    private int[] _activeMobsArr;

    private int _activeMobs;
=======
    private int _activeMobs;
    [SerializeField]
    private int _nextEventTime;
    [SerializeField]
    private float _spawnDelay;


>>>>>>> 6b9376b (#1.3)
    private int _branchTime;
    private int _startFreq_ms;
    private int _endFreq_ms;
    private float _clearTime;
    private float _frequency;

    private int _commonMonsterIdx;

<<<<<<< HEAD
    private Dictionary<TimeWithEventTypes, Queue<int[]>> _timeEventQueues
        = new Dictionary<TimeWithEventTypes, Queue<int[]>>();

    private Queue<GameObject> _circleSpawnQueue = new Queue<GameObject>();
=======
    Dictionary<TimeWithEventTypes, Queue<int[]>> _timeEventQueues
        = new Dictionary<TimeWithEventTypes, Queue<int[]>>();

    List<GameObject> _circleList = new List<GameObject>();

    int[] _activeMobsArr;
>>>>>>> 6b9376b (#1.3)

    private void Awake()
    {
        GameManager.Instance.Event.RegisterEvent(eEventType.StageSetupCompleted, StageStart);
    }

    public void StageStart()
    {
        Init();

        StartCoroutine(StartTimer(_stageDataTable.clearTime));
    }

    private void Init()
    {
        if (_poolManager == null)
            _poolManager = FindObjectOfType<ObjPoolManager>();

        InitVariables();
        InitEventTable();
        SetActiveMobsArr(ref _activeMobsArr);

        _playMinute = -1;
        _playSecond = 59;
        _spawnDelay = _startFreq_ms * 0.001f;
        _commonMonsterIdx = 0;

<<<<<<< HEAD
        _circleSpawnQueue.Clear();

        _nextEventTime = GetNextEventTime();
    }

    #region 스프레드시트 데이터 변수 초기화

    private void InitVariables()
    {
        _startFreq_ms = _stageDataTable.startEndSpawnDelay[0];
        _endFreq_ms = _stageDataTable.startEndSpawnDelay[1];
        _branchTime = _stageDataTable.branchDelay;
        _activeMobs = _stageDataTable.mobChanges[0];
        _frequency = _stageDataTable.spawnDelayInterval;
        _clearTime = _stageDataTable.clearTime;
    }

    #endregion
=======
        _nextEventTime = GetNextEventTime();
    }


>>>>>>> 6b9376b (#1.3)

    #region 시간별 이벤트 테이블 초기화, 설정

    private void InitEventTable()
    {
<<<<<<< HEAD
        if(_timeEventQueues.Count == 0)
        {
            _timeEventQueues.Add(TimeWithEventTypes.MainEvent, new Queue<int[]>());
            _timeEventQueues.Add(TimeWithEventTypes.SubEvent, new Queue<int[]>());
            _timeEventQueues.Add(TimeWithEventTypes.SpectialEvent, new Queue<int[]>());
            _timeEventQueues.Add(TimeWithEventTypes.MobChange, new Queue<int[]>());
            _timeEventQueues.Add(TimeWithEventTypes.ReduceFrequency, new Queue<int[]>());
        }
        else
=======
        if (_timeEventQueues.Count > 0)
>>>>>>> 6b9376b (#1.3)
        {
            foreach (var e in _timeEventQueues)
            {
                e.Value.Clear();
            }
<<<<<<< HEAD
        }

        Queue<int[]> targetQueue;

=======
            return;
        }

        Queue<int[]> targetQueue; // 이미 있을경우 없을경우 처리

        _timeEventQueues.Add(TimeWithEventTypes.MainEvent, new Queue<int[]>());
>>>>>>> 6b9376b (#1.3)
        targetQueue = _timeEventQueues[TimeWithEventTypes.MainEvent];
        EnqueueTimeEvents(
            targetQueue, _stageDataTable.mainEventTimes, _stageDataTable.mainEventBossMobs);

<<<<<<< HEAD
=======
        _timeEventQueues.Add(TimeWithEventTypes.SubEvent, new Queue<int[]>());
>>>>>>> 6b9376b (#1.3)
        targetQueue = _timeEventQueues[TimeWithEventTypes.SubEvent];
        EnqueueTimeEvents(
            targetQueue, _stageDataTable.subEventTimes, _stageDataTable.subEventNamedMobs);

<<<<<<< HEAD
=======
        _timeEventQueues.Add(TimeWithEventTypes.SpectialEvent, new Queue<int[]>());
>>>>>>> 6b9376b (#1.3)
        targetQueue = _timeEventQueues[TimeWithEventTypes.SpectialEvent];
        EnqueueTimeEvents(
            targetQueue, _stageDataTable.specialEventTimes, _stageDataTable.specialEventTypes);

<<<<<<< HEAD
=======
        _timeEventQueues.Add(TimeWithEventTypes.MobChange, new Queue<int[]>());
>>>>>>> 6b9376b (#1.3)
        targetQueue = _timeEventQueues[TimeWithEventTypes.MobChange];
        EnqueueTimeEvents(
            targetQueue, _stageDataTable.mobChangeTimes, _stageDataTable.mobChanges);

<<<<<<< HEAD
=======
        _timeEventQueues.Add(TimeWithEventTypes.ReduceFrequency, new Queue<int[]>());
>>>>>>> 6b9376b (#1.3)
        targetQueue = _timeEventQueues[TimeWithEventTypes.ReduceFrequency];

        for (int i = 1; i <= _clearTime / _branchTime; i++)
        {
            targetQueue.Enqueue(new int[] { _branchTime * i, (int)_frequency });
        }
    }

    private void EnqueueTimeEvents(Queue<int[]> queue, int[] times, int[] events)
    {
        for (int i = 0; i < times.Length; i++)
        {
            queue.Enqueue(new int[] { times[i], events[i] });
        }
    }

    #endregion

<<<<<<< HEAD
    #region 다음이벤트 시간탐색
=======
    #region 스프레드시트 데이터값으로 설정

    private void InitVariables()
    {
        _startFreq_ms = _stageDataTable.startEndSpawnDelay[0];
        _endFreq_ms = _stageDataTable.startEndSpawnDelay[1];
        _branchTime = _stageDataTable.branchDelay;
        _activeMobs = _stageDataTable.mobChanges[0];
        _frequency = _stageDataTable.spawnDelayInterval;
        _clearTime = _stageDataTable.clearTime;
    }

    #endregion

    #region 다음이벤트 시간탐색과 이벤트별 처리 로직
>>>>>>> 6b9376b (#1.3)

    private int GetNextEventTime()
    {
        int nearMinute = int.MaxValue;
        int nextTime = int.MaxValue;

        foreach (var e in _timeEventQueues)
        {
<<<<<<< HEAD
            if (e.Value.Count <= 0)
                continue;

=======
>>>>>>> 6b9376b (#1.3)
            nextTime = e.Value.Peek()[0];
            nearMinute = Mathf.Min(nearMinute, nextTime);
        }
        return nearMinute;
    }

<<<<<<< HEAD
    #endregion

    #region 시간별 이벤트 선택로직

    private void PlayEvent(int time)
    {
        int eventNum;
        Queue<int[]> queue;

        queue = _timeEventQueues[TimeWithEventTypes.MainEvent];
        if (queue.Count > 0 && time == queue.Peek()[0])
        {
            eventNum = queue.Dequeue()[1];
            GameObject bossMob =  GetMonster(eUnitType.Boss, _stageDataTable.bosses[eventNum]);
            _stage.Spawner.RandomSpawn(bossMob);
        }

        queue = _timeEventQueues[TimeWithEventTypes.SubEvent];
        if (queue.Count > 0 && time == queue.Peek()[0])
        {
            eventNum = queue.Dequeue()[1];
            GameObject namedMob = GetMonster(eUnitType.Named, _stageDataTable.nameds[eventNum]);
            _stage.Spawner.RandomSpawn(namedMob);
        }

        queue = _timeEventQueues[TimeWithEventTypes.SpectialEvent];
        if (queue.Count > 0 && time == queue.Peek()[0])
        {
            eventNum = queue.Dequeue()[1];
            StartSpecialEvent((SpecialEventTypes)eventNum);
        }

        queue = _timeEventQueues[TimeWithEventTypes.MobChange];
        if (queue.Count > 0 && time == queue.Peek()[0])
        {
            eventNum = queue.Dequeue()[1];
            this._activeMobs = eventNum;
        }

        queue = _timeEventQueues[TimeWithEventTypes.ReduceFrequency];
        if (queue.Count > 0 && time == queue.Peek()[0])
        {
            this._spawnDelay -= queue.Dequeue()[1] * 0.001f;
        }

    }

    #endregion

    #region Special 이벤트 처리로직
=======
    private void PlayEvent(int time)
    {
        int eventNum;

        if (time == _timeEventQueues[TimeWithEventTypes.MainEvent].Peek()[0])
        {
            eventNum = _timeEventQueues[TimeWithEventTypes.MainEvent].Dequeue()[1];
            GameObject bossMob = GetMonster(eUnitType.Boss, _stageDataTable.bosses[eventNum]);
            _stage.Spawner.RandomSpawn(bossMob);
        }

        if (time == _timeEventQueues[TimeWithEventTypes.SubEvent].Peek()[0])
        {
            eventNum = _timeEventQueues[TimeWithEventTypes.SubEvent].Dequeue()[1];
            GameObject namedMob = GetMonster(eUnitType.Named, _stageDataTable.bosses[eventNum]);
            _stage.Spawner.RandomSpawn(namedMob);
        }

        if (time == _timeEventQueues[TimeWithEventTypes.SpectialEvent].Peek()[0])
        {
            eventNum = _timeEventQueues[TimeWithEventTypes.SpectialEvent].Dequeue()[1];
            StartSpecialEvent((SpecialEventTypes)eventNum);
        }

        if (time == _timeEventQueues[TimeWithEventTypes.MobChange].Peek()[0])
        {
            eventNum = _timeEventQueues[TimeWithEventTypes.MobChange].Dequeue()[1];
            this._activeMobs = eventNum;
        }

        if (time == _timeEventQueues[TimeWithEventTypes.ReduceFrequency].Peek()[0])
        {
            this._spawnDelay -= _timeEventQueues[TimeWithEventTypes.ReduceFrequency].Dequeue()[1] * 0.001f;
        }

    }
    #endregion

    #region 시간별 이벤트 발생시 호출메서드 목록
>>>>>>> 6b9376b (#1.3)

    private void StartSpecialEvent(SpecialEventTypes eventType)
    {
        switch (eventType)
        {
            case SpecialEventTypes.None:
                break;

            case SpecialEventTypes.ReduceSpawnTime:
                _spawnDelay -= _frequency * 0.001f;
                break;

            case SpecialEventTypes.CircleSpawn:
<<<<<<< HEAD
                CircleEventLogic(eUnitType.Common, 2, 60); // 개선
                break;

            case SpecialEventTypes.BunningSpawn:
                StartCoroutine(BunningSpawn(30, _spawnDelay * 0.5f));
=======
                CircleEventLogic(eUnitType.Common, 1, 60); // 개선
                break;

            case SpecialEventTypes.BunningSpawn:
>>>>>>> 6b9376b (#1.3)
                break;

            default:
                break;
        }
    }

<<<<<<< HEAD
    private void CircleEventLogic
        (eUnitType unitType, int mobIdx, int spawnCount, int degree = 360)
    {
        GameObject mob;
        Vector3 invisiblePos = Camera.main.transform.position * 100;

        for (int i = 0; i < spawnCount; i++)
        {
            mob = GetMonster(unitType, mobIdx);
            mob.transform.position = invisiblePos;
            _circleSpawnQueue.Enqueue(mob);
        }

        _stage.Spawner.CircleSpawn(_circleSpawnQueue, degree);
    }

    IEnumerator BunningSpawn(int duration, float settingSpawnDelay)
    {
        float decreaseValue = _spawnDelay - settingSpawnDelay;
        this._spawnDelay = settingSpawnDelay;

        while (true)
        {
            yield return new WaitForSeconds(duration);

            _spawnDelay += decreaseValue;
            break;
        }
=======
    private void CircleEventLogic(eUnitType unitType, int mobIdx, int spawnCount, int degree = 360)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject mob = _poolManager.GetObj(ePoolingType.Monster);
            Monster origin = _stage.GetOriginMonster(unitType, mobIdx);
            origin.Stat.StatCopy(mob.GetComponent<Monster>().Stat);
            _circleList.Add(mob);
        }
        _stage.Spawner.CircleSpawn(_circleList, degree);
>>>>>>> 6b9376b (#1.3)
    }

    #endregion

<<<<<<< HEAD
    #region 몬스터 스폰 로직

    private GameObject GetMonster(eUnitType unitType, int mobIdx)
    {
        GameObject mob = _poolManager.GetObj(ePoolingType.Monster);
        Base_Unit origin = _stage.GetOriginMonster(unitType, mobIdx);
        Base_Unit clone = mob.GetComponent<Base_Unit>();

        origin.UnitStat.StatCopy(clone.UnitStat);
        clone.Init();

        return mob;
    }

    private void DelaySpawn()
    {
        while (true)
        {
            if (_activeMobsArr[_commonMonsterIdx] == 1)
                break;

            _commonMonsterIdx = (_commonMonsterIdx + 1) % _activeMobsArr.Length;
        }

        GameObject mob = GetMonster(eUnitType.Common, _commonMonsterIdx + 1);
        _stage.Spawner.RandomSpawn(mob);

        _commonMonsterIdx = (_commonMonsterIdx + 1) % _activeMobsArr.Length;
    }

    #endregion

    #region 타이머

=======
>>>>>>> 6b9376b (#1.3)
    private IEnumerator StartTimer(float clearTime)
    {
        int timerMinute = _playMinute;
        float timerSecond = _playSecond;

        int targetMinute = Mathf.FloorToInt(clearTime);
        float targetSecond = clearTime - targetMinute;

        float tempSpawnDelay = _spawnDelay;

        while (true)
        {
            if (targetMinute <= timerMinute && timerSecond <= targetSecond)
                break;

            yield return new WaitForSeconds(0.1f);

            timerSecond += 0.1f;
            tempSpawnDelay -= 0.1f;

            if (timerSecond >= 60)
            {
                timerSecond = 0;
                timerMinute++;

                this.PlayMinute = timerMinute;
            }

            this._playSecond = timerSecond;

            if (tempSpawnDelay <= 0)
            {
                DelaySpawn();
                tempSpawnDelay = _spawnDelay;
            }
        }
    }

<<<<<<< HEAD
    #endregion
=======
    private void DelaySpawn()
    {
        while (true)
        {
            if (_activeMobsArr[_commonMonsterIdx] == 1)
                break;

            _commonMonsterIdx = (_commonMonsterIdx + 1) % _activeMobsArr.Length;
        }

        GameObject mob = GetMonster(eUnitType.Common, _commonMonsterIdx + 1);
        _stage.Spawner.RandomSpawn(mob);

        _commonMonsterIdx = (_commonMonsterIdx + 1) % _activeMobsArr.Length;
    }

    private GameObject GetMonster(eUnitType unitType, int eventIdx)
    {
        GameObject mob = _poolManager.GetObj(ePoolingType.Monster);
        Monster origin = _stage.GetOriginMonster(unitType, eventIdx);
        origin.Stat.StatCopy(mob.GetComponent<Monster>().Stat);

        return mob;
    }
>>>>>>> 6b9376b (#1.3)

    public void SetStageData(Data_Stage data)
    {
        this._stageDataTable = data;
    }

    public void SetStage(Stage stage)
    {
        this._stage = stage;
    }

    /// <summary>
    /// 스폰 대상인 몬스터를 구별하기위한 배열생성로직
    /// </summary>
    /// <param name="mobArr"></param>
    private void SetActiveMobsArr(ref int[] mobArr)
    {
        mobArr = new int[_stage.GetMonsterCount(eUnitType.Common)];

        int temp = _activeMobs;

        for (int i = 0; i < mobArr.Length; i++)
        {
            mobArr[(mobArr.Length - 1) - i] = temp % 10;
            temp /= 10;
        }
    }

}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLancher : MonoBehaviour
{
    #region 스프레드시트에 등록된 이벤트목록

    /// <summary>
    /// 해당 이벤트는 시간과 발생시킬 이벤트를 한쌍을 이루는 구성요소
    /// </summary>
    enum TimeWithEventTypes
    {
        None = 0,
        MainEvent = 1,
        SubEvent = 2,
        SpectialEvent = 3,
        MobChange = 4,
        ReduceFrequency = 5,
    }

    /// <summary>
    /// Special 이벤트 목록
    /// </summary>
    enum SpecialEventTypes
    {
        None = 0,
        ReduceSpawnTime = 1,
        CircleSpawn = 2,
        BunningSpawn = 3,
    }

    #endregion

    #region 스테이지 구성요소
    private Data_Stage _stageDataTable;
    private Stage _stage;
    private ObjPoolManager _poolManager;
    #endregion

    private int PlayMinute
    {
        set
        {
            this._playMinute = value;

            if (value == _nextEventTime)
            {
                PlayEvent(_nextEventTime);
                _nextEventTime = GetNextEventTime();
            }
        }
    }

    [SerializeField]
    private int _playMinute;
    [SerializeField]
    private float _playSecond;
    [SerializeField]
    private int _nextEventTime;
    [SerializeField]
    private float _spawnDelay;
    [SerializeField]
    private int[] _activeMobsArr;

    private int _activeMobs;
    private int _branchTime;
    private int _startFreq_ms;
    private int _endFreq_ms;
    private float _clearTime;
    private float _frequency;

    private int _commonMonsterIdx;

    private Dictionary<TimeWithEventTypes, Queue<int[]>> _timeEventQueues
        = new Dictionary<TimeWithEventTypes, Queue<int[]>>();

    private Queue<GameObject> _circleSpawnQueue = new Queue<GameObject>();

    private void Awake()
    {
        GameManager.Instance.Event.RegisterEvent(eEventType.StageSetupCompleted, StageStart);
    }

    public void StageStart()
    {
        Init();

        StartCoroutine(StartTimer(_stageDataTable.clearTime));
    }

    private void Init()
    {
        if (_poolManager == null)
            _poolManager = FindObjectOfType<ObjPoolManager>();

        InitVariables();
        InitEventTable();
        SetActiveMobsArr(ref _activeMobsArr);

        _playMinute = -1;
        _playSecond = 59;
        _spawnDelay = _startFreq_ms * 0.001f;
        _commonMonsterIdx = 0;

        _circleSpawnQueue.Clear();

        _nextEventTime = GetNextEventTime();
    }

    #region 스프레드시트 데이터 변수 초기화

    private void InitVariables()
    {
        _startFreq_ms = _stageDataTable.startEndSpawnDelay[0];
        _endFreq_ms = _stageDataTable.startEndSpawnDelay[1];
        _branchTime = _stageDataTable.branchDelay;
        _activeMobs = _stageDataTable.mobChanges[0];
        _frequency = _stageDataTable.spawnDelayInterval;
        _clearTime = _stageDataTable.clearTime;
    }

    #endregion

    #region 시간별 이벤트 테이블 초기화, 설정

    private void InitEventTable()
    {
        if(_timeEventQueues.Count == 0)
        {
            _timeEventQueues.Add(TimeWithEventTypes.MainEvent, new Queue<int[]>());
            _timeEventQueues.Add(TimeWithEventTypes.SubEvent, new Queue<int[]>());
            _timeEventQueues.Add(TimeWithEventTypes.SpectialEvent, new Queue<int[]>());
            _timeEventQueues.Add(TimeWithEventTypes.MobChange, new Queue<int[]>());
            _timeEventQueues.Add(TimeWithEventTypes.ReduceFrequency, new Queue<int[]>());
        }
        else
        {
            foreach (var e in _timeEventQueues)
            {
                e.Value.Clear();
            }
        }

        Queue<int[]> targetQueue;

        targetQueue = _timeEventQueues[TimeWithEventTypes.MainEvent];
        EnqueueTimeEvents(
            targetQueue, _stageDataTable.mainEventTimes, _stageDataTable.mainEventBossMobs);

        targetQueue = _timeEventQueues[TimeWithEventTypes.SubEvent];
        EnqueueTimeEvents(
            targetQueue, _stageDataTable.subEventTimes, _stageDataTable.subEventNamedMobs);

        targetQueue = _timeEventQueues[TimeWithEventTypes.SpectialEvent];
        EnqueueTimeEvents(
            targetQueue, _stageDataTable.specialEventTimes, _stageDataTable.specialEventTypes);

        targetQueue = _timeEventQueues[TimeWithEventTypes.MobChange];
        EnqueueTimeEvents(
            targetQueue, _stageDataTable.mobChangeTimes, _stageDataTable.mobChanges);

        targetQueue = _timeEventQueues[TimeWithEventTypes.ReduceFrequency];

        for (int i = 1; i <= _clearTime / _branchTime; i++)
        {
            targetQueue.Enqueue(new int[] { _branchTime * i, (int)_frequency });
        }
    }

    private void EnqueueTimeEvents(Queue<int[]> queue, int[] times, int[] events)
    {
        for (int i = 0; i < times.Length; i++)
        {
            queue.Enqueue(new int[] { times[i], events[i] });
        }
    }

    #endregion

    #region 다음이벤트 시간탐색

    private int GetNextEventTime()
    {
        int nearMinute = int.MaxValue;
        int nextTime = int.MaxValue;

        foreach (var e in _timeEventQueues)
        {
            if (e.Value.Count <= 0)
                continue;

            nextTime = e.Value.Peek()[0];
            nearMinute = Mathf.Min(nearMinute, nextTime);
        }
        return nearMinute;
    }

    #endregion

    #region 시간별 이벤트 선택로직

    private void PlayEvent(int time)
    {
        int eventNum;
        Queue<int[]> queue;

        queue = _timeEventQueues[TimeWithEventTypes.MainEvent];
        if (queue.Count > 0 && time == queue.Peek()[0])
        {
            eventNum = queue.Dequeue()[1];
            GameObject bossMob =  GetMonster(eUnitType.Boss, _stageDataTable.bosses[eventNum]);
            _stage.Spawner.RandomSpawn(bossMob);
        }

        queue = _timeEventQueues[TimeWithEventTypes.SubEvent];
        if (queue.Count > 0 && time == queue.Peek()[0])
        {
            eventNum = queue.Dequeue()[1];
            GameObject namedMob = GetMonster(eUnitType.Named, _stageDataTable.nameds[eventNum]);
            _stage.Spawner.RandomSpawn(namedMob);
        }

        queue = _timeEventQueues[TimeWithEventTypes.SpectialEvent];
        if (queue.Count > 0 && time == queue.Peek()[0])
        {
            eventNum = queue.Dequeue()[1];
            StartSpecialEvent((SpecialEventTypes)eventNum);
        }

        queue = _timeEventQueues[TimeWithEventTypes.MobChange];
        if (queue.Count > 0 && time == queue.Peek()[0])
        {
            eventNum = queue.Dequeue()[1];
            this._activeMobs = eventNum;
        }

        queue = _timeEventQueues[TimeWithEventTypes.ReduceFrequency];
        if (queue.Count > 0 && time == queue.Peek()[0])
        {
            this._spawnDelay -= queue.Dequeue()[1] * 0.001f;
        }

    }

    #endregion

    #region Special 이벤트 처리로직

    private void StartSpecialEvent(SpecialEventTypes eventType)
    {
        switch (eventType)
        {
            case SpecialEventTypes.None:
                break;

            case SpecialEventTypes.ReduceSpawnTime:
                _spawnDelay -= _frequency * 0.001f;
                break;

            case SpecialEventTypes.CircleSpawn:
                CircleEventLogic(eUnitType.Common, 2, 60); // 개선
                break;

            case SpecialEventTypes.BunningSpawn:
                StartCoroutine(BunningSpawn(30, _spawnDelay * 0.5f));
                break;

            default:
                break;
        }
    }

    private void CircleEventLogic
        (eUnitType unitType, int mobIdx, int spawnCount, int degree = 360)
    {
        GameObject mob;
        Vector3 invisiblePos = Camera.main.transform.position * 100;

        for (int i = 0; i < spawnCount; i++)
        {
            mob = GetMonster(unitType, mobIdx);
            mob.transform.position = invisiblePos;
            _circleSpawnQueue.Enqueue(mob);
        }

        _stage.Spawner.CircleSpawn(_circleSpawnQueue, degree);
    }

    IEnumerator BunningSpawn(int duration, float settingSpawnDelay)
    {
        float decreaseValue = _spawnDelay - settingSpawnDelay;
        this._spawnDelay = settingSpawnDelay;

        while (true)
        {
            yield return new WaitForSeconds(duration);

            _spawnDelay += decreaseValue;
            break;
        }
    }

    #endregion

    #region 몬스터 스폰 로직

    private GameObject GetMonster(eUnitType unitType, int mobIdx)
    {
        GameObject mob = _poolManager.GetObj(ePoolingType.Monster);
        Base_Unit origin = _stage.GetOriginMonster(unitType, mobIdx);
        Base_Unit clone = mob.GetComponent<Base_Unit>();

        origin.UnitStat.StatCopy(clone.UnitStat);
        clone.Init();

        return mob;
    }

    private void DelaySpawn()
    {
        while (true)
        {
            if (_activeMobsArr[_commonMonsterIdx] == 1)
                break;

            _commonMonsterIdx = (_commonMonsterIdx + 1) % _activeMobsArr.Length;
        }

        GameObject mob = GetMonster(eUnitType.Common, _commonMonsterIdx + 1);
        _stage.Spawner.RandomSpawn(mob);

        _commonMonsterIdx = (_commonMonsterIdx + 1) % _activeMobsArr.Length;
    }

    #endregion

    #region 타이머

    private IEnumerator StartTimer(float clearTime)
    {
        int timerMinute = _playMinute;
        float timerSecond = _playSecond;

        int targetMinute = Mathf.FloorToInt(clearTime);
        float targetSecond = clearTime - targetMinute;

        float tempSpawnDelay = _spawnDelay;

        while (true)
        {
            if (targetMinute <= timerMinute && timerSecond <= targetSecond)
                break;

            yield return new WaitForSeconds(0.1f);

            timerSecond += 0.1f;
            tempSpawnDelay -= 0.1f;

            if (timerSecond >= 60)
            {
                timerSecond = 0;
                timerMinute++;

                this.PlayMinute = timerMinute;
            }

            this._playSecond = timerSecond;

            if (tempSpawnDelay <= 0)
            {
                DelaySpawn();
                tempSpawnDelay = _spawnDelay;
            }
        }
    }

    #endregion

    public void SetStageData(Data_Stage data)
    {
        this._stageDataTable = data;
    }

    public void SetStage(Stage stage)
    {
        this._stage = stage;
    }

    /// <summary>
    /// 스폰 대상인 몬스터를 구별하기위한 배열생성로직
    /// </summary>
    /// <param name="mobArr"></param>
    private void SetActiveMobsArr(ref int[] mobArr)
    {
        mobArr = new int[_stage.GetMonsterCount(eUnitType.Common)];

        int temp = _activeMobs;

        for (int i = 0; i < mobArr.Length; i++)
        {
            mobArr[(mobArr.Length - 1) - i] = temp % 10;
            temp /= 10;
        }
    }

}
>>>>>>> b757374 (# 1.4)
