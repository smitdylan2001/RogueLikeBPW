using System;
using System.IO;
using UnityEngine;

[Serializable]
    public class SaveData
    {
        public int seed;
        public int floor;
        public int health;
        public int enemieskilled;
    }
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static int floor = 1;
    public int playerHealth = 5;
    public int damage = 2;

    public GameObject player;

    CreateBoard cb;
    
    internal SaveData saveData;

    private void Awake()
    {
        LoadSave();
    }

    void Start()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        GameObject.Find("GameManager").GetComponent<CreateBoard>();
        player.SetActive(true);
    }

    void Update()
    {
        if(playerHealth<= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    public void GetDamage(int damage)
    {
        playerHealth -= damage;
    }

    public void LoadSave()
    {
        if (File.Exists(Application.dataPath + "/saveData.json"))
        {
            string json = File.ReadAllText(Application.dataPath + "/saveData.json");
            if (json != null)
            {
            saveData = JsonUtility.FromJson<SaveData>(json);

            }
        }
        else
        {
            saveData = new SaveData();
        }
    }


    private void OnApplicationQuit()
    {
        SaveGame();
    }
    public void SaveGame()
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.dataPath + "/saveData.json", json);
    }
}
