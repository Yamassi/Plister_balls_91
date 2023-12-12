using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Newtonsoft.Json;

namespace Tretimi
{
    public class DataProvider : MonoBehaviour
    {
        public static void SaveDataJSON(SaveData saveData)
        {
            Debug.Log("SaveData");
            // string json = JsonUtility.ToJson(saveData);
            string json = JsonConvert.SerializeObject(saveData);
            File.WriteAllText(Application.persistentDataPath + "/SaveData.json", json);
        }
        public static SaveData LoadDataJSON()
        {
            SaveData data;
            if (File.Exists(Application.persistentDataPath + "/SaveData.json"))
            {
                string json = File.ReadAllText(Application.persistentDataPath + "/SaveData.json");
                // data = JsonUtility.FromJson<SaveData>(json);
                data = JsonConvert.DeserializeObject<SaveData>(json);
            }
            else
            {
                data = null;
            }

            return data;
        }
        public static void SaveData(SaveData data)
        {
            if (data != null)
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath
                  + "/Data.dat");

                bf.Serialize(file, data);
                file.Close();
                Debug.Log("Game data saved!");
            }
            else
                Debug.LogError($"Data not saved {data}");

        }
        public static SaveData LoadData()
        {
            if (File.Exists(Application.persistentDataPath
              + "/Data.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file =
                  File.Open(Application.persistentDataPath
                  + "/Data.dat", FileMode.Open);
                SaveData data = (SaveData)bf.Deserialize(file);
                file.Close();

                Debug.Log("Game data loaded!");
                return data;

            }
            else
            {
                Debug.LogError("There is no save data!");
                return null;
            }

        }
    }
}

