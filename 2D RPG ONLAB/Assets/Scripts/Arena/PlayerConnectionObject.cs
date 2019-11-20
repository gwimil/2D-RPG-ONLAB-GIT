using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace EventCallbacks
{
    public class PlayerConnectionObject : NetworkBehaviour
    {
        public NetworkInstanceId ID;

        public GameObject PlayerUnitPrefab;
        public GameObject m_YouDiedTextObject;
        public Button m_RespawnButton;

    private bool isDead;

        // Use this for initialization
        void Start()
        {
            ID = netId;
            // Is this actually my own local PlayerConnectionObject?
               if (isLocalPlayer == false)
               {
                   // This object belongs to another player.
                   return;
               }
            isDead = false;
            m_RespawnButton.onClick.AddListener(ReSpawn);

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
            m_RespawnButton.gameObject.SetActive(true);
            isDead = true;
    }


        public void ReSpawn()
        {
            if (isLocalPlayer == false)
            {
                // This object belongs to another player.
                return;
            }
            if (isDead)
            {
              m_YouDiedTextObject.SetActive(false);
              m_RespawnButton.gameObject.SetActive(false);
              CmdSpawnMyUnit();
            }
            
        }

        [Command]
        void CmdSpawnMyUnit()
        {
            // float randomX = Random.Range(-5.0f, 5.0f);
            // float randomY = Random.Range(-5.0f, 5.0f);

            //  GameObject go = Instantiate(PlayerUnitPrefab, gameObject.transform.position + new Vector3(randomX,randomY,0),Quaternion.Euler(0,0,0), this.gameObject.transform);
            GameObject go = Instantiate(PlayerUnitPrefab, gameObject.transform);
            NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
            go.GetComponent<ArenaHeroes>().ID = ID;
            go.transform.SetParent(gameObject.transform);
            RpcSetParent(go, gameObject);
        }

        [ClientRpc]
        void RpcSetParent(GameObject child, GameObject parent)
        {
            child.transform.SetParent(parent.transform);
            child.GetComponent<ArenaHeroes>().ID = ID;
        }

    }
}