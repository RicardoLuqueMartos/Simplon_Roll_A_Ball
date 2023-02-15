using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    ActionData action;
    public ActionData _action { get { return action; } }

    public enum SpawnModeEnum { OnTargetObject, OneBySpawnerObject, OneOnRandomSpawnerObject }
    [SerializeField]
    SpawnModeEnum spawnMode;
    public SpawnModeEnum _spawnMode { get { return spawnMode; } }

    public List<SpawnerObject> SpawnerObjectsList = new List<SpawnerObject>();
}
