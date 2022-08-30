using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameUI : MonoBehaviour
    {
        public static GameUI Instance;

        [SerializeField] private TextMeshProUGUI ammunitionText;
        [SerializeField] private TextMeshProUGUI pickUpTutorial;
        [SerializeField] private TextMeshProUGUI reloadingWeaponText;
        
        private void Awake () 
        {
            if(!Instance) 
            {
                Instance = this;
            }
            else 
                Destroy(gameObject);
        }

        private void Start()
        {
            pickUpTutorial.transform.localScale = Vector3.zero;
        }

        public void UpdateAmmunitionText(int bulletsCount, int magazineSize)
        {
            if (ammunitionText != null)
            {
                ammunitionText.SetText($"{bulletsCount} / {magazineSize}");
            }
        }

        public void DisplayPickUpTutorial()
        {
            GameObject tutorialGameObject = pickUpTutorial.gameObject;
            tutorialGameObject.SetActive(true);
            LeanTween.scale(tutorialGameObject, Vector3.one, 0.5f);
        }

        public void HidePickUpTutorial()
        {
            StartCoroutine(HideTutorialCoroutine());
        }

        private IEnumerator HideTutorialCoroutine()
        {
            LeanTween.scale(pickUpTutorial.gameObject, Vector3.zero, 0.5f);
            yield return new WaitForSeconds(0.5f);
            pickUpTutorial.gameObject.SetActive(false);
        }
        
        public void DisplayReloadingText()
        {
            GameObject ReloadingGameObject = reloadingWeaponText.gameObject;
            ReloadingGameObject.SetActive(true);
            LeanTween.scale(ReloadingGameObject, Vector3.one, 0.5f);
        }

        public void HideReloadingText()
        {
            StartCoroutine(HideReloadingTextCoroutine());
        }

        private IEnumerator HideReloadingTextCoroutine()
        {
            LeanTween.scale(reloadingWeaponText.gameObject, Vector3.zero, 0.5f);
            yield return new WaitForSeconds(0.5f);
            reloadingWeaponText.gameObject.SetActive(false);
        }
    }
}
