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
    private Vector2 currentLookDir;
    // Start is called before the first frame update
    private void Awake()
    {
        currentLookDir = new Vector2(0f,0f);
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
        Vector3 moveDir = new Vector3(move.x, 0f, move.y);
        //Vector3 moveDir = transform.forward * move.y + transform.right * move.x;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.5f);
        currentLookDir = new Vector2(0f, transform.rotation.y);

        //transform.Translate(moveDir * speed * Time.deltaTime, Space.World);

        rb.AddForce(moveDir * speed, ForceMode.Force);
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
