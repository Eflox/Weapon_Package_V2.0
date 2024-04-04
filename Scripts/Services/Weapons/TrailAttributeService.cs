/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 02/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;
using Utils;

namespace Weapons
{
    public class TrailAttributeService : IAttributeService, IUsesOnDestroy
    {
        private TrailAttributeConfig _config;
        public int Order => (int)AttributeOrderOptions.Always;

        private GameObject _trail;

        public TrailAttributeService(IWeaponAttributeConfig config)
        {
            _config = (TrailAttributeConfig)config;
        }

        public void Initialize(ProjectileController projectileController)
        {
            _trail = new GameObject("TempTrail");
            _trail.AddComponent<TrailController>().Initialize(projectileController, _config);
        }

        public void OnDestroy()
        {
            _trail.AddComponent<DestroyAfter>().Initialize(_config.TTL);
        }
    }
}