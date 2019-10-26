using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text displayText;
    public InputAction[] inputActions;

    [HideInInspector] public RoomNavigation roomNavigation;
    [HideInInspector] public List<string> interationDescriptionInRoom = new List<string>();
    [HideInInspector] public InteractableItems interactableItems;

    List<string> actionLog = new List<string>();

    // Start is called before the first frame update
    void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();
        interactableItems = GetComponent<InteractableItems>();
    }

    void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }

    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());

        displayText.text = logAsText;
    }

    public void DisplayRoomText()
    {
        ClearCollectionsForNewRoom();
     
        UnpackRoom();

        string joinedInterationDescriptions = string.Join("\n", interationDescriptionInRoom.ToArray());

        string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInterationDescriptions;

        LogStringWithReturn(combinedText);
    }

    public void UnpackRoom()
    {
        roomNavigation.UnpackExitsInRoom();
        PrepareObjectsToTakeOrExamine(roomNavigation.currentRoom);
    }

    void PrepareObjectsToTakeOrExamine(Room currentRoom)
    {
        for (int i = 0; i < currentRoom.interactableObjectsInRoom.Length; i++)
        {
            string descriptionNotInInventory = interactableItems.GetObjectsNotInInventory(currentRoom, i);
            if (descriptionNotInInventory != null)
            {
                interationDescriptionInRoom.Add(descriptionNotInInventory);
            }
        }
    }

    void ClearCollectionsForNewRoom()
    {
        interationDescriptionInRoom.Clear();
        roomNavigation.ClearExits();
    }

    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
