using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int floor;
    public int playerHealth = 5;
    public int damage = 2;

    public GameObject player;
    public GameObject endPoint;
    bool endSpawned;

    CreateBoard cb;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        GameObject.Find("GameManager").GetComponent<CreateBoard>();
        player.SetActive(true);

        endSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetDamage()
    {
        playerHealth--;
    }

    public void SpawnEndPoint(Vector2 point)
    {
        if (!endSpawned)
        {
            Instantiate(endPoint, point, Quaternion.identity);
        }
    }
}
