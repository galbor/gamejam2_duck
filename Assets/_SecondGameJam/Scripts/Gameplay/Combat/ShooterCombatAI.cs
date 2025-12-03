using System.Collections;
using UnityEngine;

namespace _SecondGameJam.Scripts.Gameplay.Both.Combat
{
    public class ShooterCombatAI : CombatAI
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _bulletSpeed = 5.0f;
        [SerializeField] private float _spawnDistance = 0.5f;
        [SerializeField] private float _bulletMass = 20f;
        [SerializeField] private float _bulletSize = 1f;
        [SerializeField] private Sprite _bulletSprite;

        private ObjectPoolManager _objectPoolManager;
        private string _objectPoolManagerName;

        private new void Start()
        {
            base.Start();
            _objectPoolManagerName = _bulletPrefab.GetComponent<Bullet>().ObjectPoolManagerName;
            _objectPoolManager = GameObject.Find(_objectPoolManagerName).GetComponent<ObjectPoolManager>();
        }

        protected override void Attack()
        {
            if (!_targetStats) return;

            Reorient();
            UpdateAttackTime();
            StartCoroutine(ShootCoroutine());
        }

        protected void Shoot(float direction, string targetTag)
        {
            GameObject bullet = _objectPoolManager.GetObjectFromPool();
            bullet.transform.position = transform.position + AngleToVector3(direction) * _spawnDistance;
            bullet.transform.rotation = Quaternion.Euler(0, 0, direction);
            var bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetAttributes(_bulletSpeed, _thisStats.Attack, targetTag, _objectPoolManager);
            bulletScript.SetMass(_bulletMass);
            bulletScript.SetSize(_bulletSize);
            bulletScript.SetSprite(_bulletSprite);
        }

        protected IEnumerator ShootCoroutine()
        {
            // Delay shot to introduce inaccuracy:
            yield return null;
            Shoot(_targeter.transform.rotation.eulerAngles.z, _targeter.TargetTag);
        }

        private static Vector3 AngleToVector3(float angle)
        {
            return Quaternion.Euler(0, 0, angle) * Vector3.up;
        }
    }
}