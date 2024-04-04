/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 04/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using System.Linq;
using UnityEngine;

namespace Weapons
{
    public class DuplicateAttributeService : IAttributeService
    {
        private DuplicateAttributeConfig _config;

        public DuplicateAttributeService(IAttributeConfig config)
        {
            _config = (DuplicateAttributeConfig)config;
        }

        public void Initialize(ProjectileController projectileController)
        {
            float originalAngle = projectileController.transform.eulerAngles.z;

            for (int i = 0; i < _config.Count; i++)
            {
                GameObject duplicateProjectile = new GameObject($"Projectile");
                ProjectileController newProjectileController = duplicateProjectile.AddComponent<ProjectileController>();

                var attributesWithoutDuplicate = projectileController.AttributeConfigs
                    .Where(config => !(config is DuplicateAttributeConfig))
                    .ToList();

                float angleOffset = ((float)i / (_config.Count - 1) - 0.5f) * _config.Spread;
                duplicateProjectile.transform.rotation = Quaternion.Euler(0, 0, originalAngle + angleOffset);
                duplicateProjectile.transform.position = projectileController.transform.position;

                newProjectileController.Initiate(projectileController.WeaponController, attributesWithoutDuplicate);
            }
        }
    }
}