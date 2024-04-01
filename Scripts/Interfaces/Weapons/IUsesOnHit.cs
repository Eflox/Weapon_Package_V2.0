/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 26/03/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Weapons
{
    public interface IUsesOnHit
    {
        void OnHit(GameObject collidedObject);
    }
}