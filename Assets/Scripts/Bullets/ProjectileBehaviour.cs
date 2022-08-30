using System;
using System.Collections;
using UnityEngine;

namespace Bullets
{
    public abstract class ProjectileBehaviour : MonoBehaviour
    {
        private Rigidbody _bulletRigidbody;
    
        [Header("Bullet force")]
        [Range(0,20)]
        public float shootForce;
        [Range(0,20)]
        public float upwardForce;

        public Rigidbody BulletRigidbody => _bulletRigidbody;

        private void Awake()
        {
            _bulletRigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            StartCoroutine(DestroyBullet());
        }

        private IEnumerator DestroyBullet()
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }
    }
}