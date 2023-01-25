using UnityEngine;
using Mirror;
using System;
using System.Linq;
using Telepathy;
using UnityEngine.SceneManagement;
using UnityEngine.Networking.Types;

public class NetworkManagerLobby : NetworkManager 
{
   
    [Scene][SerializeField] private string menuScene = string.Empty;

    [Header("Room")]
    [SerializeField] private NetworkRoomPlayerLobby roomPlayerPrefab = null;

    public static event Action OnclientConnected;
    public static event Action OnclientDisconnected;

    public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

    public override void OnStartClient()
    {
        base.OnStartClient();
        var spawnblePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

        foreach (var prefab in spawnblePrefabs)
        {
            
            NetworkServer.Spawn(prefab);
        }
    }

    public override void OnClientConnect()
    {
        //base.OnClientConnect(conn);
        OnclientConnected?.Invoke();
        
    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        OnclientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);
        if(numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }
        if(SceneManager.GetActiveScene().name != menuScene)
        {
            conn.Disconnect();
            return;
        }
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        if(SceneManager.GetActiveScene().name == menuScene)
        {
            NetworkRoomPlayerLobby roomPlayerInstance = Instantiate(roomPlayerPrefab);

            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
        }
    }



}
