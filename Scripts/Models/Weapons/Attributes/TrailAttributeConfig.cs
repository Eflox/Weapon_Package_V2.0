/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 02/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "New Attribute", menuName = "Weapons/Attributes/Trail")]
    public class TrailAttributeConfig : ScriptableObject, IWeaponAttributeConfig
    {
        public float Width;
        public Color Color;
        public float Time;

        public IAttributeService CreateService() => new TrailAttributeService(this);
    }
}