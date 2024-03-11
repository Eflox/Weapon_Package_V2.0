/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: #CREATIONDATE#
 * Contact: c.dansembourg@icloud.com
 */

namespace Weapons
{
    /// <summary>
    /// Service handling the bounce attribute
    /// </summary>
    public class BounceAttributeService : IWeaponAttributeService, IUsesLifeCycle
    {
        private bool _finished;

        public void Initialize(IWeaponAttributeConfig config)
        {
            throw new System.NotImplementedException();
        }

        public bool IsActive()
        {
            return _finished;
        }
    }
}