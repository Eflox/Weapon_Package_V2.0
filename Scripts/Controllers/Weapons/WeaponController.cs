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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) Fire();
        }

        private void Fire()
        {
            GameObject projectile = new GameObject($"Projectile");
            ProjectileController projectileController = projectile.AddComponent<ProjectileController>();
            projectileController.Initiate(this, Config.Attributes);
        }
    }
}