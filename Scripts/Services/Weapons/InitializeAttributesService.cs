/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 11/03/2024
 * Contact: c.dansembourg@icloud.com
 */

using System.Collections.Generic;

namespace Weapons
{
    /// <summary>
    /// Service that initiliazes the attributes
    /// </summary>
    public class InitializeAttributesService
    {
        public InitializeAttributesService(List<IWeaponAttributeConfig> weaponAttributes)
        {
            foreach (var weaponAttribute in weaponAttributes)
            {
                weaponAttribute.WeaponAttributeService.Initialize();
            }
        }
    }
}