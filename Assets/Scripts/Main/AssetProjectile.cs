using UnityEngine;

namespace TowerDefenceClone
{
    public enum ProjectileType
    {
        [Tooltip("Простая пуля")] Standart,
        [Tooltip("Бронебойная пуля")] AP,
        [Tooltip("Взрывная пуля")] EXP,
        [Tooltip("Антищитовая пуля")] AS
    }

    public enum DamageType
    {
        [Tooltip("Магический")] Magic,
        [Tooltip("Физический")] Physical,
        [Tooltip("Пустотный")] Void,
        [Tooltip("Дефолтный")] Default
    }
    [CreateAssetMenu]
    public class AssetProjectile : ScriptableObject
    {
        [Header("Настройки пули")]
        public ProjectileType ProjectileType;
        public DamageType DamageType;
        public int Damage;
        public int DefaultDamage;
        public int StatusDamage;
        public int DefaultStatusDamage;
        /// <summary>
        /// Радиус поражения
        /// </summary>
        public float Radius;
        /// <summary>
        /// Скорость движения снаряда
        /// </summary>
        public float Velocity;
        /// <summary>
        /// Время жизни снаряда
        /// </summary>
        public float LifeTime;

        [Header("Настройки внешнего вида пули")]
        public Sprite ProjectileSprite;
        public Sprite ProjectileGUI;

        public Color Color = Color.white;
    }
}