using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using UnityEngine;

public class ClientHandler : MonoBehaviour
{
	public string ServerIP = "127.0.0.1";
	public int Port = 25000;

	private bool _connecting = false;
	private bool _isServer;

	private NetworkPlayer _myNetworkPlayer;
	private RemoteHandler _remoteHandler;


	void Start()
	{
		_remoteHandler = GameObject.Find("Remote Handler").GetComponent<RemoteHandler>();

		try
		{
			List<IPAddress> addresses = GetIpAddresses();
			ServerIP = addresses.First().ToString();
		}
		catch (Exception exception)
		{
			throw new Exception("Could not set ip address. " + exception.Message);
		}
	}

	void Update()
	{
		if (Network.peerType == NetworkPeerType.Disconnected && !_connecting)
		{
			Connect();
		}
	}

	public void Connect()
	{
		try
		{
			StartCoroutine(ConnectCoroutine());
		}
		catch (Exception exception)
		{
			Debug.LogError("Connection to server failed: " + exception.Message);
		}
	}

	private List<IPAddress> GetIpAddresses()
	{
		List<IPAddress> result = new List<IPAddress>();

		string path = Application.dataPath;
		path = Path.GetFullPath(Path.Combine(path, @"..\"));
		path += "ip_address.txt";

		int fileLinesCount = File.ReadAllLines(path).Count();

		if (fileLinesCount <= 0)
		{
			Debug.Log("There are no ip address entries in file or file is missing.");
			return result;
		}

		using (TextReader reader = File.OpenText(path))
		{
			for (int i = 0; i <= fileLinesCount; i++)
			{
				string text = reader.ReadLine();

				if (string.IsNullOrEmpty(text)) continue;
				string[] bits = text.Split(' ');

				foreach (string bit in bits)
				{
					string trimmedBit = bit.Trim();
					IPAddress ipAddress;
					bool isIpAddress = IPAddress.TryParse(trimmedBit, out ipAddress);

					if (isIpAddress)
					{
						result.Add(ipAddress);
					}
				}
			}
		}

		foreach (IPAddress address in result)
		{
			Debug.Log("IpAddress " + result.IndexOf(address) + " : " + address);
		}

		return result;
	}

	private IEnumerator ConnectCoroutine()
	{
		_connecting = true;
		Debug.Log("Connecting to server at " + ServerIP + ":" + Port);
		do
		{
			Network.Connect(ServerIP, Port);
			yield return new WaitForSeconds(2f);
			Debug.Log("Peer status: " + Network.peerType);
		}
		while (Network.peerType == NetworkPeerType.Disconnected);
		_connecting = false;

		yield return null;
	}

	[RPC]
	public void OpenMap()
	{
		_remoteHandler.OpenMap();
		GetComponent<NetworkView>().RPC("OpenMap", RPCMode.Server);
	}

	[RPC]
	public void BackToMenu()
	{
		_remoteHandler.BackToMenu();
		GetComponent<NetworkView>().RPC("BackToMenu", RPCMode.Server);
	}

	[RPC]
	public void MoveToPointOnMap(int index)
	{
		GetComponent<NetworkView>().RPC("MoveToPointOnMap", RPCMode.Server, index);
	}

	[RPC]
	public void OpenSlide(int index)
	{
		GetComponent<NetworkView>().RPC("OpenSlide", RPCMode.Server, index);
	}

	[RPC]
	public void OpenHiddenSlide(int index)
	{
		if (index == 0)
		{
			_remoteHandler.HideHiddenButtons();
		}

		GetComponent<NetworkView>().RPC("OpenHiddenSlide", RPCMode.Server, index);
	}

	[RPC]
	public void LaunchStandBy()
	{
		GetComponent<NetworkView>().RPC("LaunchStandBy", RPCMode.Server);
	}

	[RPC]
	void SendInfoToServer()
	{
		string someInfo = "Client " + _myNetworkPlayer.guid + ": connected to server";
		GetComponent<NetworkView>().RPC("ReceiveInfoFromClient", RPCMode.Server, someInfo);
	}

	[RPC]
	void SetPlayerInfo(NetworkPlayer player)
	{
		_myNetworkPlayer = player;
		string someInfo = "Player set";
		GetComponent<NetworkView>().RPC("ReceiveInfoFromClient", RPCMode.Server, someInfo);
	}

	[RPC]
	void ReceiveInfoFromServer(string someInfo)
	{
		Debug.Log(someInfo + "\n");
	}

	void OnConnectedToServer()
	{
		Debug.Log("Connected to server" + "\n");
	}
	void OnDisconnectedToServer()
	{
		Debug.Log("Disconnected from server" + "\n");
	}

	// Fix RPC errors

	[RPC]
	void ReceiveInfoFromClient(string someInfo) { }

	[RPC]
	void SendInfoToClient(string someInfo) { }
}
