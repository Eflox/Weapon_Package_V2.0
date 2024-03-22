/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 22/03/2024
 * Contact: c.dansembourg@icloud.com
 */

namespace Weapons
{
    /// <summary>
    /// Service handling the bounce attribute
    /// </summary>
    public class BounceAttributeService : IWeaponAttributeService, IUsesLifeCycle, IUsesInitiation
    {
        private bool _finished;
        private BounceAttributeConfig _config;

        public BounceAttributeService(IWeaponAttributeConfig config)
        {
            _config = (BounceAttributeConfig)config;
        }

        public void Initialize()
        {
            _finished = false;
        }

        public bool IsActive()
        {
            return _finished;
        }
    }
}