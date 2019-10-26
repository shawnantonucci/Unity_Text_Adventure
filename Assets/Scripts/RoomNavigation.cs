using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{

    public Room currentRoom;

    Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();
    GameController controller;

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }

    public void UnpackExitsInRoom()
    {
        for (int i = 0; i < currentRoom.exits.Length; i++)
        {
            exitDictionary.Add(currentRoom.exits[i].keyString, currentRoom.exits[i].valueRoom);
            controller.interationDescriptionInRoom.Add(currentRoom.exits[i].exitDescription);
        }
    }

    public void AttemptToChangeRooms(string direction)
    {
        if (exitDictionary.ContainsKey(direction))
        {
            currentRoom = exitDictionary[direction];
            controller.LogStringWithReturn("You make your way to the  " + direction);
            controller.DisplayRoomText();
        }
        else
        {
            controller.LogStringWithReturn("There is no path to the " + direction);
        }
    }

    public void ClearExits()
    {
        exitDictionary.Clear();
    }
}
