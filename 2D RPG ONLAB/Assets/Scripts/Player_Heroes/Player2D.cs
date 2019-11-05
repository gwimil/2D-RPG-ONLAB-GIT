using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class Player2D : MonoBehaviour
    {

        // GAME MANAGER SETS THE HERO
        [HideInInspector] public Hero m_hero;

        private MyEventSystemProvider me;
        private bool inventoryEnabled;
        Vector2 velocity;
        public float m_MovementSpeed;
        public int m_PlayerID = 0;
        public UnityEngine.EventSystems.EventSystem eventSystem;
        private GameObject go;

        private void Awake()
        {
            go = new GameObject();
        }


        private void Start()
        {
            inventoryEnabled = false;
            go = m_hero.m_FirstInventorySlot;
            eventSystem.SetSelectedGameObject(go);
        }


        void Update()
        {
            

            if (Input.GetButtonDown("InventoryP" + m_PlayerID))
            {
                m_hero.inventory.m_inventoryEnabled = !m_hero.inventory.m_inventoryEnabled;
                if (m_hero.inventory.m_inventoryEnabled)
                {
                    m_hero.inventory.m_inventory.SetActive(true);
                    inventoryEnabled = true;
                    eventSystem.SetSelectedGameObject(m_hero.m_FirstInventorySlot);
                }
                else
                {
                    m_hero.inventory.m_inventory.SetActive(false);
                    inventoryEnabled = false;
                }
            }


             if (!inventoryEnabled)
             {
                if (m_PlayerID == 0)
                {
                    Debug.Log("You forgot to give your players ID-s");
                }
                else
                {
                    velocity = new Vector2(Input.GetAxisRaw("Horizontal" + m_PlayerID), Input.GetAxisRaw("Vertical" + m_PlayerID)).normalized * m_MovementSpeed;
                }

                // ATTACK BASIC
                if (Input.GetButtonDown("BasicAttackP" + m_PlayerID))
                {
                    m_hero.Attack();
                }

                //USE SPELL 1
                if (Input.GetButtonDown("SpellOneP" + m_PlayerID))
                {
                    m_hero.UseSkill(1);
                }

                // 
                if (Input.GetButtonDown("UseP" + m_PlayerID))
                {
                    if (m_hero.bags > 0)
                    {
                        ItemPickupEventInfo ipei = new ItemPickupEventInfo();
                        ipei.EventDescription = "your hero wants to pick up an item";
                        ipei.HeroName = m_hero.gameObject.name;
                        EventSystem.Current.FireEvent(ipei);
                    }
                }


            }
            else
            {
                if (eventSystem.currentSelectedGameObject != null)
                {
                      go = eventSystem.currentSelectedGameObject;
                }
                if (eventSystem.currentSelectedGameObject == null)
                {
                    eventSystem.SetSelectedGameObject(go);
                }

                if (Input.GetButtonDown("BasicAttackP" + m_PlayerID))
                {
                    m_hero.Attack();
                }

            }


        }

        void FixedUpdate()
        {
            if (!inventoryEnabled)
            {
                m_hero.Move(velocity * Time.fixedDeltaTime);
            }
            else
            {

            }

            
        }
    }
}