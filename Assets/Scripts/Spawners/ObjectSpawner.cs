using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> itemToSpawn;
    public float maxDelayBetweenPickupAndSpawn = 10f;
    public float minDelayBetweenPickupAndSpawn = 1f;
    public bool spawnAtStart = false;
    public float rotateSpawnedObjectZ = 0f;

    protected GameObject spawnedGameObject;

    private float timeBeforeSpawn = 0.0f;
    protected bool itemIsPresent = false;

    private void Start()
    {
        RefreshTimeBeforeSpawn();
        if (spawnAtStart) {
            SpawnObject();
        }
    }

    private void RefreshTimeBeforeSpawn()
    {
        timeBeforeSpawn = Random.Range(minDelayBetweenPickupAndSpawn, maxDelayBetweenPickupAndSpawn);
    }

    protected virtual void SpawnObject()
    {
        GameObject toSpawn = itemToSpawn[Random.Range(0, itemToSpawn.Count - 1)];
        Vector3 positionToSpawn = transform.TransformPoint(Vector3.zero);
        positionToSpawn.z = -1;
        spawnedGameObject = Instantiate(toSpawn, positionToSpawn, Quaternion.Euler(0, 0, rotateSpawnedObjectZ));
        itemIsPresent = true;
    }

    protected void OnObjectUsed()
    {
        itemIsPresent = false;
        spawnedGameObject = null;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (itemIsPresent)
        {
            return;
        }
        timeBeforeSpawn -= Time.deltaTime;
        if (timeBeforeSpawn <= 0)
        {
            RefreshTimeBeforeSpawn();
            SpawnObject();
        }
    }
}
