using System;
using TowerDefenceClone;
using UnityEngine;


namespace CosmoSimClone
{
    public enum TurretMode
    {
        Main,
        Secondary,
        Auto,
        Freeze,
        ArmorDown,
        ArmorResistanceDown,
        ShieldDown,
        BleedUp
    }
    [CreateAssetMenu]

    public sealed class TurretProperties : ScriptableObject
    {

        [SerializeField] private TurretMode m_TurretMode;
        public TurretMode TurretMode => m_TurretMode;

        [SerializeField] private Projectile m_projectilePrefab;
        public Projectile ProjectilePrefab => m_projectilePrefab;

        [SerializeField] private AssetProjectile m_assetProjectile;
        public AssetProjectile AssetProjectile => m_assetProjectile;

        [SerializeField] private float m_RateOfFire;
        public float RateOfFire => m_RateOfFire;

        [SerializeField] private int m_EnergyUsage;
        public int EnergyUsage => m_EnergyUsage;

        [SerializeField] private int m_AmmoUsage;
        public int AmmoUsage => m_AmmoUsage;

        [SerializeField] private float m_DebuffTime;
        public float DebuffTime => m_DebuffTime;

        [Tooltip("Объем снимаемой характеристики в %")]
        [SerializeField][Range(0, 100)] private float m_DebuffStrength;
        public float DebuffStrength => m_DebuffStrength;

        [SerializeField] private AudioClip m_LaunchSFX;
        public AudioClip LaunchSFX => m_LaunchSFX;
    }
}

