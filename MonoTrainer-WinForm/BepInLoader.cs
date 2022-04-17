using BepInEx;
using BepInEx.Configuration;
using System.Collections;
using System.IO;
using UnityEngine;

namespace Trainer
{
    // BepInEx5.0.1 Plugin Loader Class

    [BepInPlugin("TrainerDev", "Trainer", "1.0")]
    public class BepInLoader : BaseUnityPlugin
    {
        public static BepInLoader instance;
        public static BepInEx.Logging.ManualLogSource log;
        private bool startFired = false;

        private void Awake()
        {
            //Debug.Log("[Trainer] BepInEx Awake...");

            instance = this;
            log = base.Logger;

            this.transform.parent = null;
            DontDestroyOnLoad(this);

            StartCoroutine(this.StartEx()); // Just in case Start() doesn't fire. Some game's don't.
        }

        private void Start()
        {
            if (!startFired)
            {
                //Debug.Log("[Trainer] BepInEx Start...");
                TrainerLoader.injectionMethod = TrainerLoader.InjectionMethod.BepInEx;
                TrainerLoader.Init();
                startFired = true;
            }
        }

        IEnumerator StartEx()
        {
            this.Start();
            //yield return new WaitUntil(() => Loader.initialized);
            yield return new WaitForSeconds(5);

            if (!TrainerLoader.initialized) { startFired = false; this.Start(); }
        }
    }
}


