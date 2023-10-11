using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    [field: SerializeField] Player player;
    int mask;
    public void Awake()
    {
        mask = LayerMask.NameToLayer("Enemy");
    }
    public void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.layer == mask)
            player.attackEnemyList.Add(_other.GetComponentInParent<Enemy>());
    }
    public void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.gameObject.layer == mask)
            player.attackEnemyList.Remove(_other.GetComponentInParent<Enemy>());
    }
}
