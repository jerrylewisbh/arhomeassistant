using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    // Start is called before the first frame update


    NavMeshAgent agent;
    Animator animator;



    float timeAction = 10;
    float currentActionTime = 0;

    public bool action = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //TODO: fix this gargabe
        if (!action)
        {
            if (!agent.pathPending)
            {
                if (agent.isOnNavMesh && agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {

                        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                        {
                            animator.SetTrigger("Idle");
                        }
                    }
                }
            }
            else
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
                {
                    animator.SetTrigger("Move");
                }
            }
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            currentActionTime += Time.deltaTime;

            if (currentActionTime > timeAction)
            {

                //TODO: this is terrible, change it for the final version
                animator.SetTrigger(Random.Range(0, 2) == 0 ? "Workout" : "Resting");

                action = true;
                currentActionTime = 0;

            }
        }
    }



}
