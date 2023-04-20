using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator animator;

    public float walkSpeed = 3.0f;
    public float sprintSpeed = 6.0f;
    public float turnSmoothing = 0.1f;
    float turnSmoothVelocity;

    float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private Vector3 velocity;

    // Update is called once per frame
    void Update()
    {

        move();
    }

    private void move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float speed = 0;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {

            float targetRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothing);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = sprintSpeed;
            }
            else
            {
                speed = walkSpeed;
            }
            print(speed);
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

        }

        animator.SetFloat("Speed", speed);

        applyGravity();
    }

    private void applyGravity()
    {
        velocity.y += gravity * gravityMultiplier * Time.deltaTime;
        controller.Move(velocity);
    }
}
