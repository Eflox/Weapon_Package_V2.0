/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 02/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Weapons
{
    public class TrailAttributeService : IAttributeService, IUsesFrameUpdate, IUsesOnDestroy
    {
        private ProjectileController _projectileController;
        private TrailAttributeConfig _config;

        private LineRenderer _lineRenderer;
        private EdgeCollider2D _edgeCollider;
        private List<Vector2> _pathPoints = new List<Vector2>();
        private GameObject _trailHolder;

        public int Order => (int)AttributeOrderOptions.Always;

        public TrailAttributeService(IWeaponAttributeConfig config)
        {
            _config = (TrailAttributeConfig)config;
        }

        public void Initialize(ProjectileController projectileController)
        {
            _projectileController = projectileController;

            Material spriteMaterial = new Material(Shader.Find("Sprites/Default"));

            _trailHolder = new GameObject("TempTrail");

            _edgeCollider = _trailHolder.AddComponent<EdgeCollider2D>();
            _pathPoints.Add(_projectileController.transform.position);
            _edgeCollider.SetPoints(_pathPoints);

            _lineRenderer = _trailHolder.AddComponent<LineRenderer>();
            _lineRenderer.material = spriteMaterial;
            _lineRenderer.startColor = _config.Color;
            _lineRenderer.endColor = _config.Color;

            _lineRenderer.startWidth = _config.Width;
            _lineRenderer.endWidth = _config.Width;
        }

        public void FrameUpdate()
        {
            Vector2 currentPos = _projectileController.transform.position;

            const float minimumDistanceThreshold = 0.1f;
            bool shouldAddPoint = _pathPoints.Count == 0 || Vector2.Distance(_pathPoints.Last(), currentPos) > minimumDistanceThreshold;

            if (shouldAddPoint)
            {
                _pathPoints.Add(currentPos);
                _edgeCollider.SetPoints(_pathPoints);

                Vector3[] lineRendererPoints = _pathPoints.Select(point => new Vector3(point.x, point.y, 0)).ToArray();
                _lineRenderer.positionCount = lineRendererPoints.Length;
                _lineRenderer.SetPositions(lineRendererPoints);
            }
        }

        public void OnDestroy()
        {
            _trailHolder.AddComponent<DestroyAfter>().Initialize(_config.TTL);
        }
    }
}