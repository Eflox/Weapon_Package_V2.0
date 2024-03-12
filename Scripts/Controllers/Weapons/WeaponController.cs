/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 11/03/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Controller that will handle the weapon services
    /// </summary>
    public class WeaponController : MonoBehaviour
    {
        public WeaponConfig config;

        private void Start()
        {
            new InitializeAttributesService(config.Attributes);
        }
    }
}