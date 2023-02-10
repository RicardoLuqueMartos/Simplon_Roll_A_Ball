using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpawnData : ActionData
{
    #region Variables
    [SerializeField]
    ObjectData objectToSpawn;
    public ObjectData _objectToSpawn { get { return objectToSpawn; } }
    #endregion Variables

    public enum SpawnPositionEnum { TargetObject }
    [SerializeField]
    SpawnPositionEnum spawnPosition;
    public SpawnPositionEnum _spawnPosition { get { return spawnPosition; } }

    [SerializeField]
    Vector3 positionOffset;
    public Vector3 _positionOffset { get { return positionOffset; } }

    public override void LaunchAction()
    {
        Acting();
    }

    void Acting()
    {
        GameObject newObj = (GameObject)Instantiate(objectToSpawn._prefab);

    }

    public override void LaunchAction(Target target)
    {
        if (target != null)
        {
            if (spawnPosition == SpawnPositionEnum.TargetObject)            
                Acting(target);
        }       
    }

    void Acting(Target target)
    {

        if (spawnPosition != SpawnPositionEnum.TargetObject)
        {
            Vector3 pos = target.transform.position + positionOffset;

            GameObject newObj = (GameObject)Instantiate(objectToSpawn._prefab, pos, target.transform.rotation, null);
            Bumper bumper = newObj.GetComponent<Bumper>();
            if (bumper != null)
            {
                bumper.bumperData = objectToSpawn as BumperData;

                if (bumper.bumperData._destroyType == ObjectData.DestroyTypeEnum.Timer)
                {
                    DestroyMeByTimer timer = newObj.AddComponent<DestroyMeByTimer>();
                    timer.SetDelay(bumper.bumperData._destroyAmount);
                }

            }
        }
    }

}
