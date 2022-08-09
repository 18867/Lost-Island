using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;


public class SaveLoadManager : Singleton<SaveLoadManager>
{
    private string jsonFolder;
    private List<ISavable> savableList = new List<ISavable>();//游戏中会有多个savable的数据

    private Dictionary<string, GameSaveData> saveDataDict = new Dictionary<string, GameSaveData>();


    protected override void Awake()
    {
        base.Awake();
        jsonFolder = Application.persistentDataPath + "/SAVE/";
    }


    private void OnEnable()
    {
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    private void OnStartNewGameEvent(int obj)
    {
        var resultPath = jsonFolder + "data.sav";
        if (File.Exists(resultPath))
        {
            File.Delete(resultPath);
        }
    }

    public void Register(ISavable savable)
    {
        savableList.Add(savable);
    }

    public void Save()
    {
        saveDataDict.Clear();

        foreach(var savable in savableList)
        {
            saveDataDict.Add(savable.GetType().Name, savable.GenerateSaveData());
        }

        var resultPath = jsonFolder + "data.sav";

        var jsonData = JsonConvert.SerializeObject(saveDataDict, Formatting.Indented);     
        
        if (!File.Exists(resultPath))
        {
            Directory.CreateDirectory(jsonFolder);//创建文件夹
        }

        File.WriteAllText(resultPath, jsonData);

    }


    public void Load()
    {
        var resultPath = jsonFolder + "data.sav";

        if (!File.Exists(resultPath)) return;

        var stringData = File.ReadAllText(resultPath);

        var jsonData = JsonConvert.DeserializeObject<Dictionary<string, GameSaveData>>(stringData);//反序列化生成一个字典
    
        foreach (var savable in savableList)
        {
            savable.RestoreGameData(jsonData[savable.GetType().Name]);
        }
    
    }
}
