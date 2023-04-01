using UnityEngine;


namespace TowerDefenceClone
{
    public enum ArmorType
    {
        [Tooltip("Физическая")] Physical,
        [Tooltip("Магическая")] Magical,
        [Tooltip("Пустотная")] Void
    }
    [CreateAssetMenu]

    public sealed class EnemyAsset : ScriptableObject
    {
        
        
        [Header("Настройки внешнего вида")]
        /// <summary>
        /// Цвет спрайта
        /// </summary>
        public Color color = Color.white;
        /// <summary>
        /// Размер спрайта
        /// </summary>
        public Vector2 spriteScale = new Vector2(3, 3);
        /// <summary>
        /// Прикрепленные анимации
        /// </summary>
        public RuntimeAnimatorController animations;

        [Header("Настройки игровых параметров")]
        [Tooltip("Скорость перемещения")]
        public float MoveSpeed = 1f;
        /// <summary>
        /// Радиус коллайдера
        /// </summary>
        public float Radius = 0.19f;
        /// <summary>
        /// Объем здоровья
        /// </summary>
        public int HealthPoints = 1;
        /// <summary>
        /// Объем щитов. Без сопротивляемости
        /// </summary>
        public int ShieldPoints;
        /// <summary>
        /// Запас прочности брони
        /// </summary>
        public int ArmorPoints;
        /// <summary>
        /// Тип Брони
        /// </summary>
        public ArmorType TypeOfArmor;
        /// <summary>
        /// Сопротивляемость брони
        /// </summary>
        [Range(0,100)]public int ArmorResistance;
        /// <summary>
        /// Начисляемые очки
        /// </summary>
        public int Score = 1;

        public int Damage = 1;

        public int Gold = 1;
    }
}

