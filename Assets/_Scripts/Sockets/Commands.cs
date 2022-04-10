using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commands 
{
    public const String SYNC_DATA = "syncData";
    public const String JOIN_LOBBY = "joinLobby";
    public const String CREATE_LOCATED_ROOM = "createLocatedRoom";
    public const String GET_LOCATED_ROOM_ID_LIST = "getLocatedRoomIdList";
    public const String GET_LOCATED_ROOM_PLAYERS = "getLocatedRoomPlayers";
    public const String JOIN_LOCATED_ROOM = "joinLocatedRoom";
    public const String ANOTHER_JOIN_LOCATED_ROOM = "anotherJoinLocatedRoom";
    public const String ANOTHER_EXIT_LOCATED_ROOM = "anotherExitLocatedRoom";
    public const String START_GAME = "startGame";


    private Commands()
    {
    }
}
