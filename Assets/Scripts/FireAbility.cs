using CosmoSimClone;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceClone
{
    public class FireAbility : Ability
    {
        [SerializeField] private Color m_TargetingColor;
        [SerializeField] private float m_CoolDown = 5f;
        [SerializeField] private int m_Damage = 10;
        [SerializeField] private float m_Radius = 5;
        [SerializeField] private Image m_TargetingCircle;
        [SerializeField] private Button m_FireButton;


        private void Start()
        {
            CheckCost();
        }
        private void Update()
        {
            if (m_TargetingCircle.transform.gameObject.activeSelf == true)
            {
                m_TargetingCircle.transform.position = Input.mousePosition;
            }
        }

        protected override void Use()
        {
            m_TargetingCircle.transform.localScale = new Vector3(m_Radius, m_Radius, m_Radius);
            m_TargetingCircle.transform.gameObject.SetActive(true);
            switch (ResourceType)
            {
                case ResourceType.Mana:
                    AbilitiesController.ManaChange(m_Cost);
                    break;

                case ResourceType.SuperMana:
                    AbilitiesController.SuperManaChange(m_Cost);
                    break;
            }
            ClickProtection.Instance.Activate((Vector2 v) =>
            {
                Vector3 position = v;
                position.z = -Camera.main.transform.position.z;
                position = Camera.main.ScreenToWorldPoint(position);
                foreach (var collider in Physics2D.OverlapCircleAll(position, m_Radius))
                {
                    if (collider.transform.root.TryGetComponent<Destructible>(out var enemy))
                    {
                        enemy.ApplyDamage(m_Damage, DamageType.Default);
                    }
                    StartCoroutine(Cooldown());
                    m_TargetingCircle.transform.gameObject.SetActive(false);
                }
            });
            IEnumerator Cooldown()
            {
                m_FireButton.interactable = false;
                m_IsCooldown = true;
                yield return new WaitForSeconds(m_CoolDown);
                m_IsCooldown = false;
                CheckCost();
            }
        }
        protected override void CheckCost()
        {
            if (m_IsCooldown == false)
            {
                switch (ResourceType)
                {
                    case ResourceType.Mana:
                        m_FireButton.interactable = AbilitiesController.Instance.CurrentMana >= m_Cost;
                        break;

                    case ResourceType.SuperMana:
                        m_FireButton.interactable = AbilitiesController.Instance.CurrentSuperMana >= m_Cost;
                        break;
                }

            }
        }
    }
}
