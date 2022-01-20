using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public float lookRadius = 10f;
    public float maxHealth = 50f;
    public float health;
    public float healthMultiplier = .25f;
    public float playerExp;

    public float damage = 5;
    public float range = 2;

    public float timer;

    public PlayerLevelUp playerLevelUp;

    public bool isBoss = false;
    
    public Transform homebase;
    Transform target;
    Transform player;
    Transform enemyTransform;
    NavMeshAgent agent;
    
    void Start()
    {
        player = PlayerManager.instance.player.transform;
        target = homebase.transform;
        agent = GetComponent<NavMeshAgent>();
        health = maxHealth;
        enemyTransform = GetComponent<Transform>();
        
        
    }

    private void OnEnable()
    {
        playerExp = maxHealth * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if(distance <= lookRadius && health >= maxHealth * 0.5f)
        {
            target = PlayerManager.instance.player.transform;
            agent.SetDestination(target.position);
        }

        else
        {
            target = homebase.transform;
            if (agent == null)
                return;
            agent.SetDestination(target.position);
        }

        timer += Time.deltaTime;

        if(timer >= 4)
        {
            Shoot();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius); 
    }

    public void TakeDamage(float amount)
    {
        
        health -= amount;
        if(health <= 0f)
        {
            playerLevelUp.playerExp += playerExp;
            Die();
        }
        
    }
    void Die()
    {
        if (isBoss)
        {
            playerLevelUp.damageBoost = 5;
            playerLevelUp.rangeBoost = 5;
        }
        Destroy(gameObject);


    }

    public void OnLevelUp()
    {
        maxHealth *= (1 + (healthMultiplier * (playerLevelUp.level - 1)));
    }

    void Shoot()
    {
        

        RaycastHit hit;
        if (Physics.Raycast(enemyTransform.position, enemyTransform.forward, out hit, range))
        {
            PlayerLevelUp playerLevelUp = hit.transform.GetComponent<PlayerLevelUp>();

            if (playerLevelUp != null)
            {
                playerLevelUp.TakeDamage(damage);
            }
        }
    }


}
