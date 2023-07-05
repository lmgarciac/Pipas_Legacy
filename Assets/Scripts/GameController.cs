using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject level;
    [SerializeField] GameObject sectionPrefab;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] CameraBehavior cameraBehavior;


    private GameObject section;
    private LaneController[] lanes;
    private GameObject playerGameObject;
    private PlayerController playerController;
    private LaneController currentLane;
    private int currentLaneIndex;

    private void Awake()
    {

    }

    private void OnDisable()
    {
        playerController.OnPlayerMovement -= SwitchLanes;
    }

    void Start()
    {
        section = Instantiate(sectionPrefab, level.transform);
        lanes = section.GetComponentsInChildren<LaneController>();

        currentLaneIndex = Random.Range(0, lanes.Length);
        currentLane = lanes[currentLaneIndex];


        playerGameObject = Instantiate(playerPrefab, currentLane.startPoint.position, Quaternion.identity);
        cameraBehavior.player = playerGameObject;

        playerController = playerGameObject.GetComponent<PlayerController>();
        playerController.OnPlayerMovement += SwitchLanes;

    }

    private void SwitchLanes(float value)
    {      
        if (value < 0 && currentLaneIndex > 0)
        {
            currentLaneIndex--;
            currentLane = lanes[currentLaneIndex];
            playerGameObject.transform.position = currentLane.transform.position;
        }
        else if (value > 0 && currentLaneIndex < lanes.Length - 1)
        {
            currentLaneIndex++;
            currentLane = lanes[currentLaneIndex];
            playerGameObject.transform.position = currentLane.transform.position;
        }
    }
}
