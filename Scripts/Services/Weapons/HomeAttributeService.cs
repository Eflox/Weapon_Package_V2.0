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

        public int Order => (int)AttributeOrderOptions.Always;

        private HomeAttributeConfig _config;
        private ProjectileController _projectileController;
        private GameObject _target;

        public HomeAttributeService(IWeaponAttributeConfig config)
        {
            _config = (HomeAttributeConfig)config;
        }

        public void Initialize(ProjectileController projectileController)
        {
            _projectileController = projectileController;
            IsActive = true;
        }

        public void FrameUpdate()
        {
            if (_target == null)
                _target = FindTarget();
            else
                HomeToTarget();
        }

        private GameObject FindTarget()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(_projectileController.transform.position, _config.HomingRadius);

            foreach (var hit in hits)
                if (hit.CompareTag("Enemy"))
                    return hit.gameObject;

            return null;
        }

        private void HomeToTarget()
        {
            Vector2 directionToTarget = _target.transform.position - _projectileController.transform.position;
            directionToTarget.Normalize();

            Vector2 newVelocity = Vector2.MoveTowards(_projectileController.Rigidbody2D.velocity, directionToTarget * _projectileController.Rigidbody2D.velocity.magnitude, _config.HomingAcceleration * Time.deltaTime);
            _projectileController.Rigidbody2D.velocity = newVelocity;

            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            _projectileController.transform.rotation = Quaternion.RotateTowards(_projectileController.transform.rotation, targetRotation, _projectileController.Rigidbody2D.velocity.magnitude * Time.deltaTime);
        }

        public void OnHit(GameObject collidedObject)
        {
            IsActive = false;
        }
    }
}