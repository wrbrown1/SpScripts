using SP.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SP.Combat
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] Transform pickupModel = null;
        [SerializeField] float rotationSpeed = 0f;
        [SerializeField] enum Axis { x, y, z };
        [SerializeField] Axis axis;

        static bool playedSpookySound = false;

        private void Update()
        {
            Rotate(pickupModel);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!playedSpookySound)
            {
                PlaySpookySound();
                playedSpookySound = true;
            }
            PickupSound();
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Combatant>().EquipWeapon(weapon);
                Pickup[] pickups = FindObjectsOfType<Pickup>();
                foreach(Pickup pickup in pickups)
                {
                    pickup.Later();
                }
                Spawn();
            }
        }

        public void Later()
        {
            GetComponent<DestroyParticles>().Detatch();
            Destroy(gameObject);
        }

        private void PlaySpookySound()
        {
            GameObject.FindGameObjectWithTag("Spooky").GetComponent<AudioSource>().Play();
        }

        public void PickupSound()
        {
            PlayPickupSound[] players = FindObjectsOfType<PlayPickupSound>();
            foreach(PlayPickupSound player in players)
            {
                player.gotItem = true;
                player.pickupTag = gameObject.tag;
            }
        }

        private void Spawn()
        {
            SpawnEnemies[] spawns = FindObjectsOfType<SpawnEnemies>();
            foreach(SpawnEnemies spawn in spawns)
            {
                spawn.Spawn();
            }
        }

        private void Rotate(Transform model)
        {
            if (axis == Axis.x)
            {
                model.transform.Rotate(rotationSpeed, 0f, 0f);
            }
            else if (axis == Axis.y)
            {
                model.transform.Rotate(0f, rotationSpeed, 0f);
            }
            else
            {
                model.transform.Rotate(0f, 0f, rotationSpeed);
            }
        }
    }
}
