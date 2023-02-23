using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CosmoSimClone;
using UnityEditor;
using UnityEngine.UIElements;

namespace TowerDefenceClone
{


    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        public void Use (EnemyAsset asset)
        {
            var sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            sr.color = asset.color;
            sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);

            sr.GetComponent<Animator>().runtimeAnimatorController = asset.animations;

            GetComponent<SpaceShip>().Use(asset);

            CircleCollider2D collider = GetComponentInChildren<CircleCollider2D>();

            collider.radius = asset.radius;
            //collider.offset.x = 0.01f;
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
