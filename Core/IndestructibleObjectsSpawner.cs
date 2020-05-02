using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SP.Core
{
    public class IndestructibleObjectsSpawner : MonoBehaviour
    {
        [SerializeField] GameObject indestructibleObjects;

        static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned) return;
            SpawnObjects();
            hasSpawned = true;
        }

        private void SpawnObjects()
        {
            GameObject objects = Instantiate(indestructibleObjects);
            DontDestroyOnLoad(objects);
        }
    }
}
