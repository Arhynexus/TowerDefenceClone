using CosmoSimClone;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceClone
{
    public class FireAbility : Ability
    {
        [SerializeField] private Color m_TargetingColor;
        [SerializeField] private int m_Damage = 10;
        [SerializeField] private float m_Radius = 3;
        [SerializeField] private Image m_TargetingCircle;
        [SerializeField] private Button m_FireButton;
        [SerializeField] private Text m_FireAbilityTextCost;


        private void Start()
        {
            StartCoroutine(CoolDown());
            CheckCost();
            m_TargetingCircle.color = m_TargetingColor;
            m_TargetingCircle.transform.localScale = new Vector3(m_Radius, m_Radius, m_Radius);
        }

        public override void SetAbilityStats(UpgradeAsset Asset)
        {
            m_Damage = Upgrades.GetUpgradeLevel(Asset) * m_Damage;
            m_Radius = Upgrades.GetUpgradeLevel(Asset) * m_Radius;
        }
        private void Update()
        {
            if (m_TargetingCircle.transform.gameObject.activeSelf == true)
            {
                m_TargetingCircle.transform.position = Input.mousePosition;
            }
            CheckCost();
        }

        protected override void Use()
        {
            m_TargetingCircle.transform.gameObject.SetActive(true);
            ClickProtection.Instance.Activate((Vector2 v) =>
            {
                Vector3 position = v;
                position.z = -Camera.main.transform.position.z;
                position = Camera.main.ScreenToWorldPoint(position);
                switch (ResourceType)
                {
                    case ResourceType.Mana:
                        AbilitiesController.ManaChange(m_Cost);
                        break;

                    case ResourceType.SuperMana:
                        AbilitiesController.SuperManaChange(m_Cost);
                        break;
                }
                foreach (var collider in Physics2D.OverlapCircleAll(position, m_Radius))
                {
                    if (collider.transform.root.TryGetComponent<Destructible>(out var enemy))
                    {
                        enemy.ApplyDamage(m_Damage, DamageType.Default);
                        print(m_Damage);
                    }
                }
                    StartCoroutine(CoolDown());
                m_TargetingCircle.transform.gameObject.SetActive(false);
            });
        }
        protected override void CheckCost()
        {
            switch (ResourceType)
            {
                case ResourceType.Mana:
                    m_FireButton.interactable = AbilitiesController.Instance.CurrentMana >= m_Cost && m_IsCooldown == false;
                    m_FireAbilityTextCost.color = Color.blue;
                    m_FireAbilityTextCost.text = m_Cost.ToString();
                    break;

                case ResourceType.SuperMana:
                    m_FireButton.interactable = AbilitiesController.Instance.CurrentSuperMana >= m_Cost && m_IsCooldown == false;
                    m_FireAbilityTextCost.color = Color.yellow;
                    m_FireAbilityTextCost.text = m_Cost.ToString();
                    break;
            }
        }
    }
}
