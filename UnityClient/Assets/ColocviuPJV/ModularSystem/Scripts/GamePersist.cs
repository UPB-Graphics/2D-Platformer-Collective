using System;
using System.Linq;
using System.IO;
using UnityEngine;

public class GamePersist : MonoBehaviour
{

    GameData _gameData = new GameData();
    Player _player;
    private void Start()
    {
        var _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (_player == null)
        {
            Debug.Log(_player);
            _player = FindObjectOfType<Player>();
        }
    }

    // ##################################################
    // save on exit and load on startup

    // void OnDisable()
    // {
    //     Save();
    // }

    // void Start()
    // {
    //     Load();
    // }

    // ##################################################

    public void Save(int gameNumber)
    {

        // Debug.Log(_player);
        // Debug.Log(_gameData);

        _gameData.MoneyDatas.Clear();

        // find all objects of type Money
        var objs = FindObjectsOfType<Money>(includeInactive: true);

        // loop thru them 
        foreach (var money in objs)
        {
            //save
            _gameData.MoneyDatas.Add(money.MoneyData);
        }

        _gameData.Money = _player.Money;
        _gameData.PlayerPosition = _player.transform.position;
        _gameData.PlayerVelocity = _player.GetComponent<Rigidbody>().velocity;

        // serialize the game data
        var json = JsonUtility.ToJson(_gameData);

        // save to PlayerPrefs
        // PlayerPrefs.SetString("GameData" + gameNumber, json);

        // save to file system instead of PlayerPrefs
        using (StreamWriter streamWriter = new StreamWriter($"SaveGame{gameNumber}.json"))
        {
            streamWriter.Write(json);
        }
    }

    public void Load(int gameNumber)
    {
        // load from file system 
        using (StreamReader streamReader = new StreamReader($"SaveGame{gameNumber}.json"))
        {
            // read json data from file 
            var json = streamReader.ReadToEnd();

            // load data from PlayerPrefs
            // string json = PlayerPrefs.GetString("GameData" + gameNumber);

            // load data into _gameData
            _gameData = JsonUtility.FromJson<GameData>(json);

            // find all objects of type Money
            var objs = FindObjectsOfType<Money>(includeInactive: true);

            // loop thru them 
            foreach (var money in objs)
            {
                //load
                var moneyData = _gameData.MoneyDatas.FirstOrDefault(t => t.Name == money.name);
                money.Load(moneyData);
            }

            // load player data
            _player.transform.position = _gameData.PlayerPosition;
            _player.GetComponent<Rigidbody>().velocity = _gameData.PlayerPosition;
            _player.Money = _gameData.Money;

        }

    }

}

