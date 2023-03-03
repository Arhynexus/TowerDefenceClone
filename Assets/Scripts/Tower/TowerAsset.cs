using CosmoSimClone;
using UnityEngine;

namespace TowerDefenceClone
{
    [CreateAssetMenu]

    [System.Serializable]

        public class TowerAsset: ScriptableObject
        {
        public int GoldCost = 15;
        public Sprite GUISprite;
        public Sprite Sprite;
        public TurretProperties TurretProperties;

        }
}