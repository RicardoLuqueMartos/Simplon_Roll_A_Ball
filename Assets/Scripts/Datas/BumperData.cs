using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperData : ObjectData
{
    #region Variables
    [SerializeField]
    float bumpForce = 10f;
    public float _bumpForce { get { return bumpForce; } }
      
    [SerializeField]
    int destroyAmount = 10;
    public int _destroyAmount { get { return destroyAmount; } }

    

    #endregion Variables
}
