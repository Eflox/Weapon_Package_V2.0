/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 11/03/2024
 * Contact: c.dansembourg@icloud.com
 */

using System.Collections.Generic;
using UnityEngine;
using Weapons;

/// <summary>
/// Scriptable Object holding the configuration of the weapon
/// </summary>
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/Weapon")]
public class WeaponConfig : ScriptableObject
{
    /// <summary>
    /// Attributes for the weapon
    /// </summary>
    public List<IWeaponAttributeConfig> Attributes = new List<IWeaponAttributeConfig>();
}