using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceClone
{
    public class ImageUpdate : MonoBehaviour
    {
        public enum UpdateSource
        {
            Mana,
            SuperMana,
        }

        public UpdateSource Source = UpdateSource.Mana;

        private Image m_Image;


        void Start()
        {
            StartCoroutine(Subscribe());
        }

        private IEnumerator Subscribe()
        {
            yield return new WaitForSeconds(0.5f);
            m_Image = GetComponent<Image>();

            switch (Source)
            {
                case UpdateSource.Mana:
                    AbilitiesController.ManaChanged += UpdateImage;
                    AbilitiesController.ManaChanged();
                    print(AbilitiesController.ManaChange);
                    break;

                case UpdateSource.SuperMana:
                    AbilitiesController.SuperManaChanged += UpdateImage;
                    AbilitiesController.SuperManaChanged();
                    break;
            }
        }

        private void OnDestroy()
        {
            AbilitiesController.ManaChanged -= UpdateImage;
            AbilitiesController.SuperManaChanged -= UpdateImage;
        }

        public void UpdateImage()
        {
            switch (Source)
            {
                case UpdateSource.Mana: m_Image.fillAmount = AbilitiesController.Instance.RemainingMana;
                    break;

                case UpdateSource.SuperMana: m_Image.fillAmount = AbilitiesController.Instance.RemainingSuperMana;
                    break;
            }
        }
    }
}
