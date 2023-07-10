using System.Collections.Generic;
using UnityEngine;

public class LaneController : MonoBehaviour
{
    [SerializeField] Color outlineColor = Color.red;
    [SerializeField] Transform laneTransform;
    [SerializeField] Transform spawnPointPrefab;

    public Transform startPoint;

    private List<Transform> spawnPointTransforms = new List<Transform>();
    private Dictionary<int, SpawnPointPrefabs> interactables = new Dictionary<int, SpawnPointPrefabs>();

    //Plane Components
    private MeshFilter meshFilter;
    private Plane lanePlane;
    private Bounds meshBounds;
    private Vector3 planeCenter;
    private Vector3 planeSize;

    private int obstacleOffset = 4;


    private void Start()
    {
        lanePlane = new Plane(laneTransform.up, laneTransform.position);
        meshFilter = laneTransform.GetComponent<MeshFilter>();
        meshBounds = meshFilter.sharedMesh.bounds;
        planeCenter = laneTransform.position;
        planeSize = new Vector3(meshBounds.size.x * laneTransform.localScale.x, 0f, meshBounds.size.z * laneTransform.localScale.z);

        InitSpawnPoints();

        SpawnInteractables();
    }

    private void SpawnInteractables()
    {
        foreach (var spawnPointTransform in spawnPointTransforms)
        {
            SpawnPoint spawnPoint = new SpawnPoint();

            if (spawnPointTransform.TryGetComponent<SpawnPoint>(out spawnPoint))
                spawnPoint.SpawnRandomElement();
        }    
    }

    private void InitSpawnPoints()
    {
        Bounds meshBounds = meshFilter.sharedMesh.bounds;
        planeSize = new Vector3(meshBounds.size.x * laneTransform.localScale.x, 0f, meshBounds.size.z * laneTransform.localScale.z);
        int laneLenght = (int)Mathf.Floor(planeSize.z) - obstacleOffset*2;

        int halfLaneLenght = (int) (laneLenght * 0.5f);
        int spawnPointAmount = Random.Range(0, 5);

        List<int> randomPositions = MathUtilities.GetUniqueRandomIntegerList(-halfLaneLenght,halfLaneLenght,spawnPointAmount);

        foreach (var position in randomPositions)
        {
            Transform spawnPoint = Instantiate(spawnPointPrefab, this.transform);
            spawnPoint.position = new Vector3(transform.position.x, transform.position.y, position);
            spawnPointTransforms.Add(spawnPoint);
        }
    }

    private void OnDrawGizmos()
    {
        DrawPlaneOutline();
    }

    private void OnDrawGizmosSelected()
    {
        DrawPlaneOutline();
    }

    private void DrawPlaneOutline()
    {       
        if (meshFilter == null || meshFilter.sharedMesh == null)
        {
            Debug.LogWarning("MeshFilter or Mesh not found!");
            return;
        }

        Vector3[] points = new Vector3[4];
        points[0] = planeCenter + laneTransform.right * planeSize.x * 0.5f + laneTransform.forward * planeSize.z * 0.5f;
        points[1] = planeCenter + laneTransform.right * planeSize.x * 0.5f - laneTransform.forward * planeSize.z * 0.5f;
        points[2] = planeCenter - laneTransform.right * planeSize.x * 0.5f - laneTransform.forward * planeSize.z * 0.5f;
        points[3] = planeCenter - laneTransform.right * planeSize.x * 0.5f + laneTransform.forward * planeSize.z * 0.5f;

        Gizmos.color = outlineColor;
        for (int i = 0; i < 4; i++)
        {
            Gizmos.DrawLine(points[i], points[(i + 1) % 4]);
        }

        foreach (Transform spawnPoint in spawnPointTransforms)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(spawnPoint.position, new Vector3(0.5f, 0.5f, 0.5f));
        }
    }
}
