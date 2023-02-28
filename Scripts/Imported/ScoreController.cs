using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using TMPro;

namespace CosmoSimClone
{



    public class ScoreController : SingletonBase<ScoreController>
    {
        [SerializeField] private Player m_player;

        public int CurrentScore { get; private set; }
        
        private int m_MaxScore;

        public int Kills { get; private set; }

        private int m_PlayerTeamID;

        public UnityEvent ChangePlayerScore;

        
        void Start()
        {
            CurrentScore = 0;
            Kills = 0;
            ChangePlayerScore.Invoke();
        }

        

        public void AddScore(int amount, int teamID)
        {
            m_PlayerTeamID = m_player.ActiveShip.TeamId;
            if (teamID != m_PlayerTeamID)
            {
                CurrentScore += amount;
                if (CurrentScore >= m_MaxScore)
                {
                    m_MaxScore = CurrentScore;
                }
                ChangePlayerScore.Invoke();
            }
            else return;
        }

        public void AddKill(int teamID)
        {
            m_PlayerTeamID = m_player.ActiveShip.TeamId;
            if (teamID != m_PlayerTeamID)
            {
                Kills++;
                ChangePlayerScore.Invoke();
            }
            else return;
        }

        

        
    }
}
