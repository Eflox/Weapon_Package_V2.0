/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 01/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Interface for attributes that need to be initialized
    /// </summary>
    public interface IUsesProjectileInitiation
    {
        void ProjectileInitialize(GameObject projectile);
    }
}