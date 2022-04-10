using com.tvd12.ezyfoxserver.client;
using com.tvd12.ezyfoxserver.client.logger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketInitializer : MonoBehaviour
{
	private static SocketInitializer instance;
	public string host = "127.0.0.1";
	public int port = 3005;
	private EzyClient client;

	private void Awake()
	{
		// When going back to previous scene, don't make duplication
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		// Enable EzyUnity Logger
		EzyLoggerFactory.setLoggerSupply(type => new EzyUnityLogger(type));

		//setup socket
		var socketProxy = SocketProxy.getInstance();
		client = socketProxy.setup(host, port);
	}

	private void Update()
	{
		client.processEvents();
	}
}
