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
        private float _currentRotation;

        public HomeAttributeService(IAttributeConfig config)
        {
            _config = (HomeAttributeConfig)config;
        }

        public void Initialize(ProjectileController projectileController)
        {
            _projectileController = projectileController;
            IsActive = true;
            _currentRotation = 2;
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
            if (_currentRotation < 50) _currentRotation += _config.HomingAcceleration;

            Vector2 directionToTarget = (_target.transform.position - _projectileController.transform.position).normalized;
            float angleToTarget = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            Quaternion qTarget = Quaternion.AngleAxis(angleToTarget, Vector3.forward);
            _projectileController.transform.rotation = Quaternion.Slerp(_projectileController.transform.rotation, qTarget, Time.deltaTime * _currentRotation);

            float currentSpeed = _projectileController.Rigidbody2D.velocity.magnitude;
            Vector2 newVelocity = _projectileController.transform.right * currentSpeed;
            _projectileController.Rigidbody2D.velocity = newVelocity;
        }

        public void OnHit(GameObject collidedObject)
        {
            IsActive = false;
        }
    }
}