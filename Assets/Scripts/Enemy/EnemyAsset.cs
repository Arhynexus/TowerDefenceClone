using CosmoSimClone;
using UnityEngine;


namespace TowerDefenceClone
{
    
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
        /// <summary>
        /// Скорость перемещения
        /// </summary>
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
        public int ShieldPoints = 1;
        /// <summary>
        /// Запас прочности брони
        /// </summary>
        public int ArmorPoints = 1;
        /// <summary>
        /// Сопротивляемость брони
        /// </summary>
        public int ArmorResitance = 1;
        /// <summary>
        /// Начисляемые очки
        /// </summary>
        public int Score = 1;

        public int Damage = 1;

        public int Gold = 1;
    }
}

