using Bullets;
using UnityEngine;

namespace Weapons
{
    public class MagneticEffectWeapon : Weapon
    {
        [SerializeField] private MagneticBullet projectileBehaviour;
    
        protected override void Shoot()
        {
            //TODO: Create the magnetic effect in the weapon shot
            throw new System.NotImplementedException();
        }
    }
}