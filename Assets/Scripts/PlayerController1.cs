using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody rb;
    [SerializeField] float jumpPower = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(moveInput.x, 0, moveInput.y); // we can place a Vector3 straight into the method since that's the input it is waiting for (no need for external Vector3s plugged in)
    }

    void OnJump(InputValue inputValue) {

        if (inputValue.isPressed) { 
            Debug.Log("Jumped");
        }
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpPower, rb.linearVelocity.z);
    }

    void OnMove(InputValue inputValue) {
        moveInput = inputValue.Get<Vector2>(); // makes the variable moveInput have the value of the input(key Pressed)

    }
}
