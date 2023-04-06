using UnityEngine;

namespace TowerDefenceClone
{
    public enum ResourceType
    {
        [Tooltip("Мана для обычных способностей")]Mana,
        [Tooltip("Мана для суперспособностей")]SuperMana
    }
    public abstract class Ability: MonoBehaviour
    {
        [SerializeField] protected int m_Cost;
        [SerializeField] private ResourceType m_ResourceType;
        protected bool m_IsCooldown = false;
        public ResourceType ResourceType { get { return m_ResourceType; } }
        
        public void UseAbility()
        {
            Use();
        }

        protected virtual void Use()
        {

        }

        public void OnCheckCost()
        {
            CheckCost();
        }
        protected virtual void CheckCost()
        {

        }
    }
}