﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using TMPro;

public class PenguinArea : Area
{

    [Tooltip("The Agent inside the area")]
    public PenguinAgent penguinAgent;
    [Tooltip("The baby penguine inside the area")]
    public GameObject penguinBaby;
    [Tooltip("The TextMeshPro text inside that shows the cumulative reward of the agent ")]
    public TextMeshPro cumulativeRewardText;
    [Tooltip("prefab of a live fish")]
    public Fish fishPrefab;
    private PenguinAcademy penguinAcademy;
    private List<GameObject> fishList;
    public override void ResetArea()
    {
        RemoveAllFish();
        PlacePenguin();
        PlaceBaby();
        SpawnFish(4, penguinAcademy.FishSpeed);
    }

    public void RemoveSpecificFish(GameObject fishObject)
    {
        fishList.Remove(fishObject);
        Destroy(fishObject);
    }

    //summmary
    //the number of fish remaining
    //summmary
    public int FishRemaining
    {
        get
        {
            return fishList.Count;
        }

    }
    public static Vector3 ChooseRandomPosition(Vector3 center, float minAngle, float maxAngle, float minRadius, float maxRadius)
    {
        float radius = minRadius;
        float angle = minAngle;
        if (maxRadius > minRadius)
        {
            //pick a random radius
            radius = UnityEngine.Random.Range(minRadius, maxRadius);

        }
        if (maxAngle > minAngle)
        {
            // Pick a random angle
            angle = UnityEngine.Random.Range(minAngle, maxAngle);
        }
        // Center position + forward vector rotated around the Y axis by "angle" degrees, multiplies by "radius"
        return center + Quaternion.Euler(0f, angle, 0f) * Vector3.forward * radius;
    }
    /// <summary>
    /// Remove all fish from the area
    /// </summary>
    private void RemoveAllFish()
    {
        if (fishList != null)
        {
            for (int i = 0; i < fishList.Count; i++)
            {
                if (fishList[i] != null)
                {
                    Destroy(fishList[i]);
                }
            }
        }

        fishList = new List<GameObject>();
    }
    /// <summary>
    /// Place the penguin in the area
    /// </summary>
    private void PlacePenguin()
    {
        Rigidbody rigidbody = penguinAgent.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        penguinAgent.transform.position = ChooseRandomPosition(transform.position, 0f, 360f, 0f, 9f) + Vector3.up * .5f;
        penguinAgent.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);

    }
    /// <summary>
    /// Place the baby in the area
    /// </summary>
    private void PlaceBaby()
    {
        Rigidbody rigidbody = penguinBaby.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        penguinBaby.transform.position = ChooseRandomPosition(transform.position, -45f, 45f, 4f, 9f) + Vector3.up * .5f;
        penguinBaby.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }
    /// <summary>
    /// Spawn some number of fish in the area and set their swim speed
    /// </summary>
    /// <param name="count">The number to spawn</param>
    /// <param name="fishSpeed">The swim speed</param>
    private void SpawnFish(int count, float fishSpeed)
    {
        for (int i = 0; i < count; i++)
        {
            // Spawn and place the fish
            GameObject fishObject = Instantiate<GameObject>(fishPrefab.gameObject);
            fishObject.transform.position = ChooseRandomPosition(transform.position, 100f, 260f, 2f, 13f) + Vector3.up * .5f;
            fishObject.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);

            // Set the fish's parent to this area's transform
            fishObject.transform.SetParent(transform);

            // Keep track of the fish
            fishList.Add(fishObject);

            // Set the fish speed
            fishObject.GetComponent<Fish>().fishSpeed = fishSpeed;
        }
    }
    // <summary>
    /// Called when the game starts
    /// </summary>
    private void Start()
    {
        penguinAcademy = FindObjectOfType<PenguinAcademy>();
        ResetArea();
    }
    /// <summary>
    /// Called every frame
    /// </summary>
    private void Update()
    {
        // Update the cumulative reward text
        cumulativeRewardText.text = penguinAgent.GetCumulativeReward().ToString("0.00");
    }

}


