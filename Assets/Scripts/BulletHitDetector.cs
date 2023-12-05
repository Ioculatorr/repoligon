using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitDetector : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the bullet layer
        if (other.gameObject.layer == 9)
        {
            // The collider belongs to the bullet layer
            // You can add your logic here for handling the bullet hit
            // For example, you might decrease the enemy's health or destroy the enemy
            Debug.Log("Enemy Hit by Bullet!");

            // Optional: Destroy the bullet upon hitting the enemy
            Time.timeScale = 0.1f;
        }
    }
}
