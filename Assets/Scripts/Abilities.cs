using CosmoSimClone;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceClone
{
    public class Abilities : SingletonBase<Abilities>
    {
        public interface IUsable { void Use() { } }
        [Serializable]
        public class FireAbility : IUsable
        {
            [SerializeField] private Color m_TargetingColor;
            [SerializeField] private float m_CoolDown = 5f;
            [SerializeField] private int m_Damage = 2;
            [SerializeField] private float m_Radius = 5;

            public void Use()
            {
                Instance.m_TargetingCircle.transform.localScale = new Vector3(m_Radius, m_Radius, m_Radius);
                Instance.m_TargetingCircle.transform.gameObject.SetActive(true);
                ClickProtection.Instance.Activate((Vector2 v) =>
                {
                    Vector3 position = v;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);
                    foreach (var collider in Physics2D.OverlapCircleAll(position, m_Radius))
                    {
                        if (collider.transform.parent.TryGetComponent<Destructible>(out var enemy))
                        {
                            enemy.ApplyDamage(m_Damage, DamageType.Default);
                        }
                        Instance.m_TargetingCircle.transform.gameObject.SetActive(false);
                    }
                });
            }

        }
        [Serializable]
        public class TimeAbility : IUsable
        {
            [SerializeField] private Button m_TimeButton;
            [SerializeField] private int m_Cost = 10;
            [SerializeField] private float m_Duration = 5;
            [SerializeField] private int m_CoolDown = 15;
            [SerializeField] private int m_Strength = 50;

            public void Use()
            {
                void Slow(Enemy ship)
                {
                    ship.GetComponent<SpaceShip>().ReduceMaxLinearVelocity(m_Duration, m_Strength);
                }
                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_Duration);
                    EnemyWaveManager.OnEnemySpawn -= Slow;
                }
                IEnumerator Cooldown()
                {
                    m_TimeButton.interactable = false;
                    yield return new WaitForSeconds(m_CoolDown);
                    m_TimeButton.interactable = true;
                }
                foreach (var ship in FindObjectsOfType<SpaceShip>())
                {
                    ship.ReduceMaxLinearVelocity(m_Duration, m_Strength);
                    EnemyWaveManager.OnEnemySpawn += Slow;
                    Instance.StartCoroutine(Restore());
                    Instance.StartCoroutine(Cooldown());
                }
            }
        }

        [SerializeField] private Image m_TargetingCircle;
        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();

        [SerializeField] private TimeAbility m_TimeAbility;
        public void UseTimeAbility() => m_TimeAbility.Use();

        private void Update()
        {
            if (Instance.m_TargetingCircle.transform.gameObject.activeSelf == true)
            {
                Instance.m_TargetingCircle.transform.position = Input.mousePosition;
            }
        }
    }
}
