using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    ActionData action;
    public ActionData _action { get { return action; } }

}
