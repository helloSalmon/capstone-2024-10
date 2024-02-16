using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

[Serializable]
public class GameData
{
    public string playerId = "player";
}

/// <summary>
/// 게임 오브젝트의 생성과 소멸에 대한 로직을 수행하고 다이렉트로 게임 오브젝트에 접근할 수 있는 권한을 가지고 있는 매니저
/// </summary>
public class GameManagerEX
{
    GameObject _player;
    public Action<int> OnSpawnEvent;
    GameData _gameData = new GameData();

    public GameData SaveData { get { return _gameData; } set { _gameData = value; } }

    public void Init()
    {
        _path = Path.Combine(Application.persistentDataPath, "/SaveData.json");

        if (SaveData == null)
        {
            SaveData = new GameData();
        }
    }

    #region Spawn & Despawn
    private HashSet<int> _objectRegistry = new HashSet<int>();

    public int GenerateID()
    {
        int Id = UnityEngine.Random.Range(1, int.MaxValue);
        while (_objectRegistry.Contains(Id))
            Id = UnityEngine.Random.Range(1, int.MaxValue);
        _objectRegistry.Add(Id);
        return Id;
    }

    public void DeleteID(int id)
    {
        _objectRegistry.Remove(id);
    }
    #endregion


    #region Save & Load
    public string _path;

    public void SaveGame()
    {
        string jsonStr = JsonUtility.ToJson(Managers.GameMng.SaveData);
        File.WriteAllText(_path, jsonStr);
        Debug.Log($"Save Game Completed : {_path}");
    }

    public bool LoadGame()
    {
        if (File.Exists(_path) == false)
            return false;

        string fileStr = File.ReadAllText(_path);
        GameData data = JsonUtility.FromJson<GameData>(fileStr);
        if (data != null)
        {
            Managers.GameMng.SaveData = data;
        }

        Debug.Log($"Save Game Loaded : {_path}");
        return true;
    }

    public void Clear()
    {

    }
    #endregion
}
