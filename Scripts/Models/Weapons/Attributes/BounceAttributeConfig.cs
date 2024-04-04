/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 11/03/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Bounce attribute for weapons
    /// </summary>
    [CreateAssetMenu(fileName = "New Attribute", menuName = "Weapons/Attributes/Bounce")]
    public class BounceAttributeConfig : ScriptableObject, IAttributeConfig
    {
        public int Count;
        public TargetFindingOptions TargetFindingOption = TargetFindingOptions.Random;

        public IAttributeService CreateService() => new BounceAttributeService(this);
    }
}