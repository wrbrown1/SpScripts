using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SP.Core
{
    public class DestroyParticles : MonoBehaviour
    {
        [SerializeField] ParticleSystem particles = null;

        public void Detatch()
        {
            particles.transform.parent = null;
            particles.Stop();
            //particles.GetComponent<ParticleAnimator>()
        }
    }
}
