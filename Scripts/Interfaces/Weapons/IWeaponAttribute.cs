/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 11/03/2024
 * Contact: c.dansembourg@icloud.com
 */

namespace Weapons
{
    /// <summary>
    /// Interface for attributes
    /// </summary>
    public interface IWeaponAttribute
    {
        bool IsActive { get; set; }
    }
}