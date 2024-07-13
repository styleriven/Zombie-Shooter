using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    [SerializeField]
    public PlayerControls target;

    Monster monster;
    //[SerializeField] AudioSource audioSource;

    Animator animator;
    void Start() {
        target = FindObjectOfType<PlayerControls>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = transform.GetChild(1).GetComponent<Animator>();
        monster = GetComponent<Monster>();
    }

    void Update() {
        if(monster.IsDead()) {
            navMeshAgent.isStopped = true;
            return;
        }
        distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        if(isProvoked) {
            EngageTarget();
        }
        else if(distanceToTarget <= chaseRange) {
            isProvoked = true;
        }
    }

    public void OnDamageTaken() {
        isProvoked = true;
    }

    private void EngageTarget() {
        FaceTarget();
        if(distanceToTarget >= navMeshAgent.stoppingDistance) {
            ChaseTarget();
            animator.SetBool("Walk", true);
        }
        else {
            animator.SetBool("Walk", false);
        }
        if(distanceToTarget <= navMeshAgent.stoppingDistance) {
            AttackTarget();
        }
        else {
            animator.SetBool("Attack", false);
        }
    }

    private void ChaseTarget() {
        navMeshAgent.SetDestination(target.transform.position);
    }

    private void AttackTarget() {
        animator.SetBool("Attack", true);
    }

    private void FaceTarget() {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
