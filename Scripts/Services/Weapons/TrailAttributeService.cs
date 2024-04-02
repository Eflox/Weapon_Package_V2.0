/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 02/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using Unity.VisualScripting;
using UnityEngine;

namespace Weapons
{
    public class TrailAttributeService : IAttributeService, IUsesFrameUpdate
    {
        private ProjectileController _projectileController;
        private TrailAttributeConfig _config;
        private TrailRenderer _trailRenderer;

        public TrailAttributeService(IWeaponAttributeConfig config)
        {
            _config = (TrailAttributeConfig)config;
        }

        public void Initialize(ProjectileController projectileController)
        {
            _projectileController = projectileController;
            _trailRenderer = _projectileController.AddComponent<TrailRenderer>();

            Material spriteMaterial = new Material(Shader.Find("Sprites/Default"));
            _trailRenderer.material = spriteMaterial;

            _trailRenderer.startColor = _config.Color;
            _trailRenderer.endColor = _config.Color;

            _trailRenderer.startWidth = _config.Width;
            _trailRenderer.endWidth = _config.Width;

            _trailRenderer.time = _config.Time;
        }

        public void FrameUpdate()
        {
        }
    }
}