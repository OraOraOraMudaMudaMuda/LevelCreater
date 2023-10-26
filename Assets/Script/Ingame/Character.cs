using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum State { IDLE, MOVE, ATTACK, DEAD }
    public State state;
    public Rigidbody2D rigid;
    [field: SerializeField] protected float speed = 3f;

    public SpriteRenderer image;
    public Animator animator;
    

    [field: SerializeField] public int MaxHealth { get; protected set; } = 5;
    [field: SerializeField] public int Health { get; protected set; } = 5;
    [field: SerializeField] public int MaxStamina { get; protected set; } = 100;
    [field: SerializeField] public int Stamina { get; protected set; } = 100;
    [field: SerializeField] public int Power { get; protected set; } = 1;

    public virtual void Init()
    {
        Stamina = MaxStamina;
        Health = MaxHealth;
        Idle();
    }

    public virtual void Idle() { rigid.velocity = Vector2.zero; }
    public virtual void Movement(Vector2 _val)    {    }
    public virtual void Dead() { }
    public virtual void Attack() { }

    public virtual void Damaged(int _damage)
    {
        Health -= _damage;
        if (Health <= 0)
        {
            Health = 0;
            Dead();
        }
    }
    
}
