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
        [SerializeField] private int m_Strength = 50;
        [SerializeField] private Text m_TimeAbilityTextCost;

        private void Start()
        {
            StartCoroutine(CoolDown());
            CheckCost();
        }

        private void Update()
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
            void Slow(Enemy ship)
            {
                ship.GetComponent<SpaceShip>().ReduceMaxLinearVelocity(m_Duration, m_Strength);
            }
            IEnumerator Restore()
            {
                yield return new WaitForSeconds(m_Duration);
                EnemyWaveManager.OnEnemySpawn -= Slow;
            }
            foreach (var ship in Destructible.AllDestructibles)
            {
                ship.GetComponent<SpaceShip>().ReduceMaxLinearVelocity(m_Duration, m_Strength);
                EnemyWaveManager.OnEnemySpawn += Slow;
            }
            StartCoroutine(Restore());
            StartCoroutine(CoolDown());
        }

        public override void SetAbilityStats(UpgradeAsset Asset)
        {
            m_Duration = Upgrades.GetUpgradeLevel(Asset) * m_Duration;
            m_Strength = (int)(m_Strength + (Upgrades.GetUpgradeLevel(Asset) * m_Strength * 0.05f));
        }
        protected override void CheckCost()
        {
            switch (ResourceType)
            {
                case ResourceType.Mana:
                    m_TimeButton.interactable = AbilitiesController.Instance.CurrentMana >= m_Cost && m_IsCooldown == false;
                    m_TimeAbilityTextCost.color = Color.blue;
                    m_TimeAbilityTextCost.text = m_Cost.ToString();
                    break;

                case ResourceType.SuperMana:
                    m_TimeButton.interactable = AbilitiesController.Instance.CurrentSuperMana >= m_Cost && m_IsCooldown == false;
                    m_TimeAbilityTextCost.color = Color.yellow;
                    m_TimeAbilityTextCost.text = m_Cost.ToString();
                    break;
            }
        }
    }
}
