using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Enemy : Mob
{
    public GameObject spawnOnDeath;
    public int numToSpawnOnDeath;

    public EnemyType enemyType = EnemyType.None;
    public static event Action<EnemyType> OnAnyEnemyKilled;

    protected void Die(bool shouldGoLeft)
    {
        SpinAway(true);
        if (spawnOnDeath)
        {
            for (int i = 0; i < numToSpawnOnDeath; i++)
                Instantiate(spawnOnDeath, transform.position, Quaternion.identity);
        }
        OnAnyEnemyKilled?.Invoke(enemyType);
        Destroy(gameObject, 3f);
    }
}

public enum EnemyType
{
    None, Slime
}