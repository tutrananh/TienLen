using com.tvd12.ezyfoxserver.client.entity;
using com.tvd12.ezyfoxserver.client.factory;
using com.tvd12.ezyfoxserver.client.request;
using com.tvd12.ezyfoxserver.client.util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketRequest : EzyLoggable
{
	private static readonly SocketRequest INSTANCE = new SocketRequest();

	public static SocketRequest getInstance()
	{
		return INSTANCE;
	}
	public void sendAppAccessRequest()
	{
		var client = SocketProxy.getInstance().Client;
		var request = new EzyAppAccessRequest(SocketProxy.APP_NAME);
		client.send(request);
	}
	public void SendJoinLobbyRequest()
	{
		var client = SocketProxy.getInstance().Client;
		client.getApp().send(Commands.JOIN_LOBBY);
	}

	public void SendCreateLocatedRoomRequest()
	{
		var client = SocketProxy.getInstance().Client;
		client.getApp().send(Commands.CREATE_LOCATED_ROOM);
	}

	public void SendGetLocatedRoomIdListRequest()
	{
		var client = SocketProxy.getInstance().Client;
		client.getApp().send(Commands.GET_LOCATED_ROOM_ID_LIST);
	}

	public void SendGetLocatedRoomPlayersRequest()
	{
		var client = SocketProxy.getInstance().Client;
		client.getApp().send(Commands.GET_LOCATED_ROOM_PLAYERS);
	}

	public void SendJoinLocatedRoomRequest(int roomId)
	{
		var client = SocketProxy.getInstance().Client;
		EzyObject data = EzyEntityFactory
			.newObjectBuilder()
			.append("roomId", roomId)
			.build();
		client.getApp().send(Commands.JOIN_LOCATED_ROOM, data);
	}

	public void SendStartGameRequest()
	{
		var client = SocketProxy.getInstance().Client;
		client.getApp().send(Commands.START_GAME);
	}

	
}
