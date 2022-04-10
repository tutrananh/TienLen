using com.tvd12.ezyfoxserver.client;
using com.tvd12.ezyfoxserver.client.config;
using com.tvd12.ezyfoxserver.client.constant;
using com.tvd12.ezyfoxserver.client.entity;
using com.tvd12.ezyfoxserver.client.handler;
using com.tvd12.ezyfoxserver.client.request;
using System;
using System.Collections.Generic;
using UnityEngine;

class HandshakeHandler : EzyHandshakeHandler
{
	protected override EzyRequest getLoginRequest()
	{
		return new EzyLoginRequest(
			SocketProxy.ZONE_NAME,
			SocketProxy.getInstance().UserAuthenInfo.Username,
			SocketProxy.getInstance().UserAuthenInfo.Password
		);
	}
}
class LoginSuccessHandler : EzyLoginSuccessHandler
{
	protected override void handleLoginSuccess(EzyData responseData)
	{
		logger.debug("Login successfully!");
		// send the appAccessRequest to server
		SocketRequest.getInstance().sendAppAccessRequest();
	}
}
class AppAccessHandler : EzyAppAccessHandler
{
	public static event Action appAccessSuccessEvent;
	protected override void postHandle(EzyApp app, EzyArray data)
	{
		logger.debug("App access successfully!");
		// Start game logic
		appAccessSuccessEvent?.Invoke();
	}
}

#region App Data Handler

class JoinLobbyResponseHandler : EzyAbstractAppDataHandler<EzyObject>
{
	public static event Action joinedLobbyEvent;
	protected override void process(EzyApp app, EzyObject data)
	{
		joinedLobbyEvent?.Invoke();
	}
}

class CreateRoomResponseHandler : EzyAbstractAppDataHandler<EzyObject>
{
	public static event Action<int> roomCreatedEvent;
	protected override void process(EzyApp app, EzyObject data)
	{
		logger.info("Room created successfully: " + data.ToString());
		roomCreatedEvent?.Invoke(data.get<int>("roomId"));
	}
}

class GetLocatedRoomIdListResponse : EzyAbstractAppDataHandler<EzyArray>
{
	public static event Action<List<int>> locatedRoomIdListResponseEvent;
	protected override void process(EzyApp app, EzyArray data)
	{
		logger.info("Room id list: " + data.get<EzyArray>(0).ToString());
		// TODO: should change to toList<long>() in the next version
		List<int> roomIdList = data.get<EzyArray>(0).toList<int>();
		locatedRoomIdListResponseEvent?.Invoke(roomIdList);
	}
}
/*
class GetMMORoomPlayersResponseHandler : EzyAbstractAppDataHandler<EzyObject>
{
	public static event Action<List<string>, string> mmoRoomPlayersResponseEvent;
	protected override void process(EzyApp app, EzyObject data)
	{
		logger.info("Current room's players: " + data);
		List<string> playerNames = data.get<EzyArray>("players").toList<string>();
		string masterName = data.get<string>("master");
		logger.info("Player Names: " + string.Join(",", playerNames));
		logger.info("Master Name: " + masterName);

		mmoRoomPlayersResponseEvent?.Invoke(playerNames, masterName);
	}
}*/

class JoinLocatedRoomResponseHandler : EzyAbstractAppDataHandler<EzyObject>
{
	public static event Action<int> joinRoomResponseEvent;
	protected override void process(EzyApp app, EzyObject data)
	{
		logger.info("room id: " + data);
		int roomId = data.get<int>("roomId");
		joinRoomResponseEvent?.Invoke(roomId);
	}
}

class AnotherJoinLocatedRoomHandler : EzyAbstractAppDataHandler<EzyObject>
{
	public static event Action<string> anotherJoinMMORoomEvent;
	protected override void process(EzyApp app, EzyObject data)
	{
		logger.info("Another player join room: " + data);
		string anotherName = data.get<string>("playerName");
		anotherJoinMMORoomEvent?.Invoke(anotherName);
	}
}

class AnotherExitLocatedRoomHandler : EzyAbstractAppDataHandler<EzyObject>
{
	public static event Action<string> anotherExitMMORoomEvent;
	protected override void process(EzyApp app, EzyObject data)
	{
		logger.info("Another player exit room: " + data);
		string anotherName = data.get<string>("playerName");
		anotherExitMMORoomEvent?.Invoke(anotherName);
	}
}

#endregion
public class SocketProxy
{
	private static readonly SocketProxy INSTANCE = new SocketProxy();

	public const string ZONE_NAME = "EzyCard";
	public const string PLUGIN_NAME = "EzyCard";
	public const string APP_NAME = "EzyCard";

	private string host;
	private int port;
	private EzyClient client;
	private User userAuthenInfo = new User("", "");

	public string Host => host;
	public int Port => port;
	public EzyClient Client => client;
	public User UserAuthenInfo => userAuthenInfo;

	public static SocketProxy getInstance()
	{
		return INSTANCE;
	}

	public EzyClient setup(string host, int port)
	{
		Debug.Log("Start setting up socket client...");
		this.host = host;
		this.port = port;

		var config = EzyClientConfig.builder()
			.clientName(ZONE_NAME)
			.build();
		var clients = EzyClients.getInstance();
		client = clients.newDefaultClient(config);
		var setup = client.setup();

		// Add some data handlers to setup
		setup.addDataHandler(EzyCommand.HANDSHAKE, new HandshakeHandler());
		setup.addDataHandler(EzyCommand.LOGIN, new LoginSuccessHandler());
		setup.addDataHandler(EzyCommand.APP_ACCESS, new AppAccessHandler());

		// Set up EzySmashers app
		var appSetup = setup.setupApp(APP_NAME);
		appSetup.addDataHandler(Commands.JOIN_LOBBY, new JoinLobbyResponseHandler());
		appSetup.addDataHandler(Commands.CREATE_LOCATED_ROOM, new CreateRoomResponseHandler());
	/*	appSetup.addDataHandler(Commands.GET_LOCATED_ROOM_ID_LIST, new GetMMORoomIdListResponse());
		appSetup.addDataHandler(Commands.GET_LOCATED_ROOM_PLAYERS, new GetMMORoomPlayersResponseHandler());*/
		appSetup.addDataHandler(Commands.JOIN_LOCATED_ROOM, new JoinLocatedRoomResponseHandler());
		appSetup.addDataHandler(Commands.ANOTHER_JOIN_LOCATED_ROOM, new AnotherJoinLocatedRoomHandler());
		appSetup.addDataHandler(Commands.ANOTHER_EXIT_LOCATED_ROOM, new AnotherExitLocatedRoomHandler());

		Debug.Log("Finish setting up socket client!");
		return client;
	}

    public void login(string username, string password)
    {
		userAuthenInfo.SetUser(username,password);
		client.connect(Host, Port);
	}
}