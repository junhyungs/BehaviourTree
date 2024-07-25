using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int _health;

    private void Awake()
    {
        _health = 30;
    }

    public bool TakeHit()
    {
        _health -= 10;

        bool isDead = _health <= 0;

        if (isDead)
        {
            Die();
        }

        return isDead;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
