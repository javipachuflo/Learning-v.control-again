using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody rb;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float acceleration = 20f;
    [SerializeField] float deceleration = 10f;
    [SerializeField] float fallMultiplier = 2.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 targetVelocity = new Vector3(moveInput.x, 0, moveInput.y) * maxSpeed;
        Vector3 velocity = rb.linearVelocity;
        Vector3 velocityChange;

        if (moveInput.magnitude > 0.01f)
        {
            // Accelerate towards target velocity
            velocityChange = Vector3.MoveTowards(
                new Vector3(velocity.x, 0, velocity.z),
                new Vector3(targetVelocity.x, 0, targetVelocity.z),
                acceleration * Time.deltaTime
            ) - new Vector3(velocity.x, 0, velocity.z);
        }
        else
        {
            // Decelerate to zero
            velocityChange = Vector3.MoveTowards(
                new Vector3(velocity.x, 0, velocity.z),
                Vector3.zero,
                deceleration * Time.deltaTime
            ) - new Vector3(velocity.x, 0, velocity.z);
        }

        rb.AddForce(velocityChange, ForceMode.VelocityChange);


        // jump stuff
        if (rb.linearVelocity.y < 0 /*i.e. you are falling*/)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime; // increased-gravity fall
        }

        //Debug.Log(moveInput); // shows in the console the input (yes, it's normalised)
    }

    void OnJump(InputValue inputValue) {

        if (inputValue.isPressed) { 
            Debug.Log("Jumped");

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpPower, rb.linearVelocity.z); // normal jump
        }
    }

    void OnMove(InputValue inputValue) {
        moveInput = inputValue.Get<Vector2>(); // makes the variable moveInput have the value of the input(key Pressed)

    }
}
