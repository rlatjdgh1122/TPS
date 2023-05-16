using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, Iitem
{
    public int Score = 10; 
    public void Use(GameObject target)
    {
        GameManager.Instance.AddScore(Score);
        Destroy(gameObject);
    }
}
