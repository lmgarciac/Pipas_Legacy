using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private List<SpawnPointPrefabs> prefabList = new List<SpawnPointPrefabs>();

    void Start()
    {
        if (prefabList.Count == 0)
            return;

        int elementIndex = Random.Range(0, prefabList.Count);
        GameObject element = prefabList[elementIndex].prefab;

        Instantiate(element, transform);
    }

    [System.Serializable]
    public class SpawnPointPrefabs
    {
        public SpawnPointElementType type;
        public GameObject prefab;
    }

    public enum SpawnPointElementType
    {
        Poop = 0,
        Trash
    }
}
