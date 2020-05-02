//using SP.Saving;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//using UnityEngine.SceneManagement;

//namespace SP.SceneManagement
//{
//    public class Portal : MonoBehaviour
//    {
//        [SerializeField] int sceneToLoadIndex = -1;
//        [SerializeField] Transform spawnLocation;
//        [SerializeField] float fadeOutTime = 2f;
//        [SerializeField] float fadeInTime = 2f;
//        [SerializeField] float transitionWait = 2f;
//        private void OnTriggerEnter(Collider other)
//        {
//            if(other.tag == "Player")
//            {
//                StartCoroutine(TransferScene());
//            }
//        }

//        private IEnumerator TransferScene()
//        {
//            if(sceneToLoadIndex < 0)
//            {
//                print("Scene to load is not set!");
//                yield break;
//            }
//            DontDestroyOnLoad(gameObject);
//            SceneTransition sceneTransition = FindObjectOfType<SceneTransition>();
//            yield return sceneTransition.FadeOut(fadeOutTime);
//            //SavingWrap wrap = FindObjectOfType<SavingWrap>();
//            //wrap.Save();
//            yield return SceneManager.LoadSceneAsync(sceneToLoadIndex);
//            //wrap.Load();
//            Portal other = GetPortal();
//            UpdatePlayer(other);
//            //wrap.Save();
//            yield return new WaitForSeconds(transitionWait);
//            yield return sceneTransition.FadeIn(fadeInTime);

//            Destroy(gameObject);
//        }

//        private Portal GetPortal()
//        {
//            foreach (Portal portal in FindObjectsOfType<Portal>()){
//                if (portal == this)
//                {
//                    continue;
//                }
//                //if (portal.sceneToLoadIndex != sceneToLoadIndex)
//                //{
//                //    continue;
//                //}
//                return portal;
//            }
//            return null;
//        }

//        private void UpdatePlayer(Portal other)
//        {
//            GameObject player = GameObject.FindGameObjectWithTag("Player");
//            player.GetComponent<NavMeshAgent>().Warp(other.spawnLocation.position);
//            player.transform.rotation = other.spawnLocation.rotation;
//        }
//    }
//}

using SP.Saving;
using SP.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] Transform spawnPosition;
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeOutTime = .5f;
        [SerializeField] float loadWaitTime = 2f;

        enum DestinationLink
        {
            A, B, C, D, E, F, G, H, I
        }

        [SerializeField] DestinationLink destinationLink;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if(sceneToLoad < 0)
            {
                print("Scene to load is not set on " + gameObject.name + "!");
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            SceneTransition sceneTransition = FindObjectOfType<SceneTransition>();
            yield return sceneTransition.FadeOut(fadeOutTime);

            SavingWrap savingWrap = FindObjectOfType<SavingWrap>();
            savingWrap.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            savingWrap.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            savingWrap.Save();

            yield return new WaitForSeconds(loadWaitTime);

            yield return sceneTransition.FadeIn(fadeInTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPosition.position;
            player.transform.rotation = otherPortal.spawnPosition.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destinationLink != destinationLink) continue;
                return portal;
            }
            return null;
        }
    }
}