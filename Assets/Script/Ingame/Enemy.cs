using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public Collider2D detectCollider;
    public Collider2D attackCollider;
    private int mask;

    public void AIStart(Transform _target)
    {
        if(mask == 0)
            mask = 1 << LayerMask.NameToLayer("Player");
        StartCoroutine(AICoroutine(_target));
    }

    public override void Dead()
    {
        Destroy(gameObject);
    }
    public void DeadStart()
    {
        state = State.DEAD;
        rigid.velocity = Vector2.zero;
        animator.Play(GameManager.Instance.DeadHash);
        StopAllCoroutines();
    }
    public void AttackStart()
    {        
        if(animator.GetCurrentAnimatorStateInfo(0).shortNameHash != GameManager.Instance.AttackHash)
            animator.Play(GameManager.Instance.AttackHash);
        
        state = State.ATTACK;
        rigid.velocity = Vector2.zero;
    }
    public override void Attack()
    {
        base.Attack();
        if (attackCollider.IsTouchingLayers(mask))
            GameManager.Instance.Player.GetComponent<Player>().Damaged(1);
    }

    public void Move(Transform _target)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash != GameManager.Instance.MoveHash)
            animator.Play(GameManager.Instance.MoveHash);

        state = State.MOVE;
        if (_target == null)
            return;
        Vector2 targetPos = Vector2.zero;
        if (transform.position.x < _target.position.x)
            targetPos = Vector2.left * 0.5f;
        else
            targetPos = Vector2.right * 0.5f;

        Vector2 dir = (Vector2)(_target.position - transform.position) + targetPos;

        if (dir.x < 0)
        {
            transform.localScale = Vector3.one + (Vector3.left * 2f);
        }
        else if (dir.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        rigid.velocity = dir.normalized * speed;
    }
    public IEnumerator AICoroutine(Transform _target)
    {
        while (Health > 0)
        {
            if (detectCollider.IsTouchingLayers(mask))
            {
                AttackStart();
                yield return new WaitForSeconds(1f);
            }
            else
            {
                Move(_target);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    public override void Damaged(int _damage)
    {
        Health -= _damage;
        if (Health <= 0)
        {
            Health = 0;
            DeadStart();
        }
    }


}
