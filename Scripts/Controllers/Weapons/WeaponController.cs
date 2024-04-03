/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 11/03/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Controller that will handle the weapon services
    /// </summary>
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] public WeaponConfig Config;

        private float _lastFireTime = -1.5f;

        private void Start()
        {
            _lastFireTime = -Config.FireRate;
        }

        private void Update()
        {
            if (Time.time - _lastFireTime >= Config.FireRate)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    Fire();
                    _lastFireTime = Time.time;
                }
            }
        }

        private void Fire()
        {
            GameObject projectile = new GameObject($"Projectile");
            ProjectileController projectileController = projectile.AddComponent<ProjectileController>();

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector2 direction = mousePosition - projectile.transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

            projectileController.Initiate(this, Config.Attributes);
        }
    }
}