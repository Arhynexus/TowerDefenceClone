using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{
    public class Turret : MonoBehaviour
    {

        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties;

        private float m_RefireTimer;

        public bool CanFire => m_RefireTimer <= 0;

        private SpaceShip m_Ship;

        [SerializeField] private AudioSource m_AudioSource;


        void Start()
        {
            m_Ship = transform.root.GetComponent<SpaceShip>();
        }


        void Update()
        {
            if (m_RefireTimer > 0) m_RefireTimer -= Time.deltaTime;
            else if (m_Mode == TurretMode.Auto) Fire();
        }

        // Public API

        public void Fire()
        {
            if (m_TurretProperties == null) return;

            if (m_RefireTimer > 0) return;
            if (m_Ship)
            {
                if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) return;
                if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) return;
            }

            


            if (m_AudioSource != null)
            {
                m_AudioSource.Play();
            }
            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.SetParentShooter(m_Ship);
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            

            // Add Consumption of Ammo and Energy

            m_RefireTimer = m_TurretProperties.RateOfFire;

            // Add play sound for turret
        }

        public void AssignLoadOut(TurretProperties props)
        {
            if(m_Mode != props.TurretMode) return;
            m_RefireTimer = 0;
            m_TurretProperties = props;
        }
    }
}


