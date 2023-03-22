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
        
        private static event Action<int> OnGoldpdate;
        public static void OnGoldUpdateSubscribe(Action<int> act)
        {
            OnGoldpdate += act;
            act(Instance.m_Gold);
        }
        public static void OnGoldUpdateUnsubscribe(Action<int> act)
        {
            OnGoldpdate -= act;
        }
        public static event Action<int> OnLifepdate;
        public static void OnLifeUpdateSubscribe(Action<int> act)
        {
            OnLifepdate += act;
            act(Instance.Lives);
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

        [SerializeField] private Tower m_TowerPrefab;
        public void TryBuild(TowerAsset TowerAsset, Transform m_BuildSite)
        {
            ChangeGold(-TowerAsset.GoldCost);
            var tower = Instantiate(m_TowerPrefab, m_BuildSite.position, Quaternion.identity);
            tower.GetComponentInChildren<SpriteRenderer>().sprite = TowerAsset.Sprite;
            tower.GetComponentInChildren<Turret>().AssignLoadOut(TowerAsset.TurretProperties);
            Destroy(m_BuildSite.gameObject);
        }
    }
}
