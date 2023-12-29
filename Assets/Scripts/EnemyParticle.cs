using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyParticle : MonoBehaviour
{
    [SerializeField] private UnityEvent enemyParticleHit;

    // You can add any actions or effects you want here
    void OnParticleCollision(GameObject other)
    {
        // Check if the collision is with the player
        if (other.CompareTag("Player"))
        {
            // Perform some action when the particle collides with the player
            // For example, you can call a method on the player script to handle the hit
            enemyParticleHit.Invoke();
        }
    }
}
