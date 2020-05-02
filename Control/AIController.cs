using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SP.Combat;
using UnityEngine.AI;
using SP.Core;
using System;
using SP.Move;

namespace SP.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float aggroRange = 10f;
        [SerializeField] bool isGuard = false;
        [SerializeField] float suspicionTimer = 0f;
        [SerializeField] Patroller patroller;       
        [SerializeField] float waypointRadius = 1f;

        [Header("Waypoint Navigation Settings")]
        [SerializeField] float lingerTimeLowerBound = 0f;
        [SerializeField] float lingerTimeUpperBound = 0f;
        [SerializeField] float patrolSpeed, chaseSpeed;

        Animator animator;
        Combatant combatant;
        GameObject player;        
        Vector3 guardPost;
        float playerLastSeen = Mathf.Infinity;
        float timeSinceArrival;
        float waypointLingerTime;
        int currentWaypointIndex = 0;
        bool manuallyAggrod = false;

        private void Start()
        {
            animator = GetComponent<Animator>();
            combatant = GetComponent<Combatant>();
            player = GameObject.FindWithTag("Player");
            guardPost = transform.position;
            GetComponent<Animator>().SetBool("beenAlerted", false);
        }
        private void Update()
        {
            if (player.GetComponent<Health>().GetIsDead() && !GetComponent<Health>().GetIsDead())
            {
                GetComponent<NavMeshAgent>().isStopped = true;
            }
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")){
                if (!GetComponent<Health>().GetIsDead() && Aggro() && combatant.CanAttack(player))
                {
                    playerLastSeen = 0f;
                    combatant.Attack(player);
                    GetComponent<NavMeshAgent>().speed = chaseSpeed;
                }
                else if(playerLastSeen < suspicionTimer)
                {
                    GetComponent<ActionAgenda>().CancelCurrentAction();
                }
                else
                {
                    if (isGuard && !GetComponent<Health>().GetIsDead())
                    {
                        Patrol();
                    }
                    else
                    {
                        combatant.Cancel();                       
                    }
                }
            }
            playerLastSeen += Time.deltaTime;
            timeSinceArrival += Time.deltaTime;
        }

        private void Patrol()
        {
            GetComponent<NavMeshAgent>().speed = patrolSpeed;
            Vector3 nextPosition = guardPost;
            if (patroller)
            {
                GetComponent<Animator>().SetBool("beenAlerted", true);
                if (AtWaypoint())
                {
                    waypointLingerTime = UnityEngine.Random.Range(lingerTimeLowerBound, lingerTimeUpperBound);
                    timeSinceArrival = 0f;
                    ProgressToNextWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
                if(timeSinceArrival >= waypointLingerTime)
                {
                    GetComponent<Movement>().MoveAction(nextPosition);
                }                
            }
            else
            {
                ReturnToPost();
            }
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patroller.GetWaypoint(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointRadius;

        }
        private void ProgressToNextWaypoint()
        {
            currentWaypointIndex = patroller.GetNextIndex(currentWaypointIndex);
        }
        private void ReturnToPost()
        {
            GetComponent<Movement>().MoveAction(guardPost);
        } 

        private bool Aggro()
        {
            float distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
            if (distanceFromPlayer <= aggroRange && !player.GetComponent<Health>().GetIsDead())
            {
                GetComponent<Animator>().SetBool("beenAlerted", true);
                return true;

            } 
            else if (GetComponent<Health>().IsTakingDamage() && !player.GetComponent<Health>().GetIsDead())
            {
                GetComponent<Animator>().SetBool("beenAlerted", true);
                return true;
            }
            else if (manuallyAggrod && !player.GetComponent<Health>().GetIsDead())
            {
                GetComponent<Animator>().SetBool("beenAlerted", true);
                return true;
            }
            else
            {
                return false;               
            }
        }

        public void SetAggrod(bool aggrod)
        {
            manuallyAggrod = aggrod;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, aggroRange);
        }
    }
}