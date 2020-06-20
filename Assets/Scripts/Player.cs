﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum MoveDirection
{
    none,Up,Down,Left,Right,
}

public class Player : MonoBehaviour
{
    private float moveSpeed = 3f;
    private float gridSize = 1f;
    private enum Orientation
    {
        Horizontal,
        Vertical
    }

    private Orientation gridOrientation = Orientation.Horizontal;
    private bool allowDiagonals = false;
    private bool correctDiagonalSpeed = true;
    private Vector2 input;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;
    CreateBoard cb;
    int attackCount = 5;
    float attackArea = 3;

    private void Start()
    {
        cb = GameObject.Find("GameManager").GetComponent<CreateBoard>();    
    }

    public void Update()
    {
        //Check for movement input
        if (!isMoving)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (!allowDiagonals)
            {
                if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                {
                    input.y = 0;
                }
                else
                {
                    input.x = 0;
                }
            }
            //If momenent is detected move the character
            if (input != Vector2.zero)
            {
                StartCoroutine(move(transform));
            }
        }

        //Explosion attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Get colliders in circle
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackArea);
            int i = 0;

            //Check all colliders for tag
            while (i < hitColliders.Length)
            {
                //Destroy walls
                if (hitColliders[i].tag == "Wall")
                {
                    Destroy(hitColliders[i].gameObject);
                    cb.tiles[(int)hitColliders[i].gameObject.transform.position.x][(int)hitColliders[i].gameObject.transform.position.y] = CreateBoard.TileType.Floor;
                }

                //Kill enemies
                else if (hitColliders[i].tag == "Enemy")
                {
                    Destroy(hitColliders[i].gameObject);
                    attackCount += 2;
                }
                i++;
            } 
            attackCount--;

            //limit attacks
            if (attackCount > 5) attackCount = 5;

            //if empty, use a small attack
            if (attackCount <= 0) attackArea = 1.2f;
            else attackArea = 3f;
        }
    }

    public IEnumerator move(Transform transform)
    {
        isMoving = true;
        startPosition = transform.position;

        t = 0;
        //Check direction
        if (gridOrientation == Orientation.Horizontal)
        {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize, startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z  );
        }
        else
        {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize, startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
        }

        //Check if player can move diagnal
        if (allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0)
        {
            factor = 0.7f;
        }
        else
        {
            factor = 1f;
        }

        //Move player
        if (cb.tiles[(int)endPosition.x][(int)endPosition.y] == CreateBoard.TileType.Floor)
        {
            while (t < 1f)
            {
                t += Time.deltaTime * (moveSpeed / gridSize) * factor;
                 transform.position = Vector3.Lerp(startPosition, endPosition, t);
                yield return null;
            }
        }

        isMoving = false;
        yield return null;
    }
}
