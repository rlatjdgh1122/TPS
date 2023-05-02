using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;
    public Transform playerTransform;

    public float maxDistance = 5f;

    public float timeBetSpawnMax = 7;
    public float timeBetSpawnMin = 2f;
    private float timeBetSpawn;

    private float lastSpawnTime;

    private void Start()
    {
        timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
        lastSpawnTime = 0;
    }
    private void Update()
    {
        if (Time.time >= timeBetSpawn + lastSpawnTime && playerTransform != null)
        {
            lastSpawnTime = Time.time;

            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            Spawn();
        }
    }
    private void Spawn()
    {
        Vector3 spawnPosition =
            GetRandomPointOnNavMesh(playerTransform.position, maxDistance);

        spawnPosition += Vector3.up * .5f;

        GameObject selectedItem = items[Random.Range(0, items.Length)];
        GameObject item = Instantiate(selectedItem, spawnPosition, Quaternion.identity);

        Destroy(item, 5f);
    }

    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        Vector3 randomPos = center + Random.insideUnitSphere * distance;

        NavMeshHit hit;
        //distance 반경 안에서, randomPos에 가장 가까운 내비메시 위의 한 점을 찾음
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        return hit.position;
    }
}
