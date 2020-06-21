using Pathfinding;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    Player playerScript;
    Vector3 targetPos;

    Seeker seeker;

    int damage = 1;
    bool canMove;

    void Start()
    {
        //Get needed components
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        targetPos = player.GetComponent<Transform>().position;
        seeker = GetComponent<Seeker>();
        canMove = true;
    }

    void OnPathComplete(Path p)
    {
    }
    
    void FixedUpdate()
    {
        //Get player location
        targetPos = player.GetComponent<Transform>().position;

        //Check if player is near, the previous path finding is complete and the enemy can move
        if (Mathf.Abs(targetPos.x - transform.position.x) <= 10 && Mathf.Abs(targetPos.y - transform.position.y) <= 10 && seeker.IsDone() && canMove)
        {
            //Seek new path to player
            seeker.StartPath(transform.position, targetPos, OnPathComplete);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if it hit the player
        if (collision.tag == "Player")
        {
            //Do damange and freeze enemy
            GameManager.instance.GetDamage(damage);
            StartCoroutine(justAttacked());
            canMove = false;
        }
    }

    //Freeze enemy to give player time to do something
    IEnumerator justAttacked()
    {
        yield return new WaitForSeconds(2.5f);
        canMove = true;
        yield return null;
    }
}
