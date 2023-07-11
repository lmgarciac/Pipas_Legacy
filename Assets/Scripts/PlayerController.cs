using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject character;
    public GameObject dog;

    [Header ("Player parameters")]
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float jumpForce = 50f;
    [SerializeField] private float groundRaycastDistance = 0.05f;
    [SerializeField] private float upwardGravityMultiplier = 1f;
    [SerializeField] private float fallingGravityMultiplier = 2f;

    private Animator characterAnimator;
    private Animator dogAnimator;

    private Rigidbody characterRb;
    private Rigidbody dogRb;

    public event Action<float> OnPlayerMovement;


    private bool isJumping = false;
    private bool isFalling = false;

    void Start()
    {
        if (character != null)
        {
            characterAnimator = character.GetComponent<Animator>();
            characterRb = character.GetComponent<Rigidbody>();
        }

        if (dog != null)
        {
            dogAnimator = dog.GetComponent<Animator>();
            dogRb = dog.GetComponent<Rigidbody>();
        }
    }


    void Update()
    {
        ControlPlayer();
        CharacterMoveTranslate();
    }


    private void CharacterMoveTranslate()
    {
        dog.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    private void CharacterMove()
    {
        //characterRb.velocity = Vector3.forward;
        dogRb.velocity = new Vector3(dogRb.velocity.x, dogRb.velocity.y, movementSpeed);


        //Vector3 movement = Vector3.forward * 1f;
        //characterRb.MovePosition(characterRb.position + movement * Time.deltaTime);
        //dogRb.MovePosition(dogRb.position + movement * Time.deltaTime);
    }

    private void ControlPlayer()
    {
        if (Input.GetKeyDown(KeyCode.D))
            OnPlayerMovement?.Invoke(-1);
        if (Input.GetKeyDown(KeyCode.A))
            OnPlayerMovement?.Invoke(1);
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            isFalling = false;

            isJumping = true;
            Jump();
        }
    }

    private void Jump()
    {
        dogRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    //void ControllPlayer()
    //{
    //    float moveHorizontal = Input.GetAxisRaw("Horizontal");
    //    float moveVertical = Input.GetAxisRaw("Vertical");

    //    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

    //    if (movement != Vector3.zero)
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
    //        anim.SetInteger("Walk", 1);
    //    }
    //    else {
    //        anim.SetInteger("Walk", 0);
    //    }

    //    transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);

    //    if (Input.GetButtonDown("Jump") && Time.time > canJump)
    //    {
    //            rb.AddForce(0, jumpForce, 0);
    //            canJump = Time.time + timeBeforeNextJump;
    //            anim.SetTrigger("jump");
    //    }
    //}

    private void FixedUpdate()
    {
        //CharacterMove();

        if (isJumping && dogRb.velocity.y < 0f)
        {
            // Player is falling after reaching the peak of the jump
            isJumping = false;
            isFalling = true;
        }

        if (isFalling)
        {
            // Apply custom gravity while falling
            dogRb.AddForce(Physics.gravity * fallingGravityMultiplier, ForceMode.Acceleration);
        }
        else
        {
            // Apply custom gravity while not jumping or falling
            dogRb.AddForce(Physics.gravity * upwardGravityMultiplier, ForceMode.Acceleration);
        }
    }

    private bool IsGrounded()
    {
        // Perform a raycast downwards to check if the player is on the ground
        return Physics.Raycast(dog.transform.position, -Vector3.up, groundRaycastDistance);
    }

    private void OnDrawGizmos()
    {
        // Draw a Gizmo to visualize the ground detection raycast
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(dog.transform.position, dog.transform.position - dog.transform.up * groundRaycastDistance);
    }
}