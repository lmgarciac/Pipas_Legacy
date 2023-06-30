using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField] private GameObject sectionPrefab;
    [SerializeField] private GameObject firstSnapTo;
    [SerializeField] private GameObject lastSnapTo;

    [SerializeField]
    [Range(1, 1000)]
    private int lenght = 1;

    [SerializeField]
    [Range(0.1f, 0.3f)]
    private float sectionLenght = 0.21f;

  

    private void Start()
    {
        Generate();
    }


    private void Generate()
    {
        if (sectionLenght == 0)
            return;

        Vector3 sectionSpawnPosition = new Vector3();
        Vector3 sectionRotation = new Vector3(180, 0, 0);
        string sectionName = sectionPrefab.name;
        GameObject previousSection = new GameObject();

        int count = Mathf.RoundToInt(lenght / sectionLenght);

        for (int i = 0; i < count; i++)
        {
            sectionSpawnPosition.x = transform.position.x;
            sectionSpawnPosition.y = transform.position.y + sectionLenght * (i + 1);
            sectionSpawnPosition.z = transform.position.z;

            GameObject part = Instantiate(sectionPrefab, sectionSpawnPosition, Quaternion.identity, this.transform);
            part.transform.eulerAngles = sectionRotation;
            part.name = $"{sectionName}{i + 1}";

            FixedJoint partJoint = part.GetComponent<FixedJoint>();

            if (i == 0)
            {
                //Destroy(part.GetComponent<CharacterJoint>());
                partJoint.connectedBody = firstSnapTo.transform.GetComponent<Rigidbody>();
            }
            else
            {
                partJoint.connectedBody = previousSection.transform.GetComponent<Rigidbody>();
            }

            if (i == count - 1)
            {
                partJoint.connectedBody = lastSnapTo.transform.GetComponent<Rigidbody>();
            }

            previousSection = part;
        }
    }

    void Update()
    {
        
    }
}
