
using System.Threading;
using UnityEngine;
using Logger = UnityEngine.Debug;
using System;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;

namespace Trainer
{
    // Primary Loader Class for Injecting via an Injector Tool (i.e MInjector, MInject, SharpMonoInjector). Also gets refenrced by BepInLoader.

    public static class TrainerLoader
    {
        #region[Declarations]

        // Declare our GameObject
        private static GameObject _Load;
        public static GameObject Load { get { return _Load; } set { _Load = value; } }

        public static InjectionMethod injectionMethod = InjectionMethod.Injector;
        public enum InjectionMethod
        {
            Injector = 0,
            Doorstop = 1,
            BepInEx = 2
        }

        public static bool initialized = false;

        public static bool consoleInitialized = false;

        #endregion

        #region[Loader Methods]

        #region[Doorstop Support]
        public static void Main(string[] args)
        {
            TrainerLoader.injectionMethod = InjectionMethod.Doorstop;

            InitThreading();
        }
        #endregion

        // Our Loader Method. Must be Static
        public static void Init()
        {
            #region[Create Trainer GameObject]

            // Create a new Gameobject
            TrainerLoader._Load = new GameObject("Trainer");

            #region[Set our GameObject as Root]
            
            Load.transform.parent = null; // Old Unity
            //Load.transform.SetParent(null); // New Unity
            Transform gameRootObject = Load.transform.root;
            if (gameRootObject != null)
            {
                //Logger.Log("[Trainer] Root GameObject is: " + gameRootObject.gameObject.name);
                if (gameRootObject.gameObject != TrainerLoader.Load)
                {
                    //Logger.Log("[Trainer] Attempting to set Trainer as Root!");
                    gameRootObject.parent = Load.transform; // Old Unity
                    //gameRootObject.SetParent(null); // New Unity
                }
            }
            //else { Logger.Log("[Trainer] Loader is a root object!"); }

            #endregion

            // Add Components to our GameObject, we add our Injected menu, etc. Pay attention to load order, it can cause issues.
            //TrainerLoader._Load.AddComponent<Trainer.Utils.CustomConsole>();
            TrainerLoader._Load.AddComponent<Trainer.Menu.TrainerMenu>();

            // Tell Unity not to destory our GameObject on scene change
            GameObject.DontDestroyOnLoad(TrainerLoader._Load);

            #endregion

            Application.runInBackground = true;
            initialized = true;
        }

        public static void InitThreading()
        {
            new Thread(() =>
            {
                Thread.Sleep(5000); // 5 second sleep as initialization occurs *really* early
                Init();

            }).Start();
        }

        #endregion

    }
}
