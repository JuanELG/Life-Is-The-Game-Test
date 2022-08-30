using UnityEngine;

namespace Player
{
    public class PlayerData
    {
        private string _selectedAnimation;
        private GameObject _currentGun;

        public GameObject CurrentGun
        {
            get => _currentGun;
            set => _currentGun = value;
        }

        public string SelectedAnimation
        {
            get => _selectedAnimation;
            set => _selectedAnimation = value;
        }
    }
}
