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
        public float moveSpeed = 1f;
        /// <summary>
        /// Радиус коллайдера
        /// </summary>
        public float radius = 0.19f;
        /// <summary>
        /// Объем здоровья
        /// </summary>
        public int healthPoints = 1;
        /// <summary>
        /// Объем щитов. Без сопротивляемости
        /// </summary>
        public int shieldPoints = 1;
        /// <summary>
        /// Запас прочности брони
        /// </summary>
        public int armorPoints = 1;
        /// <summary>
        /// Сопротивляемость брони
        /// </summary>
        public int armorResitance = 1;
        /// <summary>
        /// Начисляемые очки
        /// </summary>
        public int score = 1;
    }
}

