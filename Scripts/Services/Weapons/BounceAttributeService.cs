/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 22/03/2024
 * Contact: c.dansembourg@icloud.com
 */

using System.Linq;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Service handling the bounce attribute
    /// </summary>
    public class BounceAttributeService : IAttributeService, IUsesLifeCycle, IUsesOnHit
    {
        private ProjectileController _projectileController;
        private BounceAttributeConfig _config;
        private bool _isActive;
        private int _bounceCount;

        public BounceAttributeService(IWeaponAttributeConfig config)
        {
            _config = (BounceAttributeConfig)config;
        }

        public void Initialize(ProjectileController projectileController)
        {
            _projectileController = projectileController;
            _isActive = true;
            _bounceCount = _config.Count;
        }

        public bool IsActive()
        {
            return _isActive;
        }

        public void OnHit(GameObject collidedObject)
        {
            if (_bounceCount-- <= 0)
                _isActive = false;
            else
                CalculateBounce(collidedObject);
        }

        private void CalculateBounce(GameObject excludedObject)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy")
                            .Where(obj => obj != excludedObject)
                            .ToList();

            if (enemies.Count == 0) return;

            GameObject closestEnemy = null;
            float closestDistance = float.MaxValue;

            foreach (GameObject enemy in enemies)
            {
                float distance = (enemy.transform.position - _projectileController.transform.position).sqrMagnitude;
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }

            if (closestEnemy != null)
            {
                Vector2 direction = closestEnemy.transform.position - _projectileController.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _projectileController.transform.rotation = Quaternion.Euler(0, 0, angle);

                float currentSpeed = _projectileController.Rigibody2D.velocity.magnitude;
                _projectileController.Rigibody2D.velocity = _projectileController.transform.right * currentSpeed;
            }
        }
    }
}