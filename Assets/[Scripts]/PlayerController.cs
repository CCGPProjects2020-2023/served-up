using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputActions input;
    Vector2 move = Vector2.zero;
    public float speed = 10;
    Rigidbody rb;
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

    private void Pickup()
    {
        throw new NotImplementedException();
    }

    private void Interact()
    {
        throw new NotImplementedException();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        SpeedControl();
    }

    public void Move()
    {
        Vector3 moveDir = transform.forward * move.y + transform.right * move.x;
        rb.AddForce(moveDir * speed, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 vel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (vel.magnitude > speed)
        {
            Vector3 controlVel = vel.normalized * speed;

            rb.velocity = new Vector3(controlVel.x, rb.velocity.y, controlVel.z);
        }
    }
}
