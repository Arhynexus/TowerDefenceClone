using UnityEngine;
using CosmoSimClone;
using UnityEditor;
using System;

namespace TowerDefenceClone
{


    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int m_Damage = 1;
        [SerializeField] private int m_Gold = 1;

        public event Action OnEnd;

        private void OnDestroy() { OnEnd?.Invoke(); }

        public void Use (EnemyAsset asset)
        {
            var sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            sr.color = asset.color;
            sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);

            sr.GetComponent<Animator>().runtimeAnimatorController = asset.animations;

            SpaceShip spaceShip = GetComponent<SpaceShip>();

            spaceShip.Use(asset);
            spaceShip.SetMaxLinearVelocity(asset.MoveSpeed);


            CircleCollider2D collider = GetComponentInChildren<CircleCollider2D>();

            collider.radius = asset.Radius;

            m_Damage = asset.Damage;
            m_Gold = asset.Gold;
            //collider.offset.x = 0.01f;
        }

        public void DamagePlayer()
        {
            TDPlayer.Instance.ChangeLife(m_Damage);
        }

        public void GiveGold()
        {
            TDPlayer.Instance.ChangeGold(m_Gold);
        }

        [CustomEditor(typeof(Enemy))]

        public class EnemyInspector: Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                EnemyAsset a = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;
                Debug.Log(a);

                if(a)
                {
                    (target as Enemy).Use(a);
                }
                //GUILayout.Label("Heya");
            }
        }
    }
}
