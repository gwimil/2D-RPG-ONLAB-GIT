using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace EventCallbacks
{
    public class MyNetworkManager : NetworkManager
    {
        public int chosenCharacter = 0;
        public GameObject[] characters;

        //subclass for sending network messages
        public class NetworkMessage : MessageBase
        {
            public int chosenClass;
        }

        public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
        {
            NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
            int selectedClass = message.chosenClass;
            Debug.Log("server add with message " + selectedClass);

            GameObject player;
            Transform startPos = GetStartPosition();

            if (startPos != null)
            {
               // player = Instantiate(characters[selectedClass], startPos.position + pos, startPos.rotation) as GameObject;
                player = Instantiate(playerPrefab, startPos.position , startPos.rotation) as GameObject;
                player.GetComponent<PlayerConnectionObject>().PlayerUnitPrefab = characters[selectedClass];
            }
            else
            {
              // player = Instantiate(characters[selectedClass], Vector3.zero, Quaternion.identity) as GameObject;
                player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                player.GetComponent<PlayerConnectionObject>().PlayerUnitPrefab = characters[selectedClass];
            }

            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            NetworkMessage test = new NetworkMessage();
            test.chosenClass = chosenCharacter;

            ClientScene.AddPlayer(conn, 0, test);
        }


        public override void OnClientSceneChanged(NetworkConnection conn)
        {
            //base.OnClientSceneChanged(conn);
        }

    }
}

