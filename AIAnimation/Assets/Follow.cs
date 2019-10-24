using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance > 2.0f)
        {
            animator.SetBool("movement", true);
        }
        else
            animator.SetBool("movement", false);

        agent.SetDestination(target.transform.position);
    }
}
