using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float speed = 1.5f;

    [SerializeField]
    private EnemyData data;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetEnemyValues();
    }

    // Update is called once per frame
    void Update()
    {
        Swarm();
    }

    private void SetEnemyValues()
    {
        GetComponent<Health>().SetHealth(data.hp, data.hp);
        damage = data.damage;
        speed = Random.Range((float)data.speed, (float)data.speed + 2f);
        // Debug.Log(Random.Range((float)data.speed, (float)data.speed + 2f));
    }

    private void Swarm()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogError(other.gameObject.name);
        // other.GetComponent<Health>().Damage(damage);
        if (this.GetComponent<Health>().GetObjectType == ObjectType.RESOURCE)
        {
            TreeHandler.Instance.AddCoin();
            this.GetComponent<Health>().Damage(10000);
            Debug.Log("AddCoin");
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                if (other.GetComponent<Health>() != null)
                {
                    other.GetComponent<Health>().Damage(damage);
                    Debug.Log("Dame PLAYER");                    
                }
            }
            else if (other.CompareTag("Shield"))
            {
                this.GetComponent<Health>().Damage(10000);
                Debug.Log("Dame Object");
            }
        }
    }
}
