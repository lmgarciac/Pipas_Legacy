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

    private bool switchingLanes = false;

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

        playerController = playerGameObject.GetComponent<PlayerController>();

        cameraBehavior.player = playerController.dog;

        playerController.OnPlayerMovement += SwitchLanes;
    }

    private void SwitchLanes(float value)
    {          
        if (value < 0 && currentLaneIndex > 0 && !switchingLanes)
        {
            switchingLanes = true;
            StartCoroutine(SwitchLanesLerp(true, lanes[currentLaneIndex - 1].transform.position.x, 0.2f));
        }
        else if (value > 0 && currentLaneIndex < lanes.Length - 1 && !switchingLanes)
        {
            switchingLanes = true;
            StartCoroutine(SwitchLanesLerp(false, lanes[currentLaneIndex + 1].transform.position.x, 0.2f));
        }
    }

    private IEnumerator SwitchLanesLerp(bool switchLeft, float targetPosition, float duration)
    {
        float elapsedTime = 0f;
        float initialPosition = playerGameObject.transform.position.x;
        float lerpValue = initialPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            lerpValue = Mathf.Lerp(initialPosition, targetPosition, t);

            //Debug.Log($"CurrentPosition = {lerpValue}");

            playerGameObject.transform.position = new Vector3(lerpValue,
                                                  playerGameObject.transform.position.y,
                                                  playerGameObject.transform.position.z);
            yield return null;
        }

        //Debug.Log("Finalizado");

        if (switchLeft)
        {
            currentLaneIndex--;
        }
        else
        {
            currentLaneIndex++;
        }

        currentLane = lanes[currentLaneIndex];
        switchingLanes = false;

        yield return null;
    }
}
