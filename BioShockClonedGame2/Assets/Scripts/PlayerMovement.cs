using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 9f;

    Vector3 velocity;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    private bool lerpCrouch;
    private bool crouching;
    public float crouchTimer;

    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt( jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
        {
            speed = 4f;
        } else if (Input.GetKeyUp(KeyCode.LeftControl) && isGrounded)
        {
            speed = 9f;
        }

        if (Input.GetKeyDown(KeyCode.C) && isGrounded)
        {
            
                crouching = true;
                
                controller.height = 2;
            
            
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            crouching = false;

            controller.height = Mathf.Lerp(controller.height, 3, 0.5f);
        }

    }

   
}
