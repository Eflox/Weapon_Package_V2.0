/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 02/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "New Attribute", menuName = "Weapons/Attributes/Home")]
    public class HomeAttributeConfig : ScriptableObject, IWeaponAttributeConfig
    {
        public float HomingRadius;
        public float HomingRotationSpeed;
        public float HomingAcceleration;

        public IAttributeService CreateService() => new HomeAttributeService(this);
    }
}