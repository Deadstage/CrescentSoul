using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newKickWeaponData", menuName = "Data/Weapon Data/Kick Weapon")]
public class SO_KickWeaponData : SO_WeaponData
{
    [SerializeField] private WeaponAttackDetails[] attackDetails;

    public WeaponAttackDetails[] AttackDetails { get => attackDetails; private set => attackDetails = value; }
}
