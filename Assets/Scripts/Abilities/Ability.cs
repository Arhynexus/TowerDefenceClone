using System.Collections;
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
        [SerializeField] protected int m_CoolDown;
        protected bool m_IsCooldown = false;
        public ResourceType ResourceType { get { return m_ResourceType; } }
        
        public void UseAbility() { Use(); }

        protected virtual void Use() { }

        public virtual void SetAbilityStats(UpgradeAsset asset) { }

        protected virtual IEnumerator CoolDown() 
        {
            m_IsCooldown = true;
            yield return new WaitForSeconds(m_CoolDown);
            m_IsCooldown = false;
        }


        public void OnCheckCost() { CheckCost(); }
        protected virtual void CheckCost() { }
    }
}