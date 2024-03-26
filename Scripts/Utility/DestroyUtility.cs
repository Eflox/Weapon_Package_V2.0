/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: #CREATIONDATE#
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Weapons
{
    public static class DestroyUtility
    {
        public static void DestroyGameObject(GameObject gameObjectToDestroy)
        {
            if (gameObjectToDestroy != null)
                Object.Destroy(gameObjectToDestroy);
        }
    }
}