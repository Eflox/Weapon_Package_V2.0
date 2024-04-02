/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 02/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Weapons
{
    public class HomeAttributeService : IAttributeService, IUsesLifeCycle, IUsesFrameUpdate, IUsesOnHit
    {
        public bool IsActive { get; private set; }

        public int Order => throw new System.NotImplementedException();

        private GameObject _target;

        public void Initialize(ProjectileController projectileController)
        {
            IsActive = true;
        }

        public void FrameUpdate()
        {
        }

        public void OnHit(GameObject collidedObject)
        {
            IsActive = false;
        }
    }
}