/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 26/03/3034
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Weapons
{
    public class ProjectileController : MonoBehaviour
    {
        public WeaponController WeaponController;

        public void Initiate(WeaponController weaponController)
        {
            WeaponController = weaponController;

            SpriteRenderer spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = WeaponController.Config.ProjectileSprite;
            spriteRenderer.sortingOrder = WeaponController.Config.ProjectileSortingOrder;

            BoxCollider2D collider = this.gameObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;

            Rigidbody2D rb = this.gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.isKinematic = false;
            rb.velocity = transform.right * WeaponController.Config.ProjectileSpeed;
        }

        private void OnTriggerEnter2D(Collider2D obj)
        {
            if (((1 << obj.gameObject.layer) & WeaponController.Config.CollisionLayers) != 0)
                WeaponController.OnHit(this.gameObject, obj.gameObject);

        }
    }
}