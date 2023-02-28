using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace CosmoSimClone
{



    public class EnemyUI : MonoBehaviour
    {

        [SerializeField] private GameObject[] m_ShieldBar;
        [SerializeField] private GameObject[] m_ArmorBar;
        [SerializeField] private GameObject[] m_HealthBar;
        [SerializeField] private SpaceShip m_EnemyShip;

       
        
        void Start()
        {
            SetStartEnemyHitPoints();
            m_EnemyShip.ChangeHitPoints.AddListener(UpdateEnemyHitPoints);

        }

        #region EnemyUI

        private int m_ShieldPoints;
        private float m_CurrentShieldPoints;
        private int m_ArmorPoints;
        private float m_CurrentArmorPoints;
        private int m_HealthPoints;
        private float m_CurrentHealthPoints;

        private void SetStartEnemyHitPoints()
        {
            m_ShieldPoints = m_EnemyShip.Shield;
            m_CurrentShieldPoints = ((float)m_ShieldPoints / (float)m_EnemyShip.MaxShield) * 10;
            for (int i = 0; i < m_ShieldBar.Length; i++)
            {
                m_ShieldBar[i].SetActive(true);
            }
            m_ArmorPoints = m_EnemyShip.Armor;
            m_CurrentArmorPoints = ((float)m_ArmorPoints / (float)m_EnemyShip.MaxArmor) * 10;
            for (int i = 0; i < m_ArmorBar.Length; i++)
            {
                m_ArmorBar[i].SetActive(true);
            }
            m_HealthPoints = m_EnemyShip.CurrentHealthPoints;
            m_CurrentHealthPoints = ((float)m_HealthPoints / (float)m_EnemyShip.Hitpoints) * 10;
            for (int i = 0; i < m_HealthBar.Length; i++)
            {
                m_HealthBar[i].SetActive(true);
            }
        }
        private void OnDestroy()
        {
            m_EnemyShip.ChangeHitPoints.RemoveListener(UpdateEnemyHitPoints);
        }


        private void UpdateEnemyHitPoints()
        {
            if (m_ShieldBar != null)
            {
                m_ShieldPoints = m_EnemyShip.Shield;
                m_CurrentShieldPoints = ((float)m_ShieldPoints / (float)m_EnemyShip.MaxShield) * 10;
                
                if (m_CurrentShieldPoints < 10)
                {
                    m_ShieldBar[9].SetActive(false);
                }
                else m_ShieldBar[9].SetActive(true);
                if (m_CurrentShieldPoints < 9)
                {
                    m_ShieldBar[8].SetActive(false);
                }
                else m_ShieldBar[8].SetActive(true);
                if (m_CurrentShieldPoints < 8)
                {
                    m_ShieldBar[7].SetActive(false);
                }
                else m_ShieldBar[7].SetActive(true);
                if (m_CurrentShieldPoints < 7)
                {
                    m_ShieldBar[6].SetActive(false);
                }
                else m_ShieldBar[6].SetActive(true);
                if (m_CurrentShieldPoints < 6)
                {
                    m_ShieldBar[5].SetActive(false);
                }
                else m_ShieldBar[5].SetActive(true);
                if (m_CurrentShieldPoints < 5)
                {
                    m_ShieldBar[4].SetActive(false);
                }
                else m_ShieldBar[4].SetActive(true);
                if (m_CurrentShieldPoints < 4)
                {
                    m_ShieldBar[3].SetActive(false);
                }
                else m_ShieldBar[3].SetActive(true);
                if (m_CurrentShieldPoints < 3)
                {
                    m_ShieldBar[2].SetActive(false);
                }
                else m_ShieldBar[2].SetActive(true);
                if (m_CurrentShieldPoints < 2)
                {
                    m_ShieldBar[1].SetActive(false);
                }
                else m_ShieldBar[1].SetActive(true);
                if (m_CurrentShieldPoints < 1)
                {
                    m_ShieldBar[0].SetActive(false);
                }
                else m_ShieldBar[0].SetActive(true);
            }
            
            if(m_ArmorBar != null)
            {
                m_ArmorPoints = m_EnemyShip.Armor;
                m_CurrentArmorPoints = ((float)m_ArmorPoints / (float)m_EnemyShip.MaxArmor) * 10;
                
                if (m_CurrentArmorPoints < 10)
                {
                    m_ArmorBar[9].SetActive(false);
                }
                else m_ArmorBar[9].SetActive(true);
                if (m_CurrentArmorPoints < 9)
                {
                    m_ArmorBar[8].SetActive(false);
                }
                else m_ArmorBar[8].SetActive(true);
                if (m_CurrentArmorPoints < 8)
                {
                    m_ArmorBar[7].SetActive(false);
                }
                else m_ArmorBar[7].SetActive(true);
                if (m_CurrentArmorPoints < 7)
                {
                    m_ArmorBar[6].SetActive(false);
                }
                else m_ArmorBar[6].SetActive(true);
                if (m_CurrentArmorPoints < 6)
                {
                    m_ArmorBar[5].SetActive(false);
                }
                else m_ArmorBar[5].SetActive(true);
                if (m_CurrentArmorPoints < 5)
                {
                    m_ArmorBar[4].SetActive(false);
                }
                else m_ArmorBar[4].SetActive(true);
                if (m_CurrentArmorPoints < 4)
                {
                    m_ArmorBar[3].SetActive(false);
                }
                else m_ArmorBar[3].SetActive(true);
                if (m_CurrentArmorPoints < 3)
                {
                    m_ArmorBar[2].SetActive(false);
                }
                else m_ArmorBar[2].SetActive(true);
                if (m_CurrentArmorPoints < 2)
                {
                    m_ArmorBar[1].SetActive(false);
                }
                else m_ArmorBar[1].SetActive(true);
                if (m_CurrentArmorPoints < 1)
                {
                    m_ArmorBar[0].SetActive(false);
                }
                else m_ArmorBar[0].SetActive(true);
            }
            
            if(m_HealthBar != null)
            {
                m_HealthPoints = m_EnemyShip.CurrentHealthPoints;
                m_CurrentHealthPoints = ((float)m_HealthPoints / (float)m_EnemyShip.Hitpoints) * 10;
                
                if (m_CurrentHealthPoints < 10)
                {
                    m_HealthBar[9].SetActive(false);
                }
                else m_HealthBar[9].SetActive(true);
                if (m_CurrentHealthPoints < 9)
                {
                    m_HealthBar[8].SetActive(false);
                }
                else m_HealthBar[8].SetActive(true);
                if (m_CurrentHealthPoints < 8)
                {
                    m_HealthBar[7].SetActive(false);
                }
                else m_HealthBar[7].SetActive(true);
                if (m_CurrentHealthPoints < 7)
                {
                    m_HealthBar[6].SetActive(false);
                }
                else m_HealthBar[6].SetActive(true);
                if (m_CurrentHealthPoints < 6)
                {
                    m_HealthBar[5].SetActive(false);
                }
                else m_HealthBar[5].SetActive(true);
                if (m_CurrentHealthPoints < 5)
                {
                    m_HealthBar[4].SetActive(false);
                }
                else m_HealthBar[4].SetActive(true);
                if (m_CurrentHealthPoints < 4)
                {
                    m_HealthBar[3].SetActive(false);
                }
                else m_HealthBar[3].SetActive(true);
                if (m_CurrentHealthPoints < 3)
                {
                    m_HealthBar[2].SetActive(false);
                }
                else m_HealthBar[2].SetActive(true);
                if (m_CurrentHealthPoints < 2)
                {
                    m_HealthBar[1].SetActive(false);
                }
                else m_HealthBar[1].SetActive(true);
                if (m_CurrentHealthPoints < 1)
                {
                    m_HealthBar[0].SetActive(false);
                }
                else m_HealthBar[0].SetActive(true);
            }
        }

        #endregion

        private void Update()
        {

        }

        //private bool DisplayShiledPpoints(float shield, int pointnumber)
        //{
        //    return ((pointnumber * 10) >= shield);
        //}
    }
}