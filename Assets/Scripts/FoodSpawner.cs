using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public List<GameObject> foodToSpawn;
    public float maxDelayBetweenPickupAndSpawn = 10f;
    public float minDelayBetweenPickupAndSpawn = 1f;

    private float timeBeforeSpawn = 0.0f;
    private bool itemIsPresent = false;

    private void Start()
    {
        RefreshTimeBeforeSpawn();
    }

    private void RefreshTimeBeforeSpawn()
    {
        timeBeforeSpawn = Random.Range(minDelayBetweenPickupAndSpawn, maxDelayBetweenPickupAndSpawn);
    }

    private void SpawnFood()
    {
        GameObject toSpawn = foodToSpawn[Random.Range(0, foodToSpawn.Count - 1)];
        Vector3 positionToSpawn = transform.TransformPoint(Vector3.zero);
        positionToSpawn.z = -1;
        Instantiate(toSpawn, positionToSpawn, Quaternion.identity);
        itemIsPresent = true;
    }

    public void OnPickupCallback()
    {
        itemIsPresent = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (itemIsPresent) {
            return;
        }
        timeBeforeSpawn -= Time.deltaTime;
        if (timeBeforeSpawn <= 0)
        {
            RefreshTimeBeforeSpawn();
            SpawnFood();
        }
    }
}
