using System.Collections.Generic;
using UnityEngine;

public class LaneController : MonoBehaviour
{
    [SerializeField] Color outlineColor = Color.red;
    [SerializeField] Transform laneTransform;
    [SerializeField] Transform spawnPointPrefab;

    public Transform startPoint;

    private int spawnPointAmount;

    private List<Transform> spawnPoints = new List<Transform>();

    //Plane Components
    private MeshFilter meshFilter;
    private Plane lanePlane;
    private Bounds meshBounds;
    private Vector3 planeCenter;
    private Vector3 planeSize;


    private void Start()
    {
        lanePlane = new Plane(laneTransform.up, laneTransform.position);
        meshFilter = laneTransform.GetComponent<MeshFilter>();
        meshBounds = meshFilter.sharedMesh.bounds;
        planeCenter = laneTransform.position;
        planeSize = new Vector3(meshBounds.size.x * laneTransform.localScale.x, 0f, meshBounds.size.z * laneTransform.localScale.z);

        InitSpawnPoints();
    }

    private void InitSpawnPoints()
    {
        Bounds meshBounds = meshFilter.sharedMesh.bounds;
        planeSize = new Vector3(meshBounds.size.x * laneTransform.localScale.x, 0f, meshBounds.size.z * laneTransform.localScale.z);

        spawnPointAmount = Random.Range(0, 5);

        for (int i = 0; i < spawnPointAmount; i++)
        {
            int spawnPointPosition = Random.Range((int)(-planeSize.z * 0.5f), (int)(planeSize.z * 0.5f));
            Transform spawnPoint = Instantiate(spawnPointPrefab, this.transform);
            spawnPoint.position = new Vector3(transform.position.x, transform.position.y, spawnPointPosition);
            spawnPoints.Add(spawnPoint);
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

        foreach (Transform spawnPoint in spawnPoints)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(spawnPoint.position, new Vector3(0.5f, 0.5f, 0.5f));
        }
    }
}
