/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 03/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;
using Utils;

namespace Weapons
{
    public class ExplodeAttributeService : IAttributeService, IUsesOnHit
    {
        public int Order => (int)AttributeOrderOptions.Always;

        private ProjectileController _projectileController;
        private ExplodeAttributeConfig _config;
        private Sprite _explosionSprite;

        public ExplodeAttributeService(IWeaponAttributeConfig config)
        {
            _config = (ExplodeAttributeConfig)config;
        }

        public void Initialize(ProjectileController projectileController)
        {
            _projectileController = projectileController;
            _explosionSprite = Resources.Load<Sprite>("Weapons/Explosion");
        }

        public void OnHit(GameObject collidedObject)
        {
            GameObject explosion = new GameObject("Explosion");
            var renderer = explosion.AddComponent<SpriteRenderer>();
            renderer.sprite = _explosionSprite;

            Color spriteColor = renderer.color;
            spriteColor.a = 0.4f;
            renderer.color = spriteColor;

            var collider = explosion.AddComponent<CircleCollider2D>();
            collider.isTrigger = true;
            explosion.transform.localScale = new Vector3(_config.Radius, _config.Radius, 1f);
            explosion.transform.position = _projectileController.transform.position;

            explosion.AddComponent<DestroyAfter>().Initialize(0.05f);
        }
    }
}