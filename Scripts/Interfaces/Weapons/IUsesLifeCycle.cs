/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 11/03/2024
 * Contact: c.dansembourg@icloud.com
 */

namespace Weapons
{
    /// <summary>
    /// Attributes that have a start and end
    /// </summary>
    public interface IUsesLifeCycle
    {
        bool IsActive();
    }
}