using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionData : ScriptableObject
{
    #region Variables
    
    #endregion Variables

    public abstract void LaunchAction(Player player);
    public abstract void LaunchAction(Target target, Player player);

}
