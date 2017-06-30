using System;
using System.Collections;
using UnityEngine;

public class ServerHandler : MonoBehaviour
{
	public int Port = 25000;
	private bool _serverInitialized = false;
	private ApplicationHandler _appHandler;

	public void Start()
	{
		_appHandler = GameObject.Find("Application Handler").GetComponent<ApplicationHandler>();
	}

	public void Update()
	{
		if (Network.peerType == NetworkPeerType.Disconnected && !_serverInitialized)
		{
			CreateServer();
		}
	}

	public void CreateServer()
	{
		try
		{
			StartCoroutine(CreateServerCoroutine());
		}
		catch (Exception exception)
		{
			Debug.LogError("ServerHandler initialization failed: " + exception.Message);
		}
	}

	private IEnumerator CreateServerCoroutine()
	{
		_serverInitialized = true;
		Debug.Log("Creating server at port: " + Port);
		do
		{
			Network.InitializeServer(10, Port, false);
			yield return new WaitForSeconds(2f);
			Debug.Log("Peer status: " + Network.peerType);
		}
		while (Network.peerType == NetworkPeerType.Disconnected);
		_serverInitialized = false;

		yield return null;
	}

	void OnPlayerConnected(NetworkPlayer player)
	{
		AskClientForInfo(player);
	}

	void AskClientForInfo(NetworkPlayer player)
	{
		GetComponent<NetworkView>().RPC("SetPlayerInfo", player, player);
	}

	[RPC]
	void LaunchVideo01()
	{
		_appHandler.LaunchVideo01();
	}

	[RPC]
	void LaunchVideo02()
	{
		_appHandler.LaunchVideo02();
	}

	[RPC]
	void LaunchVideo03()
	{
		_appHandler.LaunchVideo03();
	}

	[RPC]
	void ReceiveInfoFromClient(string someInfo)
	{
		Debug.Log(someInfo + "\n");
	}

	[RPC]
	void SendInfoToClient()
	{
		string someInfo = "Server: hello client";
		GetComponent<NetworkView>().RPC("ReceiveInfoFromServer", RPCMode.Others, someInfo);
	}

	// fix RPC errors

	[RPC]
	void SendInfoToServer() { }

	[RPC]
	void SetPlayerInfo(NetworkPlayer player) { }

	[RPC]
	void ReceiveInfoFromServer(string someInfo) { }
}
