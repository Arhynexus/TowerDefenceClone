using UnityEngine;

namespace TowerDefenceClone
{

    [CreateAssetMenu]

    public sealed class UpgradeAsset : ScriptableObject
    {
        public Sprite SpriteUI;

        public int[] CostByLevel = { 3 };
    }
}

