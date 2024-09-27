using System.Collections.Generic;
using UnityEngine;

public class Global_Data : MonoBehaviour
{
    private static Data_Player _player;


    private static Dictionary<eMonsterKind, Data_Monster> _mosnterTable = new Dictionary<eMonsterKind, Data_Monster>();

    public static Dictionary<eMonsterKind, Data_Monster> mosnterTable => _mosnterTable;


    private static Dictionary<int, Data_Stage> _stageTable = new Dictionary<int, Data_Stage>();

    public static Dictionary<int, Data_Stage> stageTable => _stageTable;
}
