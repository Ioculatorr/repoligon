using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/Weapon Data")]
public class ShotgunSO : ScriptableObject
{
    public GameObject weaponPrefab;

    public float damage;
    public float fireRate;
}
