/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 04/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Weapons
{
    public class TrailController : MonoBehaviour
    {
        private ProjectileController _projectileController;
        private TrailAttributeConfig _config;

        private LineRenderer _lineRenderer;
        private EdgeCollider2D _edgeCollider;
        private List<Vector2> _pathPoints = new List<Vector2>();

        private float _damageTickTimer;
        private Color _brightColor;
        private Color _dimColor;
        private float _tempDamageTick;

        public void Initialize(ProjectileController projectileController, TrailAttributeConfig config)
        {
            _projectileController = projectileController;
            _config = config;

            Material spriteMaterial = new Material(Shader.Find("Sprites/Default"));

            _edgeCollider = this.gameObject.AddComponent<EdgeCollider2D>();
            _pathPoints.Add(_projectileController.transform.position);
            _edgeCollider.SetPoints(_pathPoints);

            _lineRenderer = this.gameObject.AddComponent<LineRenderer>();
            _lineRenderer.material = spriteMaterial;
            _brightColor = _config.Color;
            _dimColor = new Color(_brightColor.r * 0.5f, _brightColor.g * 0.5f, _brightColor.b * 0.5f, _brightColor.a);

            _lineRenderer.startColor = _brightColor;
            _lineRenderer.endColor = _brightColor;

            _lineRenderer.startWidth = _config.Width;
            _lineRenderer.endWidth = _config.Width;

            _tempDamageTick = _config.DamageTick;
        }

        private void Update()
        {
            if (_projectileController != null)
            {
                Vector2 currentPos = _projectileController.transform.position;

                const float minimumDistanceThreshold = 0.3f; //LAGGG

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

            _damageTickTimer += Time.deltaTime;
            if (_damageTickTimer >= _tempDamageTick)
            {
                _damageTickTimer = 0;
                var temp = _brightColor;
                _brightColor = _dimColor;
                _dimColor = temp;
            }

            float lerpFactor = _damageTickTimer / _tempDamageTick;
            Color currentColor = Color.Lerp(_dimColor, _brightColor, lerpFactor);
            _lineRenderer.startColor = currentColor;
            _lineRenderer.endColor = currentColor;
        }

        public void StartFading()
        {
            StartCoroutine(FadeAway(0.8f, _config.TTL));
        }

        private IEnumerator FadeAway(float duration, float delayBeforeFading)
        {
            // Wait for the specified delay before starting the fade
            yield return new WaitForSeconds(delayBeforeFading);

            float currentTime = 0;

            // Capture the initial colors
            Color initialStartColor = _lineRenderer.startColor;
            Color initialEndColor = _lineRenderer.endColor;

            while (currentTime < duration)
            {
                float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);

                // Update the colors with the new alpha
                _lineRenderer.startColor = new Color(initialStartColor.r, initialStartColor.g, initialStartColor.b, alpha);
                _lineRenderer.endColor = new Color(initialEndColor.r, initialEndColor.g, initialEndColor.b, alpha);

                currentTime += Time.deltaTime;
                yield return null; // Wait until the next frame
            }

            Destroy(gameObject); // Destroy the GameObject after fading
        }
    }
}