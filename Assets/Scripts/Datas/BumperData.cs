using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperData : ObjectData
{
    #region Variables
    [SerializeField]
    float bumpForce = 10f;
    public float _bumpForce { get { return bumpForce; } }
      
  

    

    #endregion Variables
}
