using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolingPair
{
    public PoolableMono Prefab;
    public int Count;
}

[CreateAssetMenu(menuName = "SO/PoolListSO")]
public class PoolListSO : ScriptableObject
{
    public List<PoolingPair> Pairs;
}
