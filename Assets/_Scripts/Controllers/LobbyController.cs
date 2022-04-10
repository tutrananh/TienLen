using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
	[SerializeField]
	private RoomIdList roomList;
	private void Awake()
	{
		//CreateRoomResponseHandler.roomCreatedEvent += JoinRoom;
		GetLocatedRoomIdListResponse.locatedRoomIdListResponseEvent += OnLocatedRoomIdListResponse;
//		JoinLocatedRoomResponseHandler.joinRoomResponseEvent += JoinRoom;
	}

	private void Start()
	{
		OnRefreshRoomIdList();
	}

	private void OnLocatedRoomIdListResponse(List<int> roomIdList)
	{
		roomList.SetRoomIdList(roomIdList);
	}


	public void OnRefreshRoomIdList()
	{
		Debug.Log("LobbyController: OnRefreshRoomIdList");
		SocketRequest.getInstance().SendGetLocatedRoomIdListRequest();
	}

}
