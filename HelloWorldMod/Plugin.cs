using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace HelloWorldMod
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "gg.ab.helloworld";
        public const string PluginName = "HelloWorldMod";
        public const string PluginVersion = "1.0.0";

        internal static ManualLogSource Log = null!;
        private readonly Harmony _harmony = new Harmony(PluginGuid);

        private void Awake()
        {
            Log = Logger;
            Log.LogInfo($"{PluginName} v{PluginVersion} has awakened!");

            _harmony.PatchAll();
            Log.LogInfo("Harmony patches applied successfully.");
        }

        private void OnDestroy()
        {
            _harmony.UnpatchSelf();
        }
    }

    [HarmonyPatch(typeof(Player), nameof(Player.OnSpawned))]
    public static class PlayerOnSpawnedPatch
    {
        private static void Postfix(Player __instance)
        {
            Plugin.Log.LogInfo($"Player spawned: {__instance.GetPlayerName()}");

            if (MessageHud.instance != null)
            {
                MessageHud.instance.ShowMessage(
                    MessageHud.MessageType.Center,
                    "<color=#FFD700>Hello, Valheim Modding World!</color>"
                );
            }
        }
    }
}

