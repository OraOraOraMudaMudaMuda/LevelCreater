using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Item
{
    int mask;
    public enum Type { HP, Stamina}
    public Type type;
    public void Awake()
    {
        mask = LayerMask.NameToLayer("Player");
    }
    public void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.layer == mask)
        {
            if(type == Type.Stamina)
                _other.gameObject.GetComponentInParent<Player>().AddStamina((int)value);
            else
                _other.gameObject.GetComponentInParent<Player>().AddHp((int)value);
            Destroy(gameObject);
        }
    }
}
