/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 11/03/2024
 * Contact: c.dansembourg@icloud.com
 */

using System.Linq;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Controller that will handle the weapon services
    /// </summary>
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] public WeaponConfig Config;

        private void Start()
        {
            var attributesUsingInitiation = Config.Attributes.OfType<IUsesInitiation>();
            attributesUsingInitiation.OfType<IUsesInitiation>().ToList().ForEach(service => service.Initialize());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Fire();
            }
        }

        private int _projectileCount = 0;

        private void Fire()
        {
            GameObject projectile = new GameObject($"Projectile_{_projectileCount++}");
            ProjectileController projectileController = projectile.AddComponent<ProjectileController>();
            projectileController.Initiate(this);
        }

        public void OnHit(GameObject projectile, GameObject collidedObject)
        {
            var attributesUsingInitiation = Config.Attributes.OfType<IUsesOnHit>();
            attributesUsingInitiation.OfType<IUsesOnHit>().ToList().ForEach(service => service.OnHit(projectile, collidedObject));
            Debug.Log("Projectile Hit");
        }
    }
}