using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;
using SP.Control;
using SP.Combat;
using SP.Saving;

namespace SP.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float health;
        [SerializeField] float maxHealth;
        [SerializeField] float healthRegeneration = 0f;
        Animator animator;
        bool isDead = false;
        bool damageAlert = false;

        private void Update()
        {
            if (health == 0)
            {
                Die();
            }
            RegenerateHealth();
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            isDead = false;
        }

        private void RegenerateHealth()
        {
            if(health < maxHealth && !isDead)
            {
                health += (healthRegeneration * Time.deltaTime);
            }
        }

        public float GetHealth()
        {
            return health;
        }
        public float GetMaxHealth()
        {
            return maxHealth;
        }

        public void TakeDamage(float damage)
        {
            damageAlert = true;
            health = Mathf.Max(0, health - damage);
            print(health);
            if (health == 0f && !isDead)
            {
                Die();
            }
        }

        public bool IsTakingDamage()
        {
            return damageAlert;
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            DetermineDeathAnimation();
            GetComponent<ActionAgenda>().CancelCurrentAction();
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            if (gameObject.tag == "Enemy")
            {
                GetComponentInChildren<ChooseBattleCry>().GetBattleCry();
                GetComponent<Combatant>().PlayAudioWithTag("Battle_Cry");
            }
        }

        private void DetermineDeathAnimation()
        {
            if (gameObject.GetComponent<Enemy>() != null && animator != null)
            {
                float x = Random.Range(0f, 6f);
                if (x <= 1f)
                {
                    animator.SetTrigger("die_1");
                    //Invoke("Ragdoll", 1.9f);
                }
                else if (x > 1 && x <= 2)
                {
                    animator.SetTrigger("die_2");
                    //Invoke("Ragdoll", 1.75f);
                }
                else if (x > 2 && x <= 3)
                {
                    animator.SetTrigger("die_3");
                    //Invoke("Ragdoll", 1.75f);
                }
                else if (x > 3 && x <= 4)
                {
                    animator.SetTrigger("die_4");
                    //Invoke("Ragdoll", 1.75f);
                }
                else if (x > 4 && x <= 5)
                {
                    animator.SetTrigger("die_5");
                    //Invoke("Ragdoll", 1.75f);
                }
                else
                {
                    animator.SetTrigger("die_6");
                    //Invoke("Ragdoll", 1.75f);
                }
            }
            else
            {
                if(animator)animator.SetTrigger("die");
                Destroy(gameObject.GetComponent<PlayerCharacterController>());
            }
        }

        public bool GetIsDead()
        {
            return isDead;
        }
        public void SetIsDead(bool set)
        {
            isDead = set;
        }

        //void Ragdoll()
        //{
        //    animator.enabled = false;
        //    List<Rigidbody> rigidbodies = new List<Rigidbody>();
        //    GetComponentsInChildren(rigidbodies);
        //    foreach (Rigidbody rigidbody in rigidbodies)
        //    {
        //        rigidbody.velocity = Vector3.zero;
        //        rigidbody.angularVelocity = Vector3.zero;

        //    }            
        //}

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (float)state;

            if(health == 0f)
            {
                Die();
            }
        }
    }
}