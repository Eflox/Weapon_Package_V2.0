/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 11/03/2024
 * Contact: c.dansembourg@icloud.com
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Controller that will handle the weapon services
    /// </summary>
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] public WeaponConfig Config;

        private List<IUsesInitiation> _attributesUsingInitiations = new List<IUsesInitiation>();
        private List<IUsesLifeCycle> _attributesUsingLifeCycle = new List<IUsesLifeCycle>();
        private List<IUsesFrameUpdate> _attributesUsingFrameUpdate = new List<IUsesFrameUpdate>();
        private List<IUsesOnHit> _attributesUsingOnHit = new List<IUsesOnHit>();

        private void SetupAttributeCalls()
        {
            var attributeServices = Config.Attributes
                .Select(attr => attr.Service);

            _attributesUsingInitiations = attributeServices
                .OfType<IUsesInitiation>()
                .ToList();

            _attributesUsingLifeCycle = attributeServices
                .OfType<IUsesLifeCycle>()
                .ToList();

            _attributesUsingFrameUpdate = attributeServices
                .OfType<IUsesFrameUpdate>()
                .ToList();

            _attributesUsingOnHit = attributeServices
                .OfType<IUsesOnHit>()
                .ToList();
        }

        private void Start()
        {
            SetupAttributeCalls();

            _attributesUsingInitiations.ForEach(attribute => attribute.Initialize());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Fire();
            }
        }

        private int _projectileCount = 0;

        private void Fire()
        {
            GameObject projectile = new GameObject($"Projectile_{_projectileCount++}");
            ProjectileController projectileController = projectile.AddComponent<ProjectileController>();
            projectileController.Initiate(this);
        }

        public void OnHit(GameObject projectile, GameObject collidedObject)
        {
            var attributesUsingInitiation = Config.Attributes.OfType<IUsesOnHit>();
            attributesUsingInitiation.OfType<IUsesOnHit>().ToList().ForEach(service => service.OnHit(projectile, collidedObject));
            Debug.Log("Projectile Hit");
        }
    }
}