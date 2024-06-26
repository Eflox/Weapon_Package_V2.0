/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 02/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "New Attribute", menuName = "Weapons/Attributes/Trail")]
    public class TrailAttributeConfig : ScriptableObject, IAttributeConfig
    {
        public float Width;
        public Color Color;
        public float TTL;
        public float DamageTick;

        public IAttributeService CreateService() => new TrailAttributeService(this);
    }
}