using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMeByTimer : MonoBehaviour
{
    [SerializeField]
    int Delay = 3;
    [SerializeField]
    int elapsed = 0;

    public Healthbar healthbar;

    public void SetDelay (int delay) { Delay = delay; }

    public void LaunchTimer()
    {
        elapsed = Delay;
        InvokeRepeating("UpdateTimer", 1, 1);
    }

    private void UpdateTimer()
    {
        if (elapsed <= Delay) 
        { 
            elapsed --;

            if (healthbar) healthbar.UpdateHealthBar(elapsed);
        }
        if (elapsed == 0)
            DestroySelf();
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
