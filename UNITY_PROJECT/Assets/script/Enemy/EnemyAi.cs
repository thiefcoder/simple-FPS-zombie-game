using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    Transform target;
    [HideInInspector]
    public NavMeshAgent agent;
    Animator animator;
    bool isDead=false;
    public bool canAttack = true;
    [SerializeField]
    float chaseDistance = 2f;
    [SerializeField]
    float turnspeed = 2f;
    public float damageAmount = 35f;
    [SerializeField]
    float attackTime = 2f;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance=Vector3.Distance(transform.position, target.position);

        if (!isDead && !PlayerHealth.singleton.isDead)
        {
            if (distance < chaseDistance && canAttack)
            {
                AttackPlayer();
            }

            else if (distance > chaseDistance)
            {
                ChasePlayer();
            }
        }
        else
        {
            DisableEnemy();
        }
    }
    public void EnemyDeathAnim()
    {
        isDead = true;
        animator.SetBool("IsDead", true);
    }

    void ChasePlayer()
    {
        agent.updateRotation = true;
        agent.updatePosition = true;
        agent.SetDestination(target.position);
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsAttacking", false);
    }
    void AttackPlayer()
    {
        agent.updateRotation = false;
        Vector3 Direction = target.position-transform.position;
        Direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Direction),turnspeed*Time.deltaTime);
        agent.updatePosition = false;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsAttacking", true);
        StartCoroutine(AttackTime());
    }
    void DisableEnemy()
    {
        canAttack = false;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsAttacking", false);

    }
    IEnumerator AttackTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.3f);
        PlayerHealth.singleton.DamagePlayer(damageAmount);
        yield return new WaitForSeconds(attackTime);
        canAttack = true;
    }
}
