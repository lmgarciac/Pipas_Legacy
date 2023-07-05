using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneController : MonoBehaviour
{
    [SerializeField] Color outlineColor = Color.red;
    [SerializeField] Transform laneTransform;
    
    public Transform startPoint;

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
        MeshFilter meshFilter = laneTransform.GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.sharedMesh == null)
        {
            Debug.LogWarning("MeshFilter or Mesh not found!");
            return;
        }

        Plane plane = new Plane(laneTransform.up, laneTransform.position);
        Bounds meshBounds = meshFilter.sharedMesh.bounds;
        Vector3 planeCenter = laneTransform.position;
        Vector3 planeSize = new Vector3(meshBounds.size.x * laneTransform.localScale.x, 0f, meshBounds.size.z * laneTransform.localScale.z);

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
    }
}
