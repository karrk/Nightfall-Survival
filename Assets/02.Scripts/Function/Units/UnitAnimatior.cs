using System.Collections;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class UnitAnimatior : MonoBehaviour
{
    private const string SpritePath = "Monsters/";

    SpriteRenderer _render;
    SpriteLibrary _spriteLib;
    SpriteResolver _resolver;
    Animator _anim;

    public SpriteRenderer Render => _render;

    private void Awake()
    {
        _render = GetComponentInChildren<SpriteRenderer>();
        _spriteLib = GetComponentInChildren<SpriteLibrary>();
        _resolver = GetComponentInChildren<SpriteResolver>();
        _anim = GetComponent<Animator>();
    }

    public void SetSpriteAsset(string spriteName)
    {
        this._spriteLib.spriteLibraryAsset = Resources.Load<SpriteLibraryAsset>
            ($"{SpritePath}{spriteName}/{spriteName}");
    }

    public void SetStateAnim(eUnitStates state)
    {
        _resolver.SetCategoryAndLabel(state.ToString(),$"{state}_0");
    }

    const string DeadParameter = "Die";
    const string IdleParameter = "Idle";
    const string MoveParameter = "Run";
    const string OnDamageParameter = "Hit";
    const string AttackParameter = "Attack";

    public float CurrentAnimLength
    {
        get
        {
            return _anim.GetCurrentAnimatorStateInfo(0).length;
        }
    }

    public void Init()
    {
        _anim.SetBool(DeadParameter, false);
        _anim.SetBool(MoveParameter, false);
        _anim.SetBool(IdleParameter, false);
    }

    public void SetIdleAnim(bool active)
    {
        Init();
        _anim.SetBool(IdleParameter, active);
    }

    public void SetMoveAnim(bool active)
    {
        Init();
        _anim.SetBool(MoveParameter, active);
    }

    public void SetDeadAnim(bool active)
    {
        Init();
        _anim.SetBool(DeadParameter, active);
    }

    public void PlayOnDamagedAnim()
    {
        _anim.SetTrigger(OnDamageParameter);
    }

    public void PlayAttackAnim()
    {
        _anim.SetTrigger(AttackParameter);
    }
}