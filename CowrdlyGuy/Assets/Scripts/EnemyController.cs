using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private CharacterController controller;

    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    public bool isFollow;
    public bool isAttack;
    public bool isLive;

    int multiplier = 1; // or more
    float range = 30;
    public float maxDistance = .6f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        controller.detectCollisions = true;

        isLive = true;

        InvokeRepeating("Follow", Random.Range(1f, 2f), 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.playerIsLive)
        {
            EnemyLogic();
        }
        else
        {
            IdleAction();
        }
        
    }

    private void EnemyLogic()
    {
        if (isLive && !isAttack)
        {
            if (isFollow && !GameManager.instance.enemyIsFlee)
            {
                agent.SetDestination(player.position);
                RunAction();
            }

            if (!isFollow && !GameManager.instance.enemyIsFlee)
            {
                IdleAction();
            }


            if (GameManager.instance.enemyIsFlee && Vector3.Distance(transform.position, player.position) < maxDistance)
            {
                Run();
                RunAction();
            }

            if (GameManager.instance.enemyIsFlee && Vector3.Distance(transform.position, player.position) > maxDistance)
            {
                IdleAction();
            }

        }
        else if (isLive && isAttack)
        {
            agent.isStopped = false;
            animator.SetBool("Attack", true);
            animator.SetBool("Run", false);
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Dead", true);
        }
    }

    public void Follow()
    {
        isFollow = (Random.value > 0.5f);
    }
    
    private void RunAction()
    {
        agent.isStopped = false;
        animator.SetBool("Run", true);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", false);

    }

    private void IdleAction()
    {
        agent.isStopped = true;
        animator.SetBool("Run", false);
        animator.SetBool("Idle", true);
        animator.SetBool("Attack", false);

    }

    void Run()
    {
        Vector3 runTo = transform.position - player.position;

        Vector3 newPos = transform.position + runTo;

        agent.SetDestination(newPos);

    }

   
}
