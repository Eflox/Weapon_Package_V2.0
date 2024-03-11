/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 11/03/2024
 * Contact: c.dansembourg@icloud.com
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object holding the configuration of the weapon
/// </summary>
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponConfig : ScriptableObject
{
    public List<IWeaponAttribute> Attributes = new List<IWeaponAttribute>();
}