/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 11/03/2024
 * Contact: c.dansembourg@icloud.com
 */

using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public static class WeaponAttributeSyncUtility
    {
        public static void SyncAttributes(List<IWeaponAttributeConfig> targetList, List<ScriptableObject> sourceList)
        {
            targetList.Clear();

            List<ScriptableObject> itemsToRemove = new List<ScriptableObject>();

            foreach (var attribute in sourceList)
            {
                if (attribute is IWeaponAttributeConfig config)
                {
                    targetList.Add(config);
                    Debug.Log($"Attribute {attribute.name} added");
                }
                else if (attribute != null)
                    itemsToRemove.Add(attribute);
            }

            foreach (var item in itemsToRemove)
            {
                sourceList.Remove(item);
                Debug.LogWarning($"Removed {item.name} not a weapon attribute");
            }
        }
    }
}