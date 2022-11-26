using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum ObjectType
{
    PLAYER = 0,
    ENEMY = 1,
    RESOURCE = 2
}

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private ObjectType type;

    private int MAX_HEALTH = 100;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Damage(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }
    }

    public void SetHealth(int maxHealth, int health)
    {
        this.MAX_HEALTH = maxHealth;
        this.health = health;
    }

    // Added for Visual Indicators
    private IEnumerator VisualIndicator(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void Damage(int amount)
    {
        if(amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }
        
        this.health -= amount;
        StartCoroutine(VisualIndicator(Color.red)); // Added for Visual Indicators

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative healing");
        }

        bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;
        StartCoroutine(VisualIndicator(Color.green)); // Added for Visual Indicators

        if (wouldBeOverMaxHealth)
        {
            this.health = MAX_HEALTH;
        }
        else
        {
            this.health += amount;
        }
    }

    private void Die()
    {
        Debug.Log("I am Dead!");
        if (type == ObjectType.ENEMY)
        {
            if (EnemySpawner.Instance.spawnedEnemy.Contains(this.GameObject()))
            {
                EnemySpawner.Instance.spawnedEnemy.Remove(this.GameObject());
            }
        }
        else if (type == ObjectType.RESOURCE)
        {
            if (EnemySpawner.Instance.spawnedResource.Contains(this.GameObject()))
            {
                EnemySpawner.Instance.spawnedResource.Remove(this.GameObject());
            }
        }
        Destroy(gameObject);
    }
}
