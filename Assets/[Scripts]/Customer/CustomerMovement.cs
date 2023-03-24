using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    public int currentTableIndex;
    public GameObject TableList;
    public List<GameObject> tables;
    public Vector3 targetPos;
    public float movementSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        tables = GameObject.FindGameObjectWithTag("TableList").GetComponent<TablesList>().tables;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
