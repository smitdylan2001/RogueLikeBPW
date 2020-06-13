using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public enum MoveDirection
{
    none,Up,Down,Left,Right,
}

public class Player : MonoBehaviour
{
    MoveDirection movedir;
    bool move;
    Vector2 targetLocation;
    void Start()
    {
        move = false;
    }

    void Update()
    {
            if (Input.GetKeyDown(KeyCode.W))
        {
            movedir = MoveDirection.Up;
            move = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            movedir = MoveDirection.Down;
            move = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            movedir = MoveDirection.Right;
            move = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            movedir = MoveDirection.Left;
            move = true;
        }
        switch (movedir)
        {    
            case MoveDirection.none:
                break;
            case MoveDirection.Up:
                if (move)
                {
                    targetLocation = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y + 1f));
                    move = false;
                }
                transform.position = Vector2.MoveTowards(transform.position, targetLocation, 0.1f);
                if (transform.position.y >= targetLocation.y)
                {
                    transform.position = targetLocation;
                    movedir = MoveDirection.none;
                }
                break;
            case MoveDirection.Down:
                if (move)
                {
                    targetLocation = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y - 1f));
                    move = false;
                }
                transform.position = Vector2.MoveTowards(transform.position, targetLocation, 0.1f);
                if (transform.position.y <= targetLocation.y)
                {
                    transform.position = targetLocation;
                    movedir = MoveDirection.none;
                }
                break;
            case MoveDirection.Right:
                if (move)
                {
                    targetLocation = new Vector2(Mathf.RoundToInt(transform.position.x + 1f), Mathf.RoundToInt(transform.position.y));
                    move = false;
                }
                transform.position = Vector2.MoveTowards(transform.position, targetLocation, 0.1f);
                if (transform.position.x >= targetLocation.x)
                {
                    transform.position = targetLocation;
                    movedir = MoveDirection.none;
                }
                break;
            case MoveDirection.Left:
                if (move)
                {
                    targetLocation = new Vector2(Mathf.RoundToInt( transform.position.x - 1f), Mathf.RoundToInt(transform.position.y));
                    move = false;
                }
                transform.position = Vector2.MoveTowards(transform.position, targetLocation, 0.1f);
                if (transform.position.x <= targetLocation.x)
                {
                    transform.position = targetLocation;
                    movedir = MoveDirection.none;
                }
                break;
        }
    }

    
    public void onUp()
    {
        movedir = MoveDirection.Up;
        move = true;
    }
    public void onDown()
    {
        movedir = MoveDirection.Down;
        move = true;
    }
    public void onRight()
    {
        movedir = MoveDirection.Right;
        move = true;
    }
    public void onLeft()
    {
        movedir = MoveDirection.Left;
        move = true;
    }

    public void onHold()
    {
        move = false;
    }
}
