using SP.Combat;
using SP.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SP.Core
{
    public class SpawnEnemies : MonoBehaviour
    {
        [SerializeField] Enemy[] enemies;
        [SerializeField] Enemy enemyToSpawn;
        [SerializeField] int numberOfEnemiesToSpawn;
        [SerializeField] float gizmoSphereRadius = 1f;

        bool enemiesHaveSpawned = false;
        public void Spawn()
        {
            for(int i = 0; i < numberOfEnemiesToSpawn; i++)
            {
                if (!enemiesHaveSpawned)
                {
                    Instantiate(enemyToSpawn);
                    enemiesHaveSpawned = true;
                }
                GetEm();
                enemyToSpawn.GetComponent<NavMeshAgent>().Warp(transform.position);
            }
        }

        public void GetEm()
        {
            enemies = FindObjectsOfType<Enemy>();
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<AIController>().SetAggrod(true);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, gizmoSphereRadius);
        }
    }
}
