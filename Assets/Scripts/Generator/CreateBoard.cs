using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreateBoard : MonoBehaviour
{
    public enum TileType
    {
        Wall, Floor,
    }

    public int columns = 100;
    public int rows = 100;
    public GetRandom roomAmount = new GetRandom(12,20);
    public GetRandom roomWidth = new GetRandom(3,13);
    public GetRandom roomHeight = new GetRandom(3, 13);
    public GetRandom corridorLenth = new GetRandom(1, 16);
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] floorTiles;

    private TileType[][] tiles;
    private Room[] rooms;
    private Corridor[] corridors;
    private GameObject completeBoard;

    void Start()
    {
        //make GameObject to put the tiles in
        completeBoard = new GameObject("Board");

        SetupTileArray();
        CreateRoomsCorridors();

        SetRoomTiles();
        SetCorridorTiles();

        InstantiateTiles();
        InstantiateWalls();
    }

    void SetupTileArray()
    {
        tiles = new TileType[columns][];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new TileType[rows];
        }
    }

    void CreateRoomsCorridors()
    {
        //create amount of rooms and corridors
        rooms = new Room[roomAmount.Random];
        corridors = new Corridor[rooms.Length - 1];

        //make first room and corridor
        rooms[0] = new Room();
        corridors[0] = new Corridor();
        rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);
        corridors[0].SetupCorridor(rooms[0], corridorLenth, roomWidth, roomHeight, columns, rows, true);

        for (int i = 1; i < rooms.Length; i++)
        {
            //make other rooms
            rooms[i] = new Room();
            rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);

            //make other corridors untill the last room
            if (i < corridors.Length)
            {
                corridors[i] = new Corridor();
                corridors[i].SetupCorridor(rooms[i], corridorLenth, roomWidth, roomHeight, columns, rows, false);
            }
        }
    }

    void SetRoomTiles()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];

            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;

                for (int k = 0; k<currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;
                    Debug.Log(xCoord + " tiles " + yCoord);

                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
    }

    void SetCorridorTiles()
    {
        for (int i = 0; i < corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];

            for (int j = 0; j<currentCorridor.corridorLength; j++)
            {
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                switch (currentCorridor.direction)
                {
                    case Direction.North:
                        yCoord += j;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.South:
                        yCoord -= j;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }
                Debug.Log(xCoord + " corridors " +  yCoord);
                tiles[xCoord][yCoord] = TileType.Floor;
            }
        }
    }

    void InstantiateTiles()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].Length; j++)
            {
                if (tiles[i][j] == TileType.Floor)
                {
                    InstantiateFromArray(floorTiles, i, j);
                }
                if (tiles[i][j] == TileType.Wall)
                {
                    InstantiateFromArray(wallTiles, i, j);
                }
            }
        }
    }
    void InstantiateWalls ()
    {
        float leftEdge = -1f;
        float rightEdge = columns + 0f;
        float bottomEdge = -1f;
        float topEdge = rows + 0f;

        InstantiateVerticalWall(leftEdge, bottomEdge,topEdge);
        InstantiateVerticalWall(rightEdge, bottomEdge, topEdge);

        InstantiateHorizontalWall(bottomEdge, leftEdge + 1f, rightEdge - 1f);
        InstantiateHorizontalWall(topEdge, leftEdge + 1f, rightEdge - 1f);
    }

    void InstantiateVerticalWall(float xCoord, float startY, float endY)
    {
        float currentY = startY;

        while (currentY <= endY)
        {
            InstantiateFromArray(outerWallTiles, xCoord, currentY);
            currentY++;
        }
    } 
    
    void InstantiateHorizontalWall(float yCoord, float startX, float endX)
    {
        float currentX = startX;

        while (currentX <= endX)
        {
            InstantiateFromArray(outerWallTiles, currentX, yCoord);
            currentX++;
        }
    }

    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        int randonIndex = Random.Range(0, prefabs.Length);

        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        GameObject tileInstance = Instantiate(prefabs[randonIndex], position, Quaternion.identity) as GameObject;

        tileInstance.transform.parent = completeBoard.transform;
    }

}
