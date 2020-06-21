using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public static int playerHealth = 5;
    public int damage = 2;

    public GameObject player;
    public Player playerScript;

    CreateBoard cb;
    
    internal SaveData saveData;

    //Text stats;
    TextMeshProUGUI stats;

    private void Awake()
    {
        LoadSave();
    }

    void Start()
    {
        //Check for other instance
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        //Get objects and scripts
        GameObject.Find("GameManager").GetComponent<CreateBoard>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        stats = GameObject.Find("Stats").GetComponent<TMPro.TextMeshProUGUI>();

        //Hide curson
        Cursor.visible = false;

        StartCoroutine(LateStart(.2f));
    }

    //Make sure the code does not break
    IEnumerator LateStart(float time)
    {
        yield return new WaitForSeconds(time);
        ChangeUI();
    }

    void Update()
    {
        if(playerHealth<= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //Change UI text to new values
    public void ChangeUI()
    {
        stats.text = "Floor: " + floor + "\nHealth: " + playerHealth + "\nAttacks: " + playerScript.attackCount;
    }

    public void GetDamage(int damage)
    {
        playerHealth -= damage;
        ChangeUI();
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
