/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 02/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class PierceAttributeService : IAttributeService, IUsesLifeCycle, IUsesOnHit
    {
        public int Order => (int)AttributeOrderOptions.Pierce;
        public bool IsActive { get; private set; }

        private PierceAttributeConfig _config;
        private ProjectileController _projectileController;
        private int _pierceCount;

        public PierceAttributeService(IAttributeConfig config)
        {
            _config = (PierceAttributeConfig)config;
        }

        public void Initialize(ProjectileController projectileController)
        {
            _projectileController = projectileController;
            IsActive = true;
            _pierceCount = _config.Count;
        }

        public void OnHit(GameObject collidedObject)
        {
            if (_pierceCount <= 0)
            {
                IsActive = false;
                return;
            }

            CalculatePierce();

            _pierceCount--;

            if (_pierceCount == 0)
                IsActive = false;
        }

        private void CalculatePierce()
        {
            Debug.Log("Pierce");

            var currentSpeed = _projectileController.Rigidbody2D.velocity.magnitude;
            var randomDeflection = Random.Range(-_config.Deflection, _config.Deflection);
            _projectileController.transform.Rotate(0, 0, randomDeflection);
            var newDirection = _projectileController.transform.right * Mathf.Sign(currentSpeed);
            _projectileController.Rigidbody2D.velocity = newDirection.normalized * currentSpeed;
        }
    }
}