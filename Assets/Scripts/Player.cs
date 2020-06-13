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
        switch (movedir)
        {
            case MoveDirection.none:
                break;
            case MoveDirection.Up:
                if (move)
                {
                    targetLocation = new Vector2(transform.position.x, transform.position.y + 1);
                    move = false;
                }
                transform.position = Vector2.MoveTowards(transform.position, targetLocation, 0.5f);
                if (transform.position.y >= targetLocation.y)
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
}
