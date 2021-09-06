using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Weapon : Pickup
{
    public float attackSpeed;

    public float damage;

    public TrailRenderer trailRenderer;

    public float MultiplierDamage
    {
        get;
        set;
    }

    protected Weapon()
    {
    }

    protected void Cooldown()
    {
        base.readyToUse = true;
    }

    public float GetAttackSpeed()
    {
        return this.attackSpeed;
    }

    public void Start()
    {
        this.MultiplierDamage = 1f;
    }
}