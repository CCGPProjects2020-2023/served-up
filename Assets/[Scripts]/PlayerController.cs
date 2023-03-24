using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputActions input;
    RecipeSystem recipeSystem;
    Vector2 move = Vector2.zero;
    public float speed = 10;
    public float rotationSpeed = 600;
    Rigidbody rb;
    public LayerMask placeableLayer;
    public GameObject hitObject;
    public GameObject heldItem;
    public GameObject heldItemPos;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = new PlayerInputActions();
        input.Player.Enable();
        input.Player.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => move = Vector2.zero;
        input.Player.Interact.performed += ctx => Interact();
        input.Player.Pickup.performed += ctx => Pickup();
        recipeSystem = RecipeSystem.Instance;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShootRaycast();
        Move();
        SpeedControl();
    }

    private void ShootRaycast()
    {
        RaycastHit objectHit;
        Debug.DrawRay(transform.position, transform.forward * 1, Color.green);
        if (Physics.Raycast(transform.position, transform.forward, out objectHit, 1, placeableLayer))
        {
            hitObject = objectHit.collider.gameObject;
        } else
        {
            hitObject = null;
        }
    }

    private void Pickup()
    {
        if(hitObject)
        {
            Placeable placeable = hitObject.GetComponent<Placeable>();
            if(!placeable.item && heldItem)
            {
                placeable.item = heldItem;
                placeable.item.transform.SetParent(placeable.itemPos.transform);
                placeable.item.transform.localPosition = Vector3.zero;
                heldItem = null;
            } 
            else if(placeable.item && !heldItem)
            {
                placeable.item.transform.SetParent(heldItemPos.transform);
                heldItem = placeable.item;
                heldItem.transform.localPosition = Vector3.zero;
                placeable.item = null;
            } else
            {
                ItemSO outputItem = recipeSystem.GetRecipeOutput(placeable.item.GetComponent<ItemSOHolder>().itemSO, heldItem.GetComponent<ItemSOHolder>().itemSO);
                if (outputItem != null)
                {
                    Debug.Log(outputItem.name);
                }
            }
        }
    }

    private void Interact()
    {
        throw new NotImplementedException();
    }

    public void Move()
    {
        Vector3 moveDir = new Vector3(move.x, 0f, move.y);

        rb.AddForce(moveDir * speed, ForceMode.Force);

        if (move != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void SpeedControl()
    {
        Vector3 vel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (vel.magnitude > speed)
        {
            Vector3 controlVel = vel.normalized * speed;

            rb.velocity = new Vector3(controlVel.x, 0f, controlVel.z);
        }
    }
}
