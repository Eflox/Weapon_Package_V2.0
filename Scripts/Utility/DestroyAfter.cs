/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 03/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Utils
{
    public class DestroyAfter : MonoBehaviour
    {
        public void Initialize(float ttl) => Destroy(this.gameObject, ttl);
    }
}