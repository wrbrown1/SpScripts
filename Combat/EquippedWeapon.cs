using SP.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SP.Combat
{
    public class EquippedWeapon : MonoBehaviour
    {
        [SerializeField] Weapon equippedWeapon;

        public Weapon GetEquippedWeapon()
        {
            return equippedWeapon;
        }

        public void SetEquippedWeapon(Weapon weapon)
        {
            equippedWeapon = weapon;
        }
    }
}
