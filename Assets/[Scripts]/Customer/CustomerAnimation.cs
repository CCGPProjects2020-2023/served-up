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
    public GameObject walkAwayPos;
    public Table table;
    public float rotationSpeed = 10f;
    private bool rotateToTable = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        anim = this.gameObject.GetComponent<Animator>();
        StartCoroutine(SittingDown());
        walkAwayPos = FindObjectOfType<WalkAwayPos>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLeaving)
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
                StartCoroutine(RotateCustomer(table.gameObject.transform.GetChild(0)));
                Events.onCustomerReachedTable.Invoke(table);
                anim.SetTrigger("TrSit");
                yield break;
            }
            //Debug.Log("yeild reutrn");
            yield return new WaitForEndOfFrame();
        }

    }

    IEnumerator RotateCustomer(Transform target)
    {
        while (true)
        {
            if (isCustomerAtTable)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                this.gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
            //Debug.Log("yeild reutrn");
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
        rotateToTable = false;
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
        anim.SetTrigger("TrIdle");
        agent.SetDestination(walkAwayPos.transform.position);
        StartCoroutine(DestoryCustomer());

    }

    IEnumerator DestoryCustomer()
    {
        yield return new WaitForSeconds(8);
        Destroy(this.gameObject);
    }


}
