using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Enemy Create Info")]
    public List<Transform> points = new();
    public List<GameObject> monsterPool = new();
    public PoolableMono monster;
    public float createTime = 2.0f;
    public int maxMonster = 10;

    private void Awake()
    {
        PoolManager.Instance = new PoolManager(this.transform);
        PoolManager.Instance.CreatePool(monster, maxMonster);
    }
    private void Start()
    {

        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;
        //SpawnPointGroup �ڽĵ� ��� ������ 
        foreach (Transform point in spawnPointGroup)
        {
            points.Add(point);

        }
        //CreateMonsterPool();
        InvokeRepeating("CreateMonster", 2, createTime);

        HideCursor(true);
    }
    private void HideCursor(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !state;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideCursor(false);
        }
        if (Input.GetMouseButton(1))
        {

            HideCursor(true);
        }
    }
    public void CreateMonster()
    {
        PoolManager.Instance.Pop(monster.gameObject.name);
    }
    /* public void CreateMonsterPool()
     {
         for (int i = 0; i < maxMonster; i++)
         {
             var _monster = Instantiate<GameObject>(monster);

             _monster.name = $"Monster_{i:00}";

             _monster.SetActive(false);
             Debug.Log("Weqr");
             monsterPool.Add(_monster);
         }
     }
     public void CreateMonster()
     {
         int idx = Random.Range(0, points.Count);

         GameObject _monster = GetMonsterInPool();

         _monster?.transform.SetPositionAndRotation(points[idx].position, points[idx].rotation);

         _monster?.SetActive(true);
     }

     private GameObject GetMonsterInPool()
     {
         foreach (var monster in monsterPool)
         {
             if (monster.activeSelf == false) //��밡���� ������Ʈ���� ������̸� SetActive�� true
                 return monster;
         }
         return null;
     }*/
}
