using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject player;
    private Vector3 deltaPosition;
    private Vector3 playerPosition;
    // Update is called once per frame

    private void Start()
    {
        deltaPosition = this.transform.position;
    }
    void Update()
    {
        if (this.player != null)
            FollowPlayer();
    }

    private void FollowPlayer()
    {
        playerPosition = player.transform.position;
        this.transform.position = deltaPosition + player.transform.position;
    }
}
