using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public GameObject weaponPrefab;

    public float fireRate = 2f;
}
