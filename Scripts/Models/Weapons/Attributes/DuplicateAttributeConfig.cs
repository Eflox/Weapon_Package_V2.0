/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 04/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "New Attribute", menuName = "Weapons/Attributes/Duplicate")]
    public class DuplicateAttributeConfig : ScriptableObject, IAttributeConfig
    {
        public int Count;
        public float Spread;

        public IAttributeService CreateService() => new DuplicateAttributeService(this);
    }
}