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
    public class BounceAttributeService : IAttributeService, IUsesProjectileInitiation, IUsesInitiation, IUsesLifeCycle, IUsesFrameUpdate, IUsesOnHit
    {
        private bool _isActive;
        private BounceAttributeConfig _config;
        private int _bounceCount;

        public BounceAttributeService(IWeaponAttributeConfig config)
        {
            _config = (BounceAttributeConfig)config;
        }

        public void FrameUpdate()
        {
        }

        public void Initialize()
        {
            _isActive = true;
            _bounceCount = _config.Count;
        }

        public bool IsActive()
        {
            return _isActive;
        }

        public void OnHit(GameObject projectile, GameObject collidedObject)
        {
            Debug.Log("Called Bounce Service");

            _bounceCount--;

            if (_bounceCount == 0)
                _isActive = false;

            //DestroyUtility.DestroyGameObject(collidedObject);
            //DestroyUtility.DestroyGameObject(projectile);
        }

        public void ProjectileInitialize(GameObject projectile)
        {
        }
    }
}