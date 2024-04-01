/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 26/03/3034
 * Contact: c.dansembourg@icloud.com
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Weapons
{
    public class ProjectileController : MonoBehaviour
    {
        public WeaponController WeaponController;

        private List<IUsesInitiation> _attributesUsingInitiation = new List<IUsesInitiation>();
        private List<IUsesLifeCycle> _attributesUsingLifeCycle = new List<IUsesLifeCycle>();
        private List<IUsesFrameUpdate> _attributesUsingFrameUpdate = new List<IUsesFrameUpdate>();
        private List<IUsesOnHit> _attributesUsingOnHit = new List<IUsesOnHit>();

        #region Initiatation

        public void Initiate(WeaponController weaponController, List<IWeaponAttributeConfig> attributeConfigs)
        {
            WeaponController = weaponController;

            SetupAttributes(attributeConfigs);
            SetupSpriteRenderer();
            SetupCollider();
            SetupRigidbody();

            _attributesUsingInitiation.ForEach(attribute => attribute.Initialize());
        }

        private void SetupAttributes(List<IWeaponAttributeConfig> attributeConfigs)
        {
            List<IAttributeService> attributeServices = new List<IAttributeService>();

            foreach (var attributeConfig in attributeConfigs)
            {
                var serviceInstance = attributeConfig.CreateService(); // This method needs to be defined in your interface and implemented in your configs
                attributeServices.Add(serviceInstance);
            }

            _attributesUsingInitiation = attributeServices
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

        private void SetupSpriteRenderer()
        {
            SpriteRenderer spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = WeaponController.Config.ProjectileSprite;
            spriteRenderer.sortingOrder = WeaponController.Config.ProjectileSortingOrder;
        }

        private void SetupCollider()
        {
            BoxCollider2D collider = this.gameObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;
        }

        private void SetupRigidbody()
        {
            Rigidbody2D rb = this.gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.isKinematic = false;
            rb.velocity = transform.right * WeaponController.Config.ProjectileSpeed;
        }

        #endregion Initiatation

        private void OnTriggerEnter2D(Collider2D obj)
        {
            if (((1 << obj.gameObject.layer) & WeaponController.Config.CollisionLayers) != 0)
                _attributesUsingOnHit.ForEach(attribute => attribute.OnHit(this.gameObject, obj.gameObject));

            if (_attributesUsingLifeCycle.Count > 0)
                RemoveNonActiveAttributeServices();
        }

        private void Update()
        {
            _attributesUsingFrameUpdate.ForEach(attribute => attribute.FrameUpdate());
        }

        private void RemoveNonActiveAttributeServices()
        {
            var inactiveAttributes = _attributesUsingLifeCycle.Where(attribute => !attribute.IsActive()).ToList();

            foreach (var attribute in inactiveAttributes)
            {
                _attributesUsingLifeCycle.Remove(attribute);

                if (attribute is IUsesInitiation)
                    _attributesUsingInitiation.Remove(attribute as IUsesInitiation);
                if (attribute is IUsesFrameUpdate)
                    _attributesUsingFrameUpdate.Remove(attribute as IUsesFrameUpdate);
                if (attribute is IUsesOnHit)
                    _attributesUsingOnHit.Remove(attribute as IUsesOnHit);
            }

            Debug.Log($"Removed Attributes: {inactiveAttributes.Count}");
        }
    }
}