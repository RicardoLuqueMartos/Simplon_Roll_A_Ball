using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public BumperData bumperData;
    public Healthbar healthbar;

    [SerializeField]
    int destroyAmount = 0;
    public int _destroyAmount { get { return destroyAmount; } }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.transform.GetComponent<Player>();
        if (player != null)
        {
            Rigidbody otherRigidBody = collision.transform.GetComponent<Rigidbody>();

            if (bumperData == null) DestroySelf();
            else
            {
                otherRigidBody.AddExplosionForce(bumperData._bumpForce, collision.contacts[0].point, 5);

                if (bumperData != null && bumperData._destroyType == ObjectData.DestroyTypeEnum.ContactAmount)
                {
                    destroyAmount = destroyAmount + 1;
                    if (bumperData._destroyAmount <= destroyAmount)
                        DestroySelf();
                    else if (healthbar != null)
                        healthbar.UpdateHealthBar(destroyAmount);
                }
                else if (bumperData == null) DestroySelf();
            }
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
