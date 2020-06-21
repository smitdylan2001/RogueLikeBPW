using UnityEngine;

public enum Direction
{
    North, East, South, West,
}

public class Corridor
{
    public int startXPos;
    public int startYPos;
    public int corridorLength;
    public Direction direction;
 
    public int EndPositionX
    {
        get
        {
            if (direction == Direction.North || direction == Direction.South) return startXPos;   //North & South
            if (direction == Direction.East) return startXPos + corridorLength - 1;               //East
            return startXPos - corridorLength + 1;                                                //West
        }
    } 

    public int EndPositionY
    {
        get
        {
            if (direction == Direction.East || direction == Direction.West) return startYPos;     //East & West
            if (direction == Direction.North) return startYPos + corridorLength - 1;              //North
            return startYPos - corridorLength + 1;                                                //South
        }
    }

    public void SetupCorridor (Room room, GetRandom length, GetRandom roomWidth, GetRandom roomHeight, int columns, int rows, bool firstCorridor)
    {
        //random direction 0-3
        direction = (Direction)Random.Range(0, 4);

        //Get opposite of the entering corridoor to check where the entering corridor came from
        Direction opposite = (Direction)(((int)room.enteringCorridor + 2) % 4);

        //Reverse direction if it's the same as the entering corridor
        if (!firstCorridor && direction == opposite)
        {
            int directionInt = (int)direction;
            directionInt = (directionInt + 1) % 4;
            direction = (Direction)directionInt;
        }

        corridorLength = length.Random;
        int maxLength = length.max;

        //Make sure the length is not outside of the board
        switch (direction)
        {
            case Direction.North:
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth - 1);
                startYPos = room.yPos + room.roomHeight;
                maxLength = rows - startYPos - roomHeight.min;
                break;
            case Direction.East:
                startXPos = room.xPos + room.roomWidth; 
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight - 1);
                maxLength = columns - startXPos - roomWidth.min;
                break;
            case Direction.South:
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth);
                startYPos = room.yPos;
                maxLength = startYPos - roomHeight.min;
                break;
            case Direction.West:
                startXPos = room.xPos;
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight);
                maxLength = startXPos - roomWidth.min;
                break;
        }

        //clamp length to keep the corridor inside of the board
        corridorLength = Mathf.Clamp(corridorLength, 1, maxLength);
    }
}
