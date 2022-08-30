using Managers;
using UI;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponData weaponData;

        protected int BulletsLeft;
        protected int BulletsShot;

        private bool _shooting;
        protected bool ReadyToShoot;
        private bool _reloading;
        protected Camera FPSCamera;


        [Header("References")]
        [SerializeField]
        protected Transform attackPoint;

        protected bool CanResetShot = true;

        private void Awake()
        {
            //Se llena el cargador
            BulletsLeft = weaponData.MagazineSize;
            ReadyToShoot = true;
            
            FPSCamera = Camera.main;
        }

        private void Update()
        {
            Inputs();
        }

        private void Inputs()
        {
            //Verify whether the weapon is single shot or automatic
            if (weaponData.AllowButtonHold)
                _shooting = Input.GetKey(KeyCode.Mouse0);
            else
                _shooting = Input.GetKeyDown(KeyCode.Mouse0);
        
            //Reloading
            if(Input.GetKeyDown(KeyCode.R) && BulletsLeft < weaponData.MagazineSize && !_reloading)
                Reload();
        
            //Reload automatically when trying to shoot without ammo
            if(ReadyToShoot && _shooting && !_reloading && BulletsLeft <= 0)
                Reload();
        
            //Check if the player can shoot
            if (ReadyToShoot && _shooting && !_reloading && BulletsLeft > 0)
            {
                if (GameManager.Instance.CheckSelectedWeapon() == null)
                    return;
                //Set bullets shot to 0
                BulletsShot = 0;

                Shoot();
            }
        }

        protected abstract void Shoot();


        protected void ResetShot()
        {
            //Allow shooting and invoking again
            ReadyToShoot = true;
            CanResetShot = true;
        }

        private void Reload()
        {
            _reloading = true;
            GameUI.Instance.DisplayReloadingText();
            Invoke(nameof(ReloadFinished), weaponData.ReloadTime);
        }

        private void ReloadFinished()
        {
            GameUI.Instance.HideReloadingText();
            BulletsLeft = weaponData.MagazineSize;
            _reloading = false;
        }
    }
}
