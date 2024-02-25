using System;
using Exiled.API.Features;
using Exiled.API.Enums;
using HarmonyLib;
using System.Linq;
using ChickenDinnerV2.Core.Interfaces;
using ChickenDinnerV2.Modules;
using ChickenDinnerV2.Core.Tools;

namespace ChickenDinnerV2.Core
{
    public class Main : Plugin<ConfigRegistration>
    {
        private const string prefix = "chicken-dinner-v2";
        private const string name = "Chicken Dinner V2";
        private const string author = "dmatseku";
        private const string harmonyId = "net.dmatseku.chickendinnerv2";


        private static readonly Lazy<Main> LazyInstance = new Lazy<Main>(() => new Main());
        public static Main Instance => LazyInstance.Value;
        public override PluginPriority Priority { get; } = PluginPriority.First;
        public override string Prefix => prefix;
        public override string Name => name;
        public override string Author => author;

        private Harmony HarmonyInstance;
        private ObserverManager ObserverManagerInstance;

        private Main()
        {
        }

        public override void OnEnabled()
        {
            HarmonyInstance = new Harmony(harmonyId);
            ObserverManagerInstance = new ObserverManager();

            PluginsInitialize();
            ObserverManagerInstance.Register();
            HarmonyInstance.PatchAll();
            PlayerDataBase.Init();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            HarmonyInstance.UnpatchAll();
            ObserverManagerInstance.Unregister();

            base.OnDisabled();
        }

        private void PluginsInitialize()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IInitialize).IsAssignableFrom(p) && p != typeof(IInitialize));

            foreach (Type patchType in types)
            {
                IInitialize instance = (IInitialize)Activator.CreateInstance(patchType);
                instance.Initialize();
            }
        }
    }
}
