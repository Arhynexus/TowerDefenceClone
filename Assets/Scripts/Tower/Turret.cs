using System.Collections;
using System.Collections.Generic;
using TowerDefenceClone;
using UnityEngine;

namespace CosmoSimClone
{
    public class Turret : MonoBehaviour
    {

        private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties;
        [SerializeField] private AssetProjectile m_AssetProjectile;

        private float m_RefireTimer;
        public bool CanFire => m_RefireTimer <= 0;

        private SpaceShip m_Ship;

        [SerializeField] private AudioSource m_AudioSource;

        public float DebuffTime { get; private set; }
        public float DebuffStrength { get; private set; }


        private void Start()
        {
            m_Ship = transform.root.GetComponent<SpaceShip>();
            AssignLoadOut(m_TurretProperties);
        }


        private void Update()
        {
            if (m_RefireTimer > 0) m_RefireTimer -= Time.deltaTime;
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
            projectile.Use(m_AssetProjectile);
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            m_RefireTimer = m_TurretProperties.RateOfFire;
        }

        public void AssignLoadOut(TurretProperties props)
        {
            m_RefireTimer = 0;
            m_TurretProperties = props;
            m_AssetProjectile = props.AssetProjectile;
            m_Mode = props.TurretMode;
            DebuffTime = props.DebuffTime;
            DebuffStrength = props.DebuffStrength;
        }
    }
}


