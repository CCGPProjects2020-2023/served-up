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
    }

    void Start()
    {
        recipeSystem = RecipeSystem.Instance;
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
        Vector3 rayPos = new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z);
        Debug.DrawRay(rayPos, transform.forward * 1, Color.green);
        if (Physics.Raycast(rayPos, transform.forward, out objectHit, 1, placeableLayer))
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
            } else if (placeable.item && heldItem)
            {
                ItemSO placeableItemSO = placeable.item.GetComponent<ItemSOHolder>().itemSO;
                ItemSO heldItemSO = heldItem.GetComponent<ItemSOHolder>().itemSO;
                ItemSO outputItem = recipeSystem.GetRecipeOutput(placeableItemSO, heldItemSO);
                if (outputItem != null)
                {
                    if(placeableItemSO.isDestructable && !heldItemSO.isDestructable)
                    {
                        Destroy(placeable.item);
                        GameObject newItem = Instantiate(outputItem.prefab, new Vector3(0, 0, 0), outputItem.prefab.transform.rotation);
                        newItem.transform.SetParent(placeable.itemPos.transform);
                        placeable.item = newItem;
                        placeable.item.transform.localPosition = Vector3.zero;
                    } else if(!placeableItemSO.isDestructable && heldItemSO.isDestructable)
                    {
                        Destroy(heldItem);
                        heldItem = null;

                        GameObject newItem = Instantiate(outputItem.prefab, new Vector3(0, 0, 0), outputItem.prefab.transform.rotation);
                        newItem.transform.SetParent(heldItemPos.transform);
                        heldItem = newItem;
                        heldItem.transform.localPosition = Vector3.zero;
                    } else
                    {
                        Destroy(placeable.item);
                        GameObject newItem = Instantiate(outputItem.prefab, new Vector3(0,0,0), outputItem.prefab.transform.rotation);
                        newItem.transform.SetParent(placeable.itemPos.transform);
                        placeable.item = newItem;
                        placeable.item.transform.localPosition = Vector3.zero;
                        Destroy(heldItem);
                        heldItem = null;
                    }      
                }
            }
        }
    }

    private void Interact()
    {
        if(hitObject)
        {
            if(hitObject.TryGetComponent(out Table table))
            {
                if (table.order == null)
                    table.TakeOrder();
            }
        }
    }

    public void Move()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        Vector3 moveDir = new Vector3(move.x, 0f, move.y );

        Vector3 forwardRelativeVerticalInput = move.y * forward;
        Vector3 rightRelativeHorizontalInput = move.x * right;

        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput;

        rb.AddForce(cameraRelativeMovement * speed, ForceMode.Force);


        if (move != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(cameraRelativeMovement, Vector3.up);

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
