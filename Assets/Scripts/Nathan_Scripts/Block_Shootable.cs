using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Shootable : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health = health - damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
    }
}
