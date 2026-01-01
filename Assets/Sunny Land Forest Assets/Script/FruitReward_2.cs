using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitReward : MonoBehaviour
{
    public int points = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(points);
            Destroy(gameObject);
        }
    }
}