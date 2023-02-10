using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataCreator : MonoBehaviour
{

    [MenuItem("Window/My Game/Data Creator/Create Level Data")]
    public static void CreateLevelData()
    {
        LevelData newData = ScriptableObject.CreateInstance("LevelData") as LevelData;
        newData.name = "(rename me) New Level Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Levels/" + newData.name + ".asset";
        AssetDatabase.CreateAsset(newData, path);
    }

    [MenuItem("Window/My Game/Data Creator/Create Action Data")]
    public static void CreateActionData()
    {
        ActionData newData = ScriptableObject.CreateInstance("ActionData") as ActionData;
        newData.name = "(rename me) New Action Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Actions/" + newData.name + ".asset";
        AssetDatabase.CreateAsset(newData, path);
    }

    [MenuItem("Window/My Game/Data Creator/Create Ennemy Data")]
    public static void CreateEnnemyData()
    {
        EnnemyData newData = ScriptableObject.CreateInstance("EnnemyData") as EnnemyData;
        newData.name = "(rename me) New Ennemy Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Ennemies/" + newData.name + ".asset";
        AssetDatabase.CreateAsset(newData, path);
    }

    [MenuItem("Window/My Game/Data Creator/Create Obstacle Data")]
    public static void CreateObstacleData()
    {
        ObstacleData newData = ScriptableObject.CreateInstance("ObstacleData") as ObstacleData;
        newData.name = "(rename me) New Obstacle Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Obstacles/" + newData.name + ".asset";
        AssetDatabase.CreateAsset(newData, path);
    }

    [MenuItem("Window/My Game/Data Creator/Create Bumper Data")]
    public static void CreateBumperData()
    {
        BumperData newData = ScriptableObject.CreateInstance("BumperData") as BumperData;
        newData.name = "(rename me) New Bumper Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Bumpers/" + newData.name + ".asset";
        AssetDatabase.CreateAsset(newData, path);
    }

    [MenuItem("Window/My Game/Data Creator/Create Spawn Data")]
    public static void CreateSpawnData()
    {
        SpawnData newData = ScriptableObject.CreateInstance("SpawnData") as SpawnData;
        newData.name = "(rename me) New Spawn Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Actions/" + newData.name + ".asset";
        AssetDatabase.CreateAsset(newData, path);
    }
}
