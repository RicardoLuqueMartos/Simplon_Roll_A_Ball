using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : ScriptableObject
{
    #region Variables
    [SerializeField]
    GameObject prefab;
    public GameObject _prefab { get { return prefab; } }

    public enum PositionTypeEnum { Pickup }
    PositionTypeEnum positionType;
    public PositionTypeEnum _positionType { get { return positionType; } }

    public enum DestroyTypeEnum { None, Timer, ContactAmount }
    [SerializeField]
    DestroyTypeEnum destroyType;
    public DestroyTypeEnum _destroyType { get { return destroyType; } }

    [SerializeField]
    int destroyAmount = 10;
    public int _destroyAmount { get { return destroyAmount; } }
    #endregion Variables
}
