using Bullets;
using UI;
using UnityEngine;

namespace Weapons
{
    public class ParabolicWeapon : Weapon
    {
        [SerializeField] private ParabolicBullet projectileBehaviour;

        protected override void Shoot()
        {
            ReadyToShoot = false;

            BulletsLeft--;
            BulletsShot++;

            //The exact hit position using raycast
            Ray pointToRay = FPSCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
            RaycastHit hit;

            //Check if ray hits something
            Vector3 targetPoint;
            if (Physics.Raycast(pointToRay, out hit))
                targetPoint = hit.point;
            else
                targetPoint = pointToRay.GetPoint(75); //Just a point far away from player
    
            //calculate direction from attack point to target point
            Vector3 attackPointPosition = attackPoint.position;
            Vector3 directionWithoutSpread = targetPoint - attackPointPosition;
            GameObject currentBullet = Instantiate(projectileBehaviour.gameObject, attackPointPosition, Quaternion.identity);
    
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * projectileBehaviour.shootForce, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddForce(FPSCamera.transform.up * projectileBehaviour.upwardForce, ForceMode.Impulse);

            //Invoke reset shot function (if not already invoked), with weapon data time between shooting value
            if (CanResetShot)
            {
                Invoke(nameof(ResetShot), weaponData.TimeBetweenShooting);
                CanResetShot = false;
            }
    
            //If more than one bullet per tap make sure to repeat shoot function
            if(BulletsShot < weaponData.BulletsPerClick && BulletsLeft > 0)
                Invoke(nameof(Shoot), weaponData.TimeBetweenShots);
            else
                GameUI.Instance.UpdateAmmunitionText(BulletsLeft / weaponData.BulletsPerClick, weaponData.MagazineSize / weaponData.BulletsPerClick);
    
        }
    }
}
