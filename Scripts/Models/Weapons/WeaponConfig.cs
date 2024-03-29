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

    public List<IWeaponAttribute> Attributes = new List<IWeaponAttribute>();

    [Range(1, 20)] public int FireRate = 3;
    [Range(1, 20)] public float ProjectileSpeed = 2;

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