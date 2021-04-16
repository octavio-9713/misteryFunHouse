using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    [Header("Simple Rooms")]
    public GameObject[] topRooms;
    public GameObject[] rightRooms;
    public GameObject[] bottomRooms;
    public GameObject[] leftRooms;

    [Header("Salas sin salida")]    
    public GameObject[] topRoomsFinish;
    public GameObject[] rightRoomsFinish;
    public GameObject[] bottomRoomsFinish;
    public GameObject[] leftRoomsFinish;

    public GameObject closedRoom;
}
