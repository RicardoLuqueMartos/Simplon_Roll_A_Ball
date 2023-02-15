using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    float explosionForce = 1000f;
    float delay = 2;
    bool Exploded = false;

    public float _explosionForce { get { return explosionForce; } }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.transform.GetComponent<Player>();
        if (player != null)
        {
            Rigidbody otherRigidBody = collision.transform.GetComponent<Rigidbody>();
            if (Exploded == false) { 
            otherRigidBody.AddExplosionForce(_explosionForce, collision.contacts[0].point, 5);
                Exploded = true;
            //    Invoke("DestroySelf", delay);
            }
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
