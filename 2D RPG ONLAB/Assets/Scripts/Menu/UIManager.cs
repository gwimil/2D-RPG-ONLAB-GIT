using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    enum Fit{
        left, mid, right
    };

    public class UIManager : MonoBehaviour
    {
        //jobb
        Fit mageFit = Fit.right;
        public Hero m_MageHero;
        public RectTransform m_MageInventory;
        public RectTransform m_MageHpSlider;
        public RectTransform m_MageManaSlider;
        public RectTransform m_MageBasicAttack;
        public RectTransform m_MageSpell1;
        public RectTransform m_MageSpell2;


        // közép
        Fit rangerFit = Fit.mid;
        public Hero m_RangerHero;
        public RectTransform m_RangerInventory;
        public RectTransform m_RangerHpSlider;
        public RectTransform m_RangerManaSlider;
        public RectTransform m_RangerBasicAttack;
        public RectTransform m_RangerSpell1;
        public RectTransform m_RangerSpell2;

        //bal
        Fit warriorFit = Fit.mid;
        public Hero m_WarriorHero;
        public RectTransform m_WarriorInventory;
        public RectTransform m_WarriorHpSlider;
        public RectTransform m_WarriorManaSlider;
        public RectTransform m_WarriorBasicAttack;
        public RectTransform m_WarriorSpell1;
        public RectTransform m_WarriorSpell2;

        RectTransform inventoryRectTransform;
        RectTransform hpRectTransform;
        RectTransform manaRectTransform;
        RectTransform basicRectTransform;
        RectTransform spell1RectTransform;
        RectTransform spell2RectTransform;

        
        private RectTransform MageInventory;
        private RectTransform MageHpSlider;
        private RectTransform MageManaSlider;
        private RectTransform MageBasicAttack;
        private RectTransform MageSpell1;
        private RectTransform MageSpell2;

        
        private RectTransform RangerInventory;
        private RectTransform RangerHpSlider;
        private RectTransform RangerManaSlider;
        private RectTransform RangerBasicAttack;
        private RectTransform RangerSpell1;
        private RectTransform RangerSpell2;
        
        private RectTransform WarriorInventory;
        private RectTransform WarriorHpSlider;
        private RectTransform WarriorManaSlider;
        private RectTransform WarriorBasicAttack;
        private RectTransform WarriorSpell1;
        private RectTransform WarriorSpell2;

        private GameObject go;

        [HideInInspector] public bool changeUI;

        private void Awake()
        {
            changeUI = false;

            go = new GameObject("UI1", typeof(RectTransform));
            inventoryRectTransform = go.GetComponent<RectTransform>();
            go = new GameObject("UI2", typeof(RectTransform));
            hpRectTransform = go.GetComponent<RectTransform>();
            go = new GameObject("UI3", typeof(RectTransform));
            manaRectTransform = go.GetComponent<RectTransform>();
            go = new GameObject("UI4", typeof(RectTransform));
            basicRectTransform = go.GetComponent<RectTransform>();
            go = new GameObject("UI5", typeof(RectTransform));
            spell1RectTransform = go.GetComponent<RectTransform>();
            go = new GameObject("UI6", typeof(RectTransform));
            spell2RectTransform = go.GetComponent<RectTransform>();


            MageInventory = m_MageInventory;
            MageHpSlider = m_MageHpSlider;
            MageManaSlider = m_MageManaSlider;
            MageBasicAttack = m_MageBasicAttack;
            MageSpell1 = m_MageSpell1;
            MageSpell2 = m_MageSpell2;

            RangerInventory = m_RangerInventory;
            RangerHpSlider = m_RangerHpSlider;
            RangerManaSlider = m_RangerManaSlider;
            RangerBasicAttack = m_RangerBasicAttack;
            RangerSpell1 = m_RangerSpell1;
            RangerSpell2 = m_RangerSpell2;

            WarriorInventory = m_WarriorInventory;
            WarriorHpSlider = m_WarriorHpSlider;
            WarriorManaSlider = m_WarriorManaSlider;
            WarriorBasicAttack = m_WarriorBasicAttack;
            WarriorSpell1 = m_WarriorSpell1;
            WarriorSpell2 = m_WarriorSpell2;
    }

        private void OnGUI()
        {
           if (changeUI)
            {
                SetUIToPlayerPosition();
                changeUI = false;
            }
        }

        // change when screen sides are changing
        public void SetUIToPlayerPosition()
        {
            Vector2 magePos = m_MageHero.transform.position;
            Vector2 warriorPos = m_WarriorHero.transform.position;
            Vector2 rangerPos = m_RangerHero.transform.position;

            if (MenuData.m_playerNumber == 2)
            {
                // 2 player
                if (!m_MageHero.isActiveAndEnabled)
                {
                    if (warriorPos.x < rangerPos.x)
                    {
                        m_RangerInventory.anchoredPosition = new Vector2(Screen.width / 4 + 100, 0);
                        m_WarriorInventory.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 0);

                        m_RangerHpSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -20);
                        m_WarriorHpSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -20);

                        m_RangerManaSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -30);
                        m_WarriorManaSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -30);

                        m_RangerBasicAttack.anchoredPosition = new Vector2(Screen.width / 4 + 100 -30, 15);
                        m_WarriorBasicAttack.anchoredPosition = new Vector2(-Screen.width / 4 - 100 -30, 15);

                        m_RangerSpell1.anchoredPosition = new Vector2(Screen.width / 4 + 100, 15);
                        m_WarriorSpell1.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 15);

                        m_RangerSpell2.anchoredPosition = new Vector2(Screen.width / 4 + 100 +30 , 15);
                        m_WarriorSpell2.anchoredPosition = new Vector2(-Screen.width / 4 - 100 +30, 15);
                    }
                    else
                    {
                        m_WarriorInventory.anchoredPosition = new Vector2(Screen.width / 4 + 100, 0);
                        m_RangerInventory.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 0);

                        m_WarriorHpSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -20);
                        m_RangerHpSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -20);

                        m_WarriorManaSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -30);
                        m_RangerManaSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -30);

                        m_WarriorBasicAttack.anchoredPosition = new Vector2(Screen.width / 4 + 100 - 30, 15);
                        m_RangerBasicAttack.anchoredPosition = new Vector2(-Screen.width / 4 - 100 - 30, 15);

                        m_WarriorSpell1.anchoredPosition = new Vector2(Screen.width / 4 + 100, 15);
                        m_RangerSpell1.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 15);

                        m_WarriorSpell1.anchoredPosition = new Vector2(Screen.width / 4 + 100 + 30, 15);
                        m_RangerSpell2.anchoredPosition = new Vector2(-Screen.width / 4 - 100 + 30, 15);
                    }

                }
                else if (!m_WarriorHero.isActiveAndEnabled)
                {
                    if (magePos.x < rangerPos.x)
                    {
                        m_RangerInventory.anchoredPosition = new Vector2(Screen.width / 4 + 100, 0);
                        m_MageInventory.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 0);

                        m_RangerHpSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -20);
                        m_MageHpSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -20);

                        m_RangerManaSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -30);
                        m_MageManaSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -30);

                        m_RangerBasicAttack.anchoredPosition = new Vector2(Screen.width / 4 + 100 - 30, 15);
                        m_MageBasicAttack.anchoredPosition = new Vector2(-Screen.width / 4 - 100 - 30, 15);

                        m_RangerSpell1.anchoredPosition = new Vector2(Screen.width / 4 + 100, 15);
                        m_MageSpell1.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 15);

                        m_RangerSpell2.anchoredPosition = new Vector2(Screen.width / 4 + 100 + 30, 15);
                        m_MageSpell2.anchoredPosition = new Vector2(-Screen.width / 4 - 100 + 30, 15);
                    }
                    else
                    {
                        m_MageInventory.anchoredPosition = new Vector2(Screen.width / 4 + 100, 0);
                        m_RangerInventory.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 0);

                        m_MageHpSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -20);
                        m_RangerHpSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -20);

                        m_MageManaSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -30);
                        m_RangerManaSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -30);

                        m_MageBasicAttack.anchoredPosition = new Vector2(Screen.width / 4 + 100 - 30, 15);
                        m_RangerBasicAttack.anchoredPosition = new Vector2(-Screen.width / 4 - 100 - 30, 15);

                        m_MageSpell1.anchoredPosition = new Vector2(Screen.width / 4 + 100, 15);
                        m_RangerSpell1.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 15);

                        m_MageSpell2.anchoredPosition = new Vector2(Screen.width / 4 + 100 + 30, 15);
                        m_RangerSpell2.anchoredPosition = new Vector2(-Screen.width / 4 - 100 + 30, 15);


                    }
                }
                else
                {
                    if (magePos.x < warriorPos.x)
                    {
                        m_WarriorInventory.anchoredPosition = new Vector2(Screen.width / 4 + 100, 0);
                        m_MageInventory.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 0);

                        m_WarriorHpSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -20);
                        m_MageHpSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -20);

                        m_WarriorManaSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -30);
                        m_MageManaSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -30);

                        m_WarriorBasicAttack.anchoredPosition = new Vector2(Screen.width / 4 + 100 - 30, 15);
                        m_MageBasicAttack.anchoredPosition = new Vector2(-Screen.width / 4 - 100 - 30, 15);

                        m_WarriorSpell1.anchoredPosition = new Vector2(Screen.width / 4 + 100, -15);
                        m_MageSpell1.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -15);

                        m_WarriorSpell2.anchoredPosition = new Vector2(Screen.width / 4 + 100 + 30, 15);
                        m_MageSpell2.anchoredPosition = new Vector2(-Screen.width / 4 - 100 + 30, 15);
                    }
                    else
                    {
                        m_MageInventory.anchoredPosition = new Vector2(Screen.width / 4 + 100, 0);
                        m_WarriorInventory.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 0);

                        m_MageHpSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -20);
                        m_WarriorHpSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -20);

                        m_MageManaSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -30);
                        m_WarriorManaSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -30);

                        m_MageBasicAttack.anchoredPosition = new Vector2(Screen.width / 4 + 100 - 30, 15);
                        m_WarriorBasicAttack.anchoredPosition = new Vector2(-Screen.width / 4 - 100 - 30, 15);

                        m_MageSpell1.anchoredPosition = new Vector2(Screen.width / 4 + 100, -15);
                        m_WarriorSpell1.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -15);

                        m_MageSpell2.anchoredPosition = new Vector2(Screen.width / 4 + 100 + 30, 15);
                        m_WarriorSpell2.anchoredPosition = new Vector2(-Screen.width / 4 - 100 + 30, 15);
                    }
                }
            }
            
            if (MenuData.m_playerNumber == 3)
            {
                // 3 player
                if (warriorPos.x < rangerPos.x && rangerPos.x < magePos.x)
                {
                    m_MageInventory.anchoredPosition = new Vector2(Screen.width / 4 + 100, 0);
                    m_WarriorInventory.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 0);
                    m_RangerInventory.anchoredPosition = new Vector2(0, 0);

                    m_MageHpSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -20);
                    m_WarriorHpSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -20);
                    m_RangerHpSlider.anchoredPosition = new Vector2(0, -20);

                    m_MageManaSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -30);
                    m_WarriorManaSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -30);
                    m_RangerManaSlider.anchoredPosition = new Vector2(0, -30);

                    m_MageBasicAttack.anchoredPosition = new Vector2(Screen.width / 4 + 100 -30, 15);
                    m_WarriorBasicAttack.anchoredPosition = new Vector2(-Screen.width / 4 - 100 -30, 15);
                    m_RangerBasicAttack.anchoredPosition = new Vector2(-30, 15);

                    m_MageSpell1.anchoredPosition = new Vector2(Screen.width / 4 + 100, 15);
                    m_WarriorSpell1.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 15);
                    m_RangerSpell1.anchoredPosition = new Vector2(0, 15);

                    m_MageSpell2.anchoredPosition = new Vector2(Screen.width / 4 + 100 + 30, -30);
                    m_WarriorSpell2.anchoredPosition = new Vector2(-Screen.width / 4 - 100 + 30, 15);
                    m_RangerSpell2.anchoredPosition = new Vector2(30, 15);




                }
                else if (warriorPos.x < magePos.x && magePos.x < rangerPos.x)
                {
                    m_RangerInventory.anchoredPosition = new Vector2(Screen.width / 4 + 100, 0);
                    m_WarriorInventory.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 0);
                    m_MageInventory.anchoredPosition = new Vector2(0, 0);

                    m_RangerHpSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -20);
                    m_WarriorHpSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -20);
                    m_MageHpSlider.anchoredPosition = new Vector2(0, -20);

                    m_RangerManaSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -30);
                    m_WarriorManaSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -30);
                    m_MageManaSlider.anchoredPosition = new Vector2(0, -30);

                    m_RangerBasicAttack.anchoredPosition = new Vector2(Screen.width / 4 + 100 - 30, 15);
                    m_WarriorBasicAttack.anchoredPosition = new Vector2(-Screen.width / 4 - 100 - 30, 15);
                    m_MageBasicAttack.anchoredPosition = new Vector2(-30, 15);

                    m_RangerSpell1.anchoredPosition = new Vector2(Screen.width / 4 + 100, 15);
                    m_WarriorSpell1.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 15);
                    m_MageSpell1.anchoredPosition = new Vector2(0, 15);

                    m_RangerSpell2.anchoredPosition = new Vector2(Screen.width / 4 + 100 + 30, -30);
                    m_WarriorSpell2.anchoredPosition = new Vector2(-Screen.width / 4 - 100 + 30, 15);
                    m_MageSpell2.anchoredPosition = new Vector2(30, 15);


                }
                else if (magePos.x < warriorPos.x && warriorPos.x < rangerPos.x)
                {
                    m_RangerInventory.anchoredPosition = new Vector2(Screen.width / 4 + 100, -20);
                    m_MageInventory.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -20);
                    m_WarriorInventory.anchoredPosition = new Vector2(0, -20);

                    m_RangerHpSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -20);
                    m_MageHpSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -20);
                    m_WarriorHpSlider.anchoredPosition = new Vector2(0, -20);

                    m_RangerManaSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -30);
                    m_MageManaSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -30);
                    m_WarriorManaSlider.anchoredPosition = new Vector2(0, -30);

                    m_RangerBasicAttack.anchoredPosition = new Vector2(Screen.width / 4 + 100 - 30, 15);
                    m_MageBasicAttack.anchoredPosition = new Vector2(-Screen.width / 4 - 100 - 30, 15);
                    m_WarriorBasicAttack.anchoredPosition = new Vector2(-30, 15);

                    m_RangerSpell1.anchoredPosition = new Vector2(Screen.width / 4 + 100, 15);
                    m_MageSpell1.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 15);
                    m_WarriorSpell1.anchoredPosition = new Vector2(0, 15);

                    m_RangerSpell2.anchoredPosition = new Vector2(Screen.width / 4 + 100 + 30, -30);
                    m_MageSpell2.anchoredPosition = new Vector2(-Screen.width / 4 - 100 + 30, 15);
                    m_WarriorSpell2.anchoredPosition = new Vector2(30, 15);

                }
                else if (magePos.x < rangerPos.x && rangerPos.x < warriorPos.x)
                {
                    m_WarriorInventory.anchoredPosition = new Vector2(Screen.width / 4 + 100, -20);
                    m_MageInventory.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -20);
                    m_RangerInventory.anchoredPosition = new Vector2(0, -20);

                    m_WarriorHpSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -20);
                    m_MageHpSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -20);
                    m_RangerHpSlider.anchoredPosition = new Vector2(0, -20);

                    m_WarriorManaSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -30);
                    m_MageManaSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -30);
                    m_RangerManaSlider.anchoredPosition = new Vector2(0, -30);

                    m_WarriorBasicAttack.anchoredPosition = new Vector2(Screen.width / 4 + 100 - 30, 15);
                    m_MageBasicAttack.anchoredPosition = new Vector2(-Screen.width / 4 - 100 - 30, 15);
                    m_RangerBasicAttack.anchoredPosition = new Vector2(-30, 15);

                    m_WarriorSpell1.anchoredPosition = new Vector2(Screen.width / 4 + 100, 15);
                    m_MageSpell1.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 15);
                    m_RangerSpell1.anchoredPosition = new Vector2(0, 15);

                    m_WarriorSpell2.anchoredPosition = new Vector2(Screen.width / 4 + 100 + 30, -30);
                    m_MageSpell2.anchoredPosition = new Vector2(-Screen.width / 4 - 100 + 30, 15);
                    m_RangerSpell2.anchoredPosition = new Vector2(30, 15);



                }
                else if (rangerPos.x < magePos.x && magePos.x < warriorPos.x)
                {
                    m_WarriorInventory.anchoredPosition = new Vector2(Screen.width / 4 + 100, 0);
                    m_RangerInventory.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 0);
                    m_MageInventory.anchoredPosition = new Vector2(0, 0);

                    m_WarriorHpSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -20);
                    m_RangerHpSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -20);
                    m_MageHpSlider.anchoredPosition = new Vector2(0, -20);

                    m_WarriorManaSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -30);
                    m_RangerManaSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -30);
                    m_MageManaSlider.anchoredPosition = new Vector2(0, -30);

                    m_WarriorBasicAttack.anchoredPosition = new Vector2(Screen.width / 4 + 100 - 30, 15);
                    m_RangerBasicAttack.anchoredPosition = new Vector2(-Screen.width / 4 - 100 - 30, 15);
                    m_MageBasicAttack.anchoredPosition = new Vector2(-30, 15);

                    m_WarriorSpell1.anchoredPosition = new Vector2(Screen.width / 4 + 100, 15);
                    m_RangerSpell1.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 15);
                    m_MageSpell1.anchoredPosition = new Vector2(0, 15);

                    m_WarriorSpell2.anchoredPosition = new Vector2(Screen.width / 4 + 100 + 30, -30);
                    m_RangerSpell2.anchoredPosition = new Vector2(-Screen.width / 4 - 100 + 30, 15);
                    m_MageSpell2.anchoredPosition = new Vector2(30, 15);



                }
                else if (rangerPos.x < warriorPos.x && warriorPos.x < magePos.x)
                {
                    m_MageInventory.anchoredPosition = new Vector2(Screen.width / 4 + 100, 0);
                    m_RangerInventory.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -20);
                    m_WarriorInventory.anchoredPosition = new Vector2(0, 0);

                    m_MageHpSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, 0);
                    m_RangerHpSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -20);
                    m_WarriorHpSlider.anchoredPosition = new Vector2(0, -20);

                    m_MageManaSlider.anchoredPosition = new Vector2(Screen.width / 4 + 100, -30);
                    m_RangerManaSlider.anchoredPosition = new Vector2(-Screen.width / 4 - 100, -30);
                    m_WarriorManaSlider.anchoredPosition = new Vector2(0, -30);

                    m_MageBasicAttack.anchoredPosition = new Vector2(Screen.width / 4 + 100 - 30, 15);
                    m_RangerBasicAttack.anchoredPosition = new Vector2(-Screen.width / 4 - 100 - 30, 15);
                    m_WarriorBasicAttack.anchoredPosition = new Vector2(-30, 15);

                    m_MageSpell1.anchoredPosition = new Vector2(Screen.width / 4 + 100, 15);
                    m_RangerSpell1.anchoredPosition = new Vector2(-Screen.width / 4 - 100, 15);
                    m_WarriorSpell1.anchoredPosition = new Vector2(0, 15);

                    m_MageSpell2.anchoredPosition = new Vector2(Screen.width / 4 + 100 + 30, -30);
                    m_RangerSpell2.anchoredPosition = new Vector2(-Screen.width / 4 - 100 + 30, 15);
                    m_WarriorSpell2.anchoredPosition = new Vector2(30, 15);

                }
            }
    }

    }
}

