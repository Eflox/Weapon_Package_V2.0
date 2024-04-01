/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 26/03/3034
 * Contact: c.dansembourg@icloud.com
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Weapons
{
    public class ProjectileController : MonoBehaviour
    {
        public WeaponController WeaponController;
        public Rigidbody2D Rigibody2D;

        private SpriteRenderer _spriteRenderer;

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
        }

        private void SetupAttributes(List<IWeaponAttributeConfig> attributeConfigs)
        {
            List<IAttributeService> attributeServices = new List<IAttributeService>();

            foreach (var attributeConfig in attributeConfigs)
            {
                var serviceInstance = attributeConfig.CreateService();
                attributeServices.Add(serviceInstance);
            }

            attributeServices.ForEach(attribute => attribute.Initialize(this));

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
            _spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
            _spriteRenderer.sprite = WeaponController.Config.ProjectileSprite;
            _spriteRenderer.sortingOrder = WeaponController.Config.ProjectileSortingOrder;
        }

        private void SetupCollider()
        {
            BoxCollider2D collider = this.gameObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;

            var dimensions = UtilityFunctions.GetBoxCollSizeFromSprite(_spriteRenderer);

            collider.size = dimensions.Item1;
            collider.offset = dimensions.Item2;
        }

        private void SetupRigidbody()
        {
            Rigibody2D = this.gameObject.AddComponent<Rigidbody2D>();
            Rigibody2D.gravityScale = 0;
            Rigibody2D.isKinematic = false;
            Rigibody2D.velocity = transform.right * WeaponController.Config.ProjectileSpeed;
        }

        #endregion Initiatation

        private void OnTriggerEnter2D(Collider2D obj)
        {
            if (((1 << obj.gameObject.layer) & WeaponController.Config.CollisionLayers) != 0)
                _attributesUsingOnHit.ForEach(attribute => attribute.OnHit(obj.gameObject));

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

                if (attribute is IUsesFrameUpdate)
                    _attributesUsingFrameUpdate.Remove(attribute as IUsesFrameUpdate);
                if (attribute is IUsesOnHit)
                    _attributesUsingOnHit.Remove(attribute as IUsesOnHit);
            }

            if (inactiveAttributes.Count > 0)
                Debug.Log($"Removed Attributes: {inactiveAttributes.Count}");
        }
    }
}