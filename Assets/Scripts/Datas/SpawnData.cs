using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class SpawnData : ActionData
{
    #region Variables
    [SerializeField]
    ObjectData objectToSpawn;
    public ObjectData _objectToSpawn { get { return objectToSpawn; } }
    #endregion Variables

  /*  public enum SpawnPositionEnum { TargetObject }
    [SerializeField]
    SpawnPositionEnum spawnPosition;
    public SpawnPositionEnum _spawnPosition { get { return spawnPosition; } }
  */
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
            Acting(target);
        }       
    }

    void Acting(Target target)
    {
        Vector3 pos = new Vector3();
        Quaternion rot = new Quaternion();

        if (target._spawnMode == Target.SpawnModeEnum.OnTargetObject)
        {
            pos = target.transform.position + positionOffset;
            rot = target.transform.rotation;
        }
        else if (target._spawnMode == Target.SpawnModeEnum.OneBySpawnerObject)
        {
            for (int i = 0; i < target.SpawnerObjectsList.Count; i++)
            {
                pos = target.SpawnerObjectsList[i].transform.position + positionOffset;
                rot = target.SpawnerObjectsList[i].transform.rotation;
            }
        }
        else if (target._spawnMode == Target.SpawnModeEnum.OneOnRandomSpawnerObject)
        {
            int ran = UnityEngine.Random.Range(0, target.SpawnerObjectsList.Count);

            pos = target.SpawnerObjectsList[ran].transform.position + positionOffset;
            rot = target.SpawnerObjectsList[ran].transform.rotation;
        }
        SpawnObject(pos, rot);
    }

    void SpawnObject(Vector3 pos, Quaternion rot)
    {
        GameObject newObj = (GameObject)Instantiate(objectToSpawn._prefab, pos, rot, null);
        Bumper bumper = newObj.GetComponent<Bumper>();
        Obstacle obstacle = newObj.GetComponent<Obstacle>();

        if (bumper != null)
        {
            bumper.bumperData = objectToSpawn as BumperData;

            if (bumper.bumperData._destroyType == ObjectData.DestroyTypeEnum.Timer)
            {
                DestroyMeByTimer timer = newObj.AddComponent<DestroyMeByTimer>();
                timer.SetDelay(bumper.bumperData._destroyAmount);
                if (bumper.healthbar != null) {
                    timer.healthbar = bumper.healthbar;
                    timer.healthbar.enabled = true;
                    timer.LaunchTimer();
                }
            }
            if (bumper.healthbar != null)
            {
                bumper.healthbar.maxHealthPoints = bumper.bumperData._destroyAmount;
                bumper.healthbar.enabled = true;
            }
        }

        else if (obstacle != null)
        {
            obstacle.obstacleData = objectToSpawn as ObstacleData;

            if (obstacle.obstacleData._destroyType == ObjectData.DestroyTypeEnum.Timer)
            {
                DestroyMeByTimer timer = newObj.AddComponent<DestroyMeByTimer>();
                timer.SetDelay(obstacle.obstacleData._destroyAmount);
            }

        }
    }
}
