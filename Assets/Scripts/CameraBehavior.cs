using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    //[SerializeField] private Transform levelTransform;

    [HideInInspector]
    public GameObject player;

    private Vector3 deltaPosition;
    private Vector3 playerPosition;

    private void Start()
    {
        //if (levelTransform != null)
        //    transform.position = new Vector3(levelTransform.position.x, transform.position.y, levelTransform.position.z);
    
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
        transform.position = new Vector3(transform.position.x, transform.position.y, playerPosition.z + 10f); 
//        transform.position = deltaPosition + playerPosition;
    }
}
