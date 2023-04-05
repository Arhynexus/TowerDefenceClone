using CosmoSimClone;
using System;
using UnityEditor;
using UnityEngine;

namespace TowerDefenceClone
{



    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        private Turret[] m_Turrets;
        private Destructible m_Target;
        void Start()
        {
            m_Turrets = GetComponentsInChildren<Turret>();
        }

        protected virtual void Update()
        {
            if (m_Target)
            {
                Vector2 targetVector = m_Target.transform.position - transform.position;
                if (targetVector.magnitude < m_Radius)
                {
                    foreach (var turret in m_Turrets)
                    {
                        if (turret.Mode == TurretMode.Auto)
                        {
                            turret.transform.up = targetVector;
                            turret.Fire();
                        }
                        if (turret.Mode == TurretMode.Freeze)
                        {
                            var targets = Physics2D.OverlapCircleAll(transform.position, m_Radius);
                            foreach (var trgt in targets)
                            {
                                SpaceShip spaceship = trgt.transform.root.GetComponent<SpaceShip>();
                                spaceship.ReduceMaxLinearVelocity(turret.DebuffTime, turret.DebuffStrength);
                            }
                        }
                        if (turret.Mode == TurretMode.ArmorDown)
                        {
                            var targets = Physics2D.OverlapCircleAll(transform.position, m_Radius);
                            foreach (var trgt in targets)
                            {
                                SpaceShip spaceship = trgt.transform.root.GetComponent<SpaceShip>();
                                spaceship.ReduceArmor(turret.DebuffTime, turret.DebuffStrength);
                            }
                        }
                        if (turret.Mode == TurretMode.ArmorResistanceDown)
                        {
                            var targets = Physics2D.OverlapCircleAll(transform.position, m_Radius);
                            foreach (var trgt in targets)
                            {
                                SpaceShip spaceship = trgt.transform.root.GetComponent<SpaceShip>();
                                spaceship.ReduceArmorResistance(turret.DebuffTime, turret.DebuffStrength);
                            }
                        }
                        if (turret.Mode == TurretMode.ShieldDown)
                        {
                            var targets = Physics2D.OverlapCircleAll(transform.position, m_Radius);
                            foreach (var trgt in targets)
                            {
                                SpaceShip spaceship = trgt.transform.root.GetComponent<SpaceShip>();
                                spaceship.ReduceShield(turret.DebuffTime, turret.DebuffStrength);
                            }
                        }
                        if (turret.Mode == TurretMode.BleedUp)
                        {
                            var targets = Physics2D.OverlapCircleAll(transform.position, m_Radius);
                            foreach (var trgt in targets)
                            {
                                SpaceShip spaceship = trgt.transform.root.GetComponent<SpaceShip>();
                                spaceship.DamagePerSecondToHealth(turret.DebuffTime, turret.DebuffStrength);
                            }
                        }
                    }
                }
                else
                {
                    m_Target = null;
                }
            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
                if (enter)
                {
                    m_Target = enter.transform.root.GetComponent<Destructible>();
                }
            }
        }
        public void Use(TowerAsset towerAsset)
        {
            var sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.sprite = towerAsset.Sprite;
            sprite.color = towerAsset.Color;
            m_Turrets = GetComponentsInChildren<Turret>();
            foreach (var turret in m_Turrets)
            {
                turret.AssignLoadOut(towerAsset.TurretProperties);
            }
            var buildSite = GetComponentInChildren<BuildSite>();
            buildSite.SetBuildableTowers(towerAsset.UpgradesTo);
        }

#if UNITY_EDITOR


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }
#endif
    }
}
