#region[References]
using System;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using log = UnityEngine.Debug;
using Object = UnityEngine.Object;
using console = System.Console;

#endregion

/* Instructions on Use

    Reference the Designer Project, the built .exe goes into the same folder as Trainer.dll for BepExIn\plugins. It acts as a proxy to
    access WinFoms without alot of modifications and makes it easier to design a visual layout. This is your WindowBase. 
    Design layout here and copy the  Form1.Designer.cs code for like say a button into WinForm.cs
    It makes it easier than coding it all by hand. Then remove it from the Designer, basically when built the Design should just be a Blank Window.
    
    The launched window (WinForm.cs) will have access to Unity functions and whatnot. In say like a button click put your normal modding code for
    what you want to do, like add money.

    This class is only used for in-game GUI and to launch/init the Winform. No logic should go here.


*/

namespace Trainer.Menu
{
    public class TrainerMenu : MonoBehaviour
    {
        #region[Declarations]

        #region[Trainer]

        public static TrainerMenu instance = null;

        private Rect MainWindow;
        private bool MainWindowVisible = true;



        #endregion

        #region[Winform]

        private static Thread winformThread = null;
        //private static WinFormGUI winForm = null;
        private static Designer.Form1 winForm = null;
        private static bool isGameQuiting = false;


        #endregion



        #endregion

        public void Awake()
        {
            InitWinForm();
        }

        private void Start()
        {
            #region[Window Definitions - Don't Touch]
            MainWindow = new Rect(UnityEngine.Screen.width / 2 - 100, UnityEngine.Screen.height / 2 - 350, 250f, 50f);
            #endregion
           
            instance = this;
        }

        private void Update()
        {
            #region[Trainer]
            
            if (Input.GetKeyDown(KeyCode.Backspace)) { MainWindowVisible = !MainWindowVisible; }

            #endregion

        }
        
        private void OnGUI()
        {
            if (!MainWindowVisible) { return; }
                
            if (UnityEngine.Event.current.type == UnityEngine.EventType.Layout)
            {
                #region[For IMGUI Windows]
                               
                GUI.backgroundColor = Color.black;
                GUIStyle titleStyle = new GUIStyle(GUI.skin.window);
                titleStyle.normal.textColor = Color.green;

                //MAIN WINDOW #0
                MainWindow = new Rect(MainWindow.x, MainWindow.y, 250f, 50f);
                MainWindow = GUILayout.Window(777, MainWindow, new GUI.WindowFunction(RenderUI), " Trainer v1", titleStyle, new GUILayoutOption[0]);

                #endregion
            }

        }

        private void RenderUI(int id)
        {
            #region[Styles]

            GUIStyle textStyle = new GUIStyle();
            textStyle.normal.background = Texture2D.whiteTexture;
            //textStyle.normal.textColor = Color.cyan;
            textStyle.alignment = TextAnchor.MiddleCenter;

            GUIStyle labelStyleGreen = new GUIStyle();
            labelStyleGreen.normal.textColor = Color.green;
            labelStyleGreen.alignment = TextAnchor.MiddleCenter;

            GUIStyle labelStyleCyan = new GUIStyle();
            labelStyleCyan.normal.textColor = Color.cyan;
            labelStyleCyan.alignment = TextAnchor.MiddleCenter;

            GUIStyle labelStyleWhite = new GUIStyle();
            labelStyleWhite.normal.textColor = Color.white;
            labelStyleWhite.alignment = TextAnchor.MiddleCenter;

            GUIStyle labelStyleYellow = new GUIStyle();
            labelStyleYellow.normal.textColor = Color.yellow;
            labelStyleYellow.alignment = TextAnchor.MiddleCenter;

            GUIStyle labelStyleRed = new GUIStyle();
            labelStyleRed.normal.textColor = Color.red;
            labelStyleRed.alignment = TextAnchor.MiddleCenter;

            #endregion
                       
            switch (id)
            {
                #region[Main Window]

                case 777:

                    GUI.color = Color.white;
                    if (GUILayout.Button("Toggle Winform", new GUILayoutOption[0]))
                    {
                        try
                        {
                            /*
                            if (winForm != null && !winForm.FormBase.Visible)
                            {
                                InitWinForm(); // We use a Thread otherwise it will lock the main Thread until closed. But now we need to use Thread2Thread Invokers. Better way???
                                log.LogWarning("[Trainer]: Showing WinForm");
                            }
                            else if (winForm != null && winForm.FormBase.Visible)
                            {
                                winForm.FormBase.Close();
                                log.LogWarning("[Trainer]: Closing WinForm");
                            }
                            */
                        }
                        catch (Exception e) { log.LogError("[Trainer]: ERROR: " + e.Message); }
                    }

                    break;

                #endregion

            }
            
            GUI.DragWindow();
        }

        #region[Winform Init]

        public static void InitWinForm()
        {
            winformThread = new Thread(OpenWinForm);
            winformThread.IsBackground = true;
            winformThread.Start();

            /*
            new Thread(() =>
            {
                winForm = new WinForm();
                winForm.ShowDialog();

            }).Start();
            */
        }
        
        public static void OpenWinForm()
        {
            winForm = new Designer.Form1();
            //winForm = new WinFormGUI();
            //winForm.CreateForm();
            winForm.ShowDialog();
        }

        #endregion

    }
}


