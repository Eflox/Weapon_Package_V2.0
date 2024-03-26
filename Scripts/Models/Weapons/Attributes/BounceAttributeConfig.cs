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
    public class BounceAttributeConfig : ScriptableObject, IWeaponAttributeConfig
    {
        [SerializeField] private int _count = 1;
        [SerializeField] private TargetFindingOptions TargetFindingOption = TargetFindingOptions.Random;

        public IWeaponAttributeService WeaponAttributeService { get; }

        public BounceAttributeConfig()
        {
            WeaponAttributeService = new BounceAttributeService(this);
        }
    }
}