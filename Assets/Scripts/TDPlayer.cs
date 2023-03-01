using UnityEngine;
using CosmoSimClone;
using System;

namespace TowerDefenceClone
{



    public class TDPlayer : Player
    {
        [SerializeField] private int m_Gold;

        public static new TDPlayer Instance
        {
            get 
            { 
                return Player.Instance as TDPlayer; //Создаем инстанс TDPlayer для упрощения работы с ним
            }
        }
        
        public static event Action<int> OnGoldpdate;
        public static event Action<int> OnLifepdate;

        private void Start()
        {
            OnGoldpdate(m_Gold);
            OnLifepdate(Lives);
        }

        public void ChangeGold(int change)
        {
            m_Gold += change;
            OnGoldpdate(m_Gold);
        }

        internal void ChangeLife(int amount)
        {
            TakeDamage(amount);
            OnLifepdate(Lives);
        }
    }
}
