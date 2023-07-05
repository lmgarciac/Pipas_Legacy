﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] GameObject dog;

    //public float movementSpeed = 3;
    //public float jumpForce = 300;
    //public float timeBeforeNextJump = 1.2f;

    //private float canJump = 0f;
    private Animator characterAnimator;
    private Animator dogAnimator;

    public event Action<float> OnPlayerMovement;
    //Rigidbody rb;

    
    void Start()
    {
        characterAnimator = character.GetComponent<Animator>();
        dogAnimator = dog.GetComponent<Animator>();

        //rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ControlPlayer();
    }

    private void ControlPlayer()
    {
        if (Input.GetKeyDown(KeyCode.A))
            OnPlayerMovement?.Invoke(-1);
        if (Input.GetKeyDown(KeyCode.D))
            OnPlayerMovement?.Invoke(1);
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
}