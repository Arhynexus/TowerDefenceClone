using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{
    public class PlayerStatistics
    {
        public int Kills;
        public int Score;
        public int Time;

        public void ResetStats()
        {
            Kills = 0;
            Score = 0;
            Time = 0;
        }
    }
}
