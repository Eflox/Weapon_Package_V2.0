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
        public Rigidbody2D Rigidbody2D;
        public SpriteRenderer SpriteRenderer;
        public List<IAttributeConfig> AttributeConfigs = new List<IAttributeConfig>();

        private List<IUsesLifeCycle> _attributesUsingLifeCycle = new List<IUsesLifeCycle>();
        private List<IUsesFrameUpdate> _attributesUsingFrameUpdate = new List<IUsesFrameUpdate>();
        private List<IUsesOnHit> _attributesUsingOnHit = new List<IUsesOnHit>();
        private List<IUsesOnDestroy> _attributesUsingOnDestroy = new List<IUsesOnDestroy>();

        #region Initiatation

        public void Initiate(WeaponController weaponController, List<IAttributeConfig> attributeConfigs)
        {
            WeaponController = weaponController;
            AttributeConfigs = attributeConfigs;

            SetupSpriteRenderer();
            SetupCollider();
            SetupRigidbody();
            SetupAttributes();
        }

        private void SetupAttributes()
        {
            List<IAttributeService> attributeServices = new List<IAttributeService>();

            foreach (var attributeConfig in AttributeConfigs)
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

            _attributesUsingOnDestroy = attributeServices
                .OfType<IUsesOnDestroy>()
                    .ToList();
        }

        private void SetupSpriteRenderer()
        {
            SpriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
            SpriteRenderer.sprite = WeaponController.Config.ProjectileSprite;
            SpriteRenderer.sortingOrder = WeaponController.Config.ProjectileSortingOrder;
        }

        private void SetupCollider()
        {
            BoxCollider2D collider = this.gameObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;

            var dimensions = UtilityFunctions.GetBoxCollSizeFromSprite(SpriteRenderer);

            collider.size = dimensions.Item1;
            collider.offset = dimensions.Item2;
        }

        private void SetupRigidbody()
        {
            Rigidbody2D = this.gameObject.AddComponent<Rigidbody2D>();
            Rigidbody2D.gravityScale = 0;
            Rigidbody2D.isKinematic = false;
            Rigidbody2D.velocity = transform.right * WeaponController.Config.ProjectileSpeed;
        }

        #endregion Initiatation

        private void OnTriggerEnter2D(Collider2D obj)
        {
            if (((1 << obj.gameObject.layer) & WeaponController.Config.CollisionLayers) == 0)
                return;

            if (!_attributesUsingOnHit.Any(attribute => attribute.Order > 0))
            {
                DestroyProjectile();
                return;
            }

            _attributesUsingOnHit
                .Where(attribute => attribute.Order == 0)
                .ToList()
                .ForEach(attribute => attribute.OnHit(obj.gameObject));

            _attributesUsingOnHit
                .Where(attribute => attribute.Order > 0)
                .OrderBy(attribute => attribute.Order)
                .FirstOrDefault()?
                .OnHit(obj.gameObject);

            if (_attributesUsingLifeCycle.Count > 0)
                RemoveNonActiveAttributeServices();
        }

        private void Update()
        {
            _attributesUsingFrameUpdate.ForEach(attribute => attribute.FrameUpdate());

            CheckHitScreenEdge();
        }

        private void RemoveNonActiveAttributeServices()
        {
            var inactiveAttributes = _attributesUsingLifeCycle.Where(attribute => !attribute.IsActive).ToList();

            foreach (var attribute in inactiveAttributes)
            {
                _attributesUsingLifeCycle.Remove(attribute);

                if (attribute is IUsesFrameUpdate)
                    _attributesUsingFrameUpdate.Remove(attribute as IUsesFrameUpdate);
                if (attribute is IUsesOnHit)
                    _attributesUsingOnHit.Remove(attribute as IUsesOnHit);
            }
        }

        private void CheckHitScreenEdge()
        {
            Vector2 normal = Vector2.zero;
            Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

            if (viewportPosition.x < 0) normal = Vector2.right;
            else if (viewportPosition.x > 1) normal = Vector2.left;

            if (viewportPosition.y < 0) normal = Vector2.up;
            else if (viewportPosition.y > 1) normal = Vector2.down;

            if (normal != Vector2.zero)
                DestroyProjectile();
        }

        private void DestroyProjectile()
        {
            _attributesUsingOnDestroy.ForEach(attribute => attribute.OnDestroy());
            Destroy(this.gameObject);
        }
    }
}