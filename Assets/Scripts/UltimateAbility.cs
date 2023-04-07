using CosmoSimClone;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceClone
{
    public class UltimateAbility : Ability
    {
        [SerializeField] private int m_Damage = 500;
        [SerializeField] private Button m_UltimateAbilityButton;
        [SerializeField] private Text m_UltimateAbilityCost;


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
            foreach (var ship in Destructible.AllDestructibles)
            {
                ship.ApplyDamage(m_Damage, DamageType.Default);
                print(m_Damage);
            }
            StartCoroutine(CoolDown());
        }

        protected override void CheckCost()
        {
            switch (ResourceType)
            {
                case ResourceType.Mana:
                    m_UltimateAbilityButton.interactable = AbilitiesController.Instance.CurrentMana >= m_Cost && m_IsCooldown == false;
                    m_UltimateAbilityCost.color = Color.blue;
                    m_UltimateAbilityCost.text = m_Cost.ToString();
                    break;

                case ResourceType.SuperMana:
                    m_UltimateAbilityButton.interactable = AbilitiesController.Instance.CurrentSuperMana >= m_Cost && m_IsCooldown == false;
                    m_UltimateAbilityCost.color = Color.yellow;
                    m_UltimateAbilityCost.text = m_Cost.ToString();
                    break;
            }
        }
    }
}
