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
    [SerializeField] private List<ScriptableObject> _attributesInspector = new List<ScriptableObject>();

    public List<IAttributeConfig> Attributes = new List<IAttributeConfig>();

    [Min(0.01f)] public float FireRate = 1;
    [Min(0.01f)] public float ProjectileSpeed = 5;

    public Sprite WeaponSprite;
    public Sprite ProjectileSprite;

    public LayerMask CollisionLayers;

    public LayerMask WeaponLayer;
    public int WeaponSortingOrder = 10;

    public LayerMask ProjectileLayer;
    public int ProjectileSortingOrder = 11;

    private void OnValidate()
    {
        if (_attributesInspector.Count > 0)
            WeaponAttributeSyncUtility.SyncAttributes(Attributes, _attributesInspector);
    }
}