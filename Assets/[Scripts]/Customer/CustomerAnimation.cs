using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAnimation : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    public bool isCustomerAtTable;
    public bool isLeaving;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLeaving)
        {
            isCustomerAtTable = CustomerIsAtTable();
        }
        if (agent.velocity.magnitude > 0.15)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if(isCustomerAtTable)
        {
            //start sitting
        }
        
    }

    public bool CustomerIsAtTable()
    {
        float dist = agent.remainingDistance;

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void LeaveTable()
    {
        isCustomerAtTable = false;
    }
}
