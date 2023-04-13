using CosmoSimClone;
using Unity.Mathematics;
using UnityEngine;

namespace TowerDefenceClone
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        private Turret[] m_Turrets;
        private Destructible m_Target;
        [SerializeField] private GameObject m_VisualEffects;
        [SerializeField] private bool m_IsPlayed;
        private ParticleSystem m_Particles;
        void Start()
        {
            m_Turrets = GetComponentsInChildren<Turret>();
            m_VisualEffects.SetActive(false);
            if (m_Particles != null)
            {
                var particles = Instantiate(m_Particles, m_VisualEffects.transform.position, quaternion.identity);
                particles.transform.SetParent(m_VisualEffects.transform, false);
                particles.transform.position = m_VisualEffects.transform.position;
            }
        }

        private void PlayEffects()
        {
            if (m_IsPlayed == true)
            {
                m_VisualEffects.SetActive(true);
            }
            else
            {
                m_VisualEffects.SetActive(false);
            }
        }

        protected virtual void Update()
        {
            PlayEffects();
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
                            m_IsPlayed = true;
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
                    m_IsPlayed = false;
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
            m_Particles = transform.root.GetComponentInChildren<ParticleSystem>();
            m_Particles = towerAsset.VisualEffect;
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
