using SP.Core;
using SP.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SP.Saving
{
    public class SavingWrap : MonoBehaviour
    {
        const string fileName = "MY_SAVE_FILE";
        [SerializeField] float fadeInTime = 2f;

        IEnumerator Start()
        {
            SceneTransition sceneTransition = FindObjectOfType<SceneTransition>();
            sceneTransition.PreWarmFadeIn();
            yield return GetComponent<SavingSystem>().LoadLastScene(fileName);
            yield return sceneTransition.FadeIn(fadeInTime);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5)) { Save(); }
            if (Input.GetKeyDown(KeyCode.F9)) {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetTrigger("revive");
                if(GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().GetHealth() > 0f)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().SetIsDead(false);
                }
                Load();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(fileName);
        }
        public void Load()
        {
            GetComponent<SavingSystem>().Load(fileName);
        }
    }
}
