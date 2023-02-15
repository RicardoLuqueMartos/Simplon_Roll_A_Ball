using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObject : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.GetComponent<Renderer>().enabled= false;
    }

}
