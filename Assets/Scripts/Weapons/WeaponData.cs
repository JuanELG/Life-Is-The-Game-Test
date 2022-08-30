using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "NewWeaponData", menuName = "New Weapon")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private string weaponName;
        [SerializeField] private string description;
        [SerializeField] private int damage;

        [Header("Weapon stats")]
        [Range(0,20)]
        [SerializeField] private float timeBetweenShooting;
        [Range(0,20)]
        [SerializeField] private float spread;
        [Range(0,20)]
        [SerializeField] private float reloadTime;
        [Range(0,20)]
        [SerializeField] private float timeBetweenShots;
        [Range(0,20)]
        [SerializeField] private int magazineSize;
        [Range(0,20)]
        [SerializeField] private int bulletsPerClick;
        [SerializeField] private bool allowButtonHold;

        public string WeaponName => weaponName;

        public string Description => description;

        public int Damage => damage;
        
        public float TimeBetweenShooting => timeBetweenShooting;

        public float Spread => spread;

        public float ReloadTime => reloadTime;

        public float TimeBetweenShots => timeBetweenShots;

        public int MagazineSize => magazineSize;

        public int BulletsPerClick => bulletsPerClick;

        public bool AllowButtonHold => allowButtonHold;
    }
}