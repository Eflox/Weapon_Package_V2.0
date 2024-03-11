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
    [CreateAssetMenu(fileName = "New Attribute", menuName = "Weapons/Weapon Attribute")]
    public class BounceAttributeConfig : ScriptableObject, IWeaponAttributeConfig
    {
        public int Count = 1;
        public TargetFindingOptions TargetFindingOption = TargetFindingOptions.Random;
        public IWeaponAttributeService WeaponAttributeService { get; }

        public BounceAttributeConfig()
        {
            WeaponAttributeService = new BounceAttributeService();
        }
    }
}