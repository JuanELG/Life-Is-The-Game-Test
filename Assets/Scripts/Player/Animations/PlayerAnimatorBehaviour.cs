using Managers;
using UnityEngine;

namespace Player.Animations
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimatorBehaviour : MonoBehaviour
    {
        private Animator _animatorController;

        private void Awake()
        {
            _animatorController = GetComponent<Animator>();
        }

        private void Start()
        {
            if (GameManager.Instance.IsInGame)
            {
                StartSelectedAnimation();
            }
        }

        public void StartHouseDance()
        {
            _animatorController.SetTrigger(global::Animations.HOUSE_DANCE);
            GameManager.Instance.SetSelectedAnimInPlayerData(global::Animations.HOUSE_DANCE);
        }

        public void StartMacarenaDance()
        {
            _animatorController.SetTrigger(global::Animations.MACARENA_DANCE);
            GameManager.Instance.SetSelectedAnimInPlayerData(global::Animations.MACARENA_DANCE);
        }

        public void StartHipHopDance()
        {
            _animatorController.SetTrigger(global::Animations.HIPHOP_DANCE);
            GameManager.Instance.SetSelectedAnimInPlayerData(global::Animations.HIPHOP_DANCE);
        }

        public void StartSelectedAnimation()
        {
            _animatorController.SetTrigger(GameManager.Instance.PlayerData.SelectedAnimation);
        }
    }
}