using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMeByTimer : MonoBehaviour
{
    [SerializeField]
    float Delay = 1.5f;

    public void SetDelay (int delay) { Delay = delay; }

    private void OnEnable()
    {
        Invoke("DestroySelf", Delay);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
