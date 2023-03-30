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
    private bool isInSittingCoroutine;
    public bool isInQueue;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        anim = this.gameObject.GetComponent<Animator>();
        StartCoroutine(SittingDown());
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLeaving)
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

        
            
        
        
    }

    IEnumerator SittingDown()
    {
        while (true)
        {
            if (isCustomerAtTable)
            {
                Debug.Log("SITYTING");
                anim.SetTrigger("TrSit");
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
        
    }

    public bool CustomerIsAtTable()
    {
        float dist = agent.remainingDistance;

        if (!agent.pathPending && !isInQueue)
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
        anim.SetBool("IsSitting", false);
        anim.SetTrigger("TrStand");
    }

    public void SitAnimationFinished()
    {
        anim.SetBool("IsSitting", true);
    }

    public void StandAnimationFinished()
    {
        //set destination to leave
    }
}
