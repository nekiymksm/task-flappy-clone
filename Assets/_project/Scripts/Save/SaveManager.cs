using System.IO;
using UnityEngine;

namespace _project.Scripts.Save
{
    public class SaveManager
    {
        private static SaveManager _instance;
        
        private string _directoryPath;
        
        public SaveData SaveData { get; private set; }
        
        private SaveManager()
        {
            _directoryPath = Application.persistentDataPath + "/save/";

            if (Directory.Exists(_directoryPath) == false)
            {
                new DirectoryInfo(_directoryPath).Create();
                Save(new SaveData
                {
                    difficultyLevel = 0,
                    isMusicOn = true,
                    isSoundsOn = true,
                    lastRecord = 0,
                    lastDifficultyLevel = 0
                });
            }
            else
            {
                Load();
            }
        }
 
        public static SaveManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SaveManager();
            }
                
            return _instance;
        }

        public void Save(SaveData saveData)
        {
            SaveData = saveData;

            var saveDataText = JsonUtility.ToJson(SaveData);
            File.WriteAllText(_directoryPath + "/data.json", saveDataText);
        }

        private void Load()
        {
            var pathData = File.ReadAllText(_directoryPath + "/data.json");
            SaveData = JsonUtility.FromJson<SaveData>(pathData);
        }
    }
}