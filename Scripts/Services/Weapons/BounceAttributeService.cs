/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 22/03/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Service handling the bounce attribute
    /// </summary>
    public class BounceAttributeService : IWeaponAttribute, IUsesInitiation, IUsesLifeCycle, IUsesFrameUpdate, IUsesOnHit
    {
        private bool _finished;
        private BounceAttributeConfig _config;

        public BounceAttributeService(IWeaponAttribute config)
        {
            _config = (BounceAttributeConfig)config;
        }

        public void Initialize()
        {
            _finished = false;
        }

        public bool IsActive()
        {
            return _finished;
        }

        public void OnHit(GameObject projectile, GameObject collidedObject)
        {
            DestroyUtility.DestroyGameObject(collidedObject);
            DestroyUtility.DestroyGameObject(projectile);
        }
    }
}