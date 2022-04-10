using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour
{
	private void Awake()
	{
		AppAccessHandler.appAccessSuccessEvent += OnAppAccessSuccess;
		JoinLobbyResponseHandler.joinedLobbyEvent += OnJoinedLobby;
	}

	private void OnAppAccessSuccess()
	{
		SocketRequest.getInstance().SendJoinLobbyRequest();
	}
	void OnJoinedLobby()
	{
	//	GameManager.getInstance().SetUpPlayer();

		// Change scene here
		SceneManager.LoadScene("LobbyScene");
	}
}
