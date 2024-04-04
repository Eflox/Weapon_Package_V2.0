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
        public int Order => (int)AttributeOrderOptions.Bounce;
        public bool IsActive { get; private set; }

        private ProjectileController _projectileController;
        private BounceAttributeConfig _config;
        private int _bounceCount;

        public BounceAttributeService(IAttributeConfig config)
        {
            _config = (BounceAttributeConfig)config;
        }

        public void Initialize(ProjectileController projectileController)
        {
            _projectileController = projectileController;
            IsActive = true;
            _bounceCount = _config.Count;
        }

        public void OnHit(GameObject collidedObject)
        {
            if (_bounceCount <= 0)
            {
                IsActive = false;
                return;
            }

            CalculateBounce(collidedObject);

            _bounceCount--;

            if (_bounceCount == 0)
                IsActive = false;
        }

        private void CalculateBounce(GameObject excludedObject)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy")
                            .Where(obj => obj != excludedObject)
                            .ToList();

            if (enemies.Count == 0)
            {
                IsActive = false;
                return;
            }

            GameObject newTarget = _config.TargetFindingOption switch
            {
                TargetFindingOptions.Closest => enemies
                    .Select(enemy => new { Enemy = enemy, Distance = (enemy.transform.position - _projectileController.transform.position).sqrMagnitude })
                    .OrderBy(item => item.Distance)
                    .FirstOrDefault()?.Enemy,

                TargetFindingOptions.Farthest => enemies
                    .Select(enemy => new { Enemy = enemy, Distance = (enemy.transform.position - _projectileController.transform.position).sqrMagnitude })
                    .OrderByDescending(item => item.Distance)
                    .FirstOrDefault()?.Enemy,

                TargetFindingOptions.Random => enemies.Any() ? enemies.ElementAtOrDefault(UnityEngine.Random.Range(0, enemies.Count())) : null,

                _ => null
            };

            if (newTarget != null)
            {
                Vector2 direction = newTarget.transform.position - _projectileController.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _projectileController.transform.rotation = Quaternion.Euler(0, 0, angle);

                float currentSpeed = _projectileController.Rigidbody2D.velocity.magnitude;
                _projectileController.Rigidbody2D.velocity = _projectileController.transform.right * currentSpeed;
            }
        }
    }
}