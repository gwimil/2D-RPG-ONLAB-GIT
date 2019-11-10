using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace EventCallbacks
{
    public class PlayerConnectionObject : NetworkBehaviour
    {


        public GameObject PlayerUnitPrefab;
        public GameObject m_YouDiedTextObject;

        [HideInInspector]

        // Use this for initialization
        void Start()
        {

            // Is this actually my own local PlayerConnectionObject?
               if (isLocalPlayer == false)
               {
                   // This object belongs to another player.
                   return;
               }

            CmdSpawnMyUnit();
        }


        private void Update()
        {
            if (isLocalPlayer == false)
            {
                return;
            }
        }

        public void SetYouLoseActive()
        {
            if (isLocalPlayer == false)
            {
                // This object belongs to another player.
                return;
            }

            m_YouDiedTextObject.GetComponentInChildren<Text>().text = "Meghaltál";
            m_YouDiedTextObject.SetActive(true);
        }


        public void SetYouWinActive()
        {
            if (isLocalPlayer == false)
            {
                // This object belongs to another player.
                return;
            }
            m_YouDiedTextObject.GetComponentInChildren<Text>().text = "Nyertél";
            m_YouDiedTextObject.SetActive(true);
        }

        public void ReSpawn()
        {
            if (isLocalPlayer == false)
            {
                // This object belongs to another player.
                return;
            }
            m_YouDiedTextObject.SetActive(false);
            CmdSpawnMyUnit();
        }

        [Command]
        void CmdSpawnMyUnit()
        {
            // float randomX = Random.Range(-5.0f, 5.0f);
            // float randomY = Random.Range(-5.0f, 5.0f);

            //  GameObject go = Instantiate(PlayerUnitPrefab, gameObject.transform.position + new Vector3(randomX,randomY,0),Quaternion.Euler(0,0,0), this.gameObject.transform);
            GameObject go = Instantiate(PlayerUnitPrefab, this.gameObject.transform);
            NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
            go.transform.SetParent(gameObject.transform);
       //     RpcSetParent(go, gameObject);
        }

     /*   [ClientRpc]
        void RpcSetParent(GameObject child, GameObject parent)
        {
            child.transform.SetParent(parent.transform);
        }*/

    }
}