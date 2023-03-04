using CosmoSimClone;
using UnityEditor;
using UnityEngine;

namespace TowerDefenceClone
{



    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        private Turret[] turrets;
        private Destructible target;
        void Start()
        {
            turrets = GetComponentsInChildren<Turret>();
        }

        protected virtual void Update()
        {
            if (target)
            {
                Vector2 targetVector = target.transform.position - transform.position;
                if (targetVector.magnitude < m_Radius)
                {
                    foreach (var turret in turrets)
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
                    target = null;
                }
            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
                if (enter)
                {
                    target = enter.transform.root.GetComponent<Destructible>();
                }
            }
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
