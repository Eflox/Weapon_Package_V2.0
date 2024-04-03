/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 03/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Weapons
{
    public class ExplodeAttributeService : IAttributeService, IUsesOnHit
    {
        public int Order => (int)AttributeOrderOptions.Always;

        private ProjectileController _projectileController;
        private ExplodeAttributeConfig _config;

        public ExplodeAttributeService(IWeaponAttributeConfig config)
        {
            _config = (ExplodeAttributeConfig)config;
        }

        public void Initialize(ProjectileController projectileController)
        {
            _projectileController = projectileController;
        }

        public void OnHit(GameObject collidedObject)
        {
        }
    }
}