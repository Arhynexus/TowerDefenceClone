using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CosmoSimClone;

namespace TowerDefenceClone
{



    public class TDPlayer : Player
    {
        [SerializeField] private int m_Gold;

        public void ChangeGold(int change)
        {
            m_Gold += change;
        }
    }
}
