/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 03/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "New Attribute", menuName = "Weapons/Attributes/Explode")]
    public class ExplodeAttributeConfig : ScriptableObject, IWeaponAttributeConfig
    {
        public IAttributeService CreateService() => new ExplodeAttributeService(this);
    }
}