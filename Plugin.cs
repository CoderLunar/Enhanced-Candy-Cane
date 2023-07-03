using BepInEx;
using System.Reflection;
using UnityEngine;
using HarmonyLib;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace LunarCandycane
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        private Harmony Harmony;

        void Start()
        {
            Harmony = new Harmony(PluginInfo.GUID);
            Harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPatch(typeof(VRRig), "Start", MethodType.Normal)]
        public class Init
        {
            public static async void Postfix(VRRig __instance)
            {
                await Task.Delay(500);
                
                var holdables = __instance.GetComponent<BodyDockPositions>().allObjects;
                var candycane = holdables.First(a => a.name == "CANDY CANE");
                candycane.GetComponent<Renderer>().sharedMaterial.mainTexture = Texture2D.whiteTexture;
                candycane.gameObject.AddComponent<ColorLerp>();
            }
        }
    }
}