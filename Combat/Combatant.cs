using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SP.Move;
using SP.Core;
using UnityEngine.AI;
using System;

namespace SP.Combat
{
    public class Combatant : MonoBehaviour, ActionInterface
    {

        Health target;
        float timeSinceLastAttack = 0f;
        Weapon equippedWeapon = null;

        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] bool deathCollision = false;
        [SerializeField] Transform rightHand = null;
        [SerializeField] Transform leftHand = null;
        [SerializeField] Weapon startingWeapon = null;

        private void Start()
        {
            if (gameObject.tag == "Player")
            {
                EquipWeapon(FindObjectOfType<EquippedWeapon>().GetEquippedWeapon());
            }
            else
            {
                EquipWeapon(startingWeapon);
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            FindObjectOfType<EquippedWeapon>().SetEquippedWeapon(weapon);
            equippedWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Equip(rightHand, leftHand,  animator);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if(!target) return;
            if (target.GetIsDead()) return;
            if (!IsInAttackRange())
            {
                GetComponent<Movement>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Movement>().Cancel();
                HandleAttack();
            }
            if(GetComponent<Health>().GetIsDead() && !deathCollision)
            {
                if(GetComponent<NavMeshAgent>()) Destroy(GetComponent<NavMeshAgent>());
            }
        }

        private void HandleAttack()
        {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                GetComponent<Animator>().ResetTrigger("stopAttacking");
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
        }

        private bool IsInAttackRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < equippedWeapon.GetRange();
        }

        public void Attack(GameObject enemy)
        {
            GetComponent<ActionAgenda>().Act(this);
            target = enemy.GetComponent<Health>();
        }

        public void Cancel()
        {
            target = null;
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttacking");
        }

        public bool CanAttack(GameObject target)
        {
            if (target == null) return false;
            Health targetHealth = target.GetComponent<Health>();
            return !targetHealth.GetIsDead() && target && !GetComponent<Health>().GetIsDead();
        }

        private void ConnectAttack()
        {
            if (target)
            {
                float distanceBetweenCombatants = Vector3.Distance(transform.position, target.transform.position);
                if (equippedWeapon.HasProjectile())
                {
                    equippedWeapon.FireProjectile(rightHand, leftHand, target);
                    PlayAudioWithTag("Bow_Sound_Fire");
                }
                else
                {
                    if (distanceBetweenCombatants <= equippedWeapon.GetRange() + 1f)
                    {
                        target.TakeDamage(equippedWeapon.GetDamage());
                        if(equippedWeapon.GetDamage() > 5)
                        {
                            PlayAudioWithTag("Sword_Sound");
                        }
                        else
                        {
                            PlayAudioWithTag("Punch_Sound");
                        }
                    }
                }
            }
        }

        public void PlayAudioWithTag(string audioTag)
        {
            foreach(Transform child in transform)
            {
                AudioSource audio = child.GetComponent<AudioSource>();
                if (audio && child.tag == audioTag)
                {
                    audio.Play();
                }
            }
        }

        //Animation event that is called within the animator
        void Hit()
        {
            ConnectAttack();
        }
        void Shoot()
        {
            ConnectAttack();           
        }
        void StartPull()
        {
            PlayAudioWithTag("Bow_Sound_Pull");
        }

        void StartAttack()
        {
            if(gameObject.tag == "Enemy")
            {
                GetComponentInChildren<ChooseBattleCry>().GetBattleCry();
                PlayAudioWithTag("Battle_Cry");
            }            
            GetComponent<NavMeshAgent>().enabled = false;
        }

        void EndAttack()
        {
            GetComponent<NavMeshAgent>().enabled = true;
        }
        void BeginIdle()
        {
            GetComponent<NavMeshAgent>().enabled = true;
        }
        void Step()
        {
            GetComponentInChildren<ChooseFootstep>().GetFootstep();
            PlayAudioWithTag("Footstep");
        }
        void StartSwordSwing()
        {
            PlayAudioWithTag("Sword_Swing");
        }
    }
}
