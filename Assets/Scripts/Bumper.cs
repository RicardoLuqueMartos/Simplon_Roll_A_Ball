using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{  
    public BumperData bumperData;

    [SerializeField]
    int destroyAmount = 0;
    public int _destroyAmount { get { return destroyAmount; } }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.transform.GetComponent<Player>();
        if (player != null)
        {
            Rigidbody otherRigidBody = collision.transform.GetComponent<Rigidbody>();

            otherRigidBody.AddExplosionForce(bumperData._bumpForce, collision.contacts[0].point, 5);

            if (bumperData._destroyType == ObjectData.DestroyTypeEnum.ContactAmount)
            {
                if (bumperData._destroyAmount == destroyAmount)
                    DestroySelf();
                else destroyAmount = destroyAmount + 1;
            }
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
