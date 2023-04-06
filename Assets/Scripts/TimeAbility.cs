using CosmoSimClone;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceClone
{
    public class TimeAbility : Ability
    {
        [SerializeField] private Button m_TimeButton;
        [SerializeField] private float m_Duration = 5;
        [SerializeField] private int m_CoolDown = 15;
        [SerializeField] private int m_Strength = 50;


        private void Start()
        {
            CheckCost();
        }
        protected override void Use()
        {
            switch (ResourceType)
            {
                case ResourceType.Mana:
                    AbilitiesController.ManaChange(m_Cost);
                    break;

                case ResourceType.SuperMana:
                    AbilitiesController.SuperManaChange(m_Cost);
                    break;
            }
            CheckCost();
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
                m_IsCooldown = true;
                yield return new WaitForSeconds(m_CoolDown);
                m_IsCooldown = false;
                CheckCost();
            }
            foreach (var ship in FindObjectsOfType<SpaceShip>())
            {
                ship.ReduceMaxLinearVelocity(m_Duration, m_Strength);
                EnemyWaveManager.OnEnemySpawn += Slow;
                StartCoroutine(Restore());
                StartCoroutine(Cooldown());
            }
        }
        protected override void CheckCost()
        {
            if (m_IsCooldown == false)
            {
                switch (ResourceType)
                {
                    case ResourceType.Mana:
                        m_TimeButton.interactable = AbilitiesController.Instance.CurrentMana >= m_Cost;
                        break;

                    case ResourceType.SuperMana:
                        m_TimeButton.interactable = AbilitiesController.Instance.CurrentSuperMana >= m_Cost;
                        break;
                }

            }
        }
    }
}
