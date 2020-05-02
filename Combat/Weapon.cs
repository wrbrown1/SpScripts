using System.Collections;
using System.Collections.Generic;
using SP.Core;
using UnityEngine;
using System;

namespace SP.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Create Weapon", order = 0)]
    public class Weapon  : ScriptableObject
    {
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] AnimatorOverrideController overrideController = null;
        [SerializeField] float damage = 0f;
        [SerializeField] float range = 0f;
        [SerializeField] bool rightHanded = true;
        [SerializeField] Projectile projectile = null;
        [SerializeField] Blade blade = null;

        const string weaponName = "Weapon";

        public void Equip(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);
            if (weaponPrefab)
            {
                GameObject weapon = Instantiate(weaponPrefab, GetHandDominance(rightHand, leftHand));
                weapon.name = weaponName;
            }           
            if (overrideController) animator.runtimeAnimatorController = overrideController;           
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (!oldWeapon)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (!oldWeapon) return;
            oldWeapon.name = "goodbye";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetHandDominance(Transform rightHand, Transform leftHand)
        {
            Transform handDominance;
            if (rightHanded)
            {
                handDominance = rightHand;
            }
            else
            {
                handDominance = leftHand;
            }

            return handDominance;
        }

        public float GetDamage()
        {
            return damage;
        }

        public float GetRange()
        {
            return range;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public bool HasBlade()
        {
            return blade != null;
        }

        public Blade GetBlade()
        {
            return blade;
        }

        public void FireProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHandDominance(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, damage);
        }
    }
}
