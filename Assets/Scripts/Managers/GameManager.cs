using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private bool _isInGame = false;
        [SerializeField] private List<GameObject> weaponsList;

        [SerializeField]
        private PlayerData _playerData = new PlayerData();

        public bool IsInGame
        {
            get => _isInGame;
            set => _isInGame = value;
        }

        public PlayerData PlayerData => _playerData;

        private void Awake () 
        {
            if(!Instance) 
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else 
                Destroy(gameObject);
        }

        public void SetSelectedAnimInPlayerData(string selectedMotion)
        {
            _playerData.SelectedAnimation = selectedMotion;
        }

        public void GoToGameScene()
        {
            _isInGame = true;
            SceneManager.LoadScene(Scenes.GAME_SCENE);
        }

        public bool CheckSelectedAnimation()
        {
            return _playerData.SelectedAnimation != null;
        }
        
        public GameObject CheckSelectedWeapon()
        {
            if(_playerData.CurrentGun != null)
                return _playerData.CurrentGun;

            return null;
        }

        public void DestroyCurrentPlayerWeapon()
        {
            Destroy(_playerData.CurrentGun);
        }

        public GameObject SetWeaponFromList(string weaponName)
        {
            foreach (GameObject weapon in weaponsList)
            {
                if (weapon.name.Equals(weaponName))
                {
                    _playerData.CurrentGun = Instantiate(weapon);
                    return _playerData.CurrentGun;
                }
            }
            return null;
        }
    }
}
