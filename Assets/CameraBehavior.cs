using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject player;
    private Vector3 deltaPosition;

    // Update is called once per frame

    private void Start()
    {
        deltaPosition = this.transform.position;
    }
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        this.transform.position = deltaPosition + player.transform.position;
    }
}
