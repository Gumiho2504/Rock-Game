using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class RockNetworkManager : NetworkManager
    {
        
      /*  GameObject playerChoice;*/
      
        public Transform player1, player2;
        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            Transform start = numPlayers == 0 ? player1 : player2;
            GameObject player = Instantiate(playerPrefab, start.position, start.rotation);

            NetworkServer.AddPlayerForConnection(conn, player);

            //NetworkServer.Spawn(player);
           /* if (numPlayers == 2)
            {
                playerChoice = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "playerchoice"));
                NetworkServer.Spawn(playerChoice);
            }*/
        }
      
        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            //NetworkServer.DestroyPlayerForConnection(conn);
        
      
        base.OnServerDisconnect(conn);
           
        }
        public override void OnClientDisconnect()
        {
            OnStopClient();
            base.OnClientDisconnect();
           
        }





}// end of class


