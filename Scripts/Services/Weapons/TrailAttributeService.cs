/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 02/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Weapons
{
    public class TrailAttributeService : IAttributeService, IUsesOnDestroy
    {
        private TrailAttributeConfig _config;
        public int Order => (int)AttributeOrderOptions.Always;

        private GameObject _trail;
        private TrailController _trailController;

        public TrailAttributeService(IAttributeConfig config)
        {
            _config = (TrailAttributeConfig)config;
        }

        public void Initialize(ProjectileController projectileController)
        {
            _trail = new GameObject("TempTrail");
            _trailController = _trail.AddComponent<TrailController>();
            _trailController.Initialize(projectileController, _config);
        }

        public void OnDestroy()
        {
            _trailController.StartFading();
        }
    }
}