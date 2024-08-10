using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public bool isAttack;
    public UIGauge uiGauge;
    public List<Enemy> attackEnemyList;
    public int score;

    public void Start()
    {
        if (uiGauge == null)
        {
            uiGauge = UIManager.Instance.transform.GetComponentInChildren<UIGauge>();
            uiGauge.HPRefresh(Health, MaxHealth);
            uiGauge.StaminaRefresh(Stamina, MaxStamina);
        }
        GameManager.Instance.GameStart();
    }

    public void Update()
    {
        AddStamina(Time.deltaTime * 2.5f);
    }

    public override void Init()
    {
        base.Init();
        transform.position = Vector2.zero;
        score = 0;

        if (uiGauge != null)
        {
            uiGauge.HPRefresh(Health, MaxHealth);
            uiGauge.StaminaRefresh(Stamina, MaxStamina);
        }
    }

    public override void Idle()
    {
        base.Idle();

        if (state != State.IDLE)
        {
            state = State.IDLE;
            animator.Play(GameManager.Instance.IdleHash);
        }
    }

    public override void Movement(Vector2 _val)
    {
        if (state == State.ATTACK || state == State.DEAD)
            return;

        base.Movement(_val);
        if (_val.x < 0f)
            transform.localScale = Vector3.one + Vector3.left * 2f;
        else if (_val.x > 0f)
            transform.localScale = Vector3.one;

        rigid.velocity = _val.normalized * speed;

        if (state != State.MOVE)
        {
            state = State.MOVE;
            animator.Play(GameManager.Instance.MoveHash);
        }
    }

    public override void Attack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == GameManager.Instance.AttackHash 
            || Stamina < 10)
        {
            return;
        }
        
        base.Attack();
        rigid.velocity = Vector2.zero;

        if (state != State.ATTACK)
        {
            state = State.ATTACK;
            animator.Play(GameManager.Instance.AttackHash);
        }

        Enemy[] attackArray = attackEnemyList.ToArray();

        for (int i = 0; i < attackArray.Length; i++)
        {
            attackArray[i].Damaged(Power);
            if (attackArray[i].Health <= 0)
            {
                attackEnemyList.Remove(attackArray[i]);
                score++;
            }
        }
        AddStamina(-10);        
    }

    public override void Damaged(int _damage)
    {
        base.Damaged(_damage);
        uiGauge.HPRefresh(Health, MaxHealth);
    }

    public override void Dead()
    {
        if (state != State.DEAD)
        {
            state = State.DEAD;
            animator.Play(GameManager.Instance.DeadHash);
        }
    }

    public void AddStamina(float _stamina)
    {
        Stamina += _stamina;

        if (Stamina > MaxStamina)
            Stamina = MaxStamina;

        uiGauge.StaminaRefresh(Stamina, MaxStamina);
    }

    public void AddHp(int _hp)
    {
        Health += _hp;

        if (Health > MaxHealth)
            Health = MaxHealth;

        uiGauge.HPRefresh(Health, MaxHealth);
    }
}
