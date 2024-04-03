using ChickenDinnerV2.Core.Interfaces;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;

namespace ChickenDinnerV2.Core.Tools
{
    internal static class PlayerDataBase
    {
        private static bool isInitialized;

        private static Dictionary<int, Dictionary<Type, IPlayerData>> dataBase;

        public static void Init()
        {
            if (!isInitialized)
            {
                isInitialized = true;

                dataBase = new Dictionary<int, Dictionary<Type, IPlayerData>>();
                Exiled.Events.Handlers.Player.ChangingRole += OnChangingRole;
                Exiled.Events.Handlers.Player.Spawned += OnSpawned;
            }
        }

        public static void Destroy()
        {
            if (isInitialized)
            {
                Exiled.Events.Handlers.Player.ChangingRole -= OnChangingRole;
                Exiled.Events.Handlers.Player.Spawned -= OnSpawned;

                isInitialized = false;
            }
        }

        private static void OnChangingRole(ChangingRoleEventArgs ev)
        {
            Dictionary<Type, IPlayerData> PlayerGeneralBase = getPlayerDataBase(ev.Player.Id);

            foreach (KeyValuePair<Type, IPlayerData> playerDataObject in PlayerGeneralBase)
            {
                IPlayerData playerData = playerDataObject.Value;

                playerData.RoleChange(ev.NewRole);
            }
        }

        private static void OnSpawned(SpawnedEventArgs ev)
        {
            Dictionary<Type, IPlayerData> PlayerGeneralBase = getPlayerDataBase(ev.Player.Id);

            foreach (KeyValuePair<Type, IPlayerData> playerDataObject in PlayerGeneralBase)
            {
                IPlayerData playerData = playerDataObject.Value;

                playerData.Spawned();
            }
        }

        private static Dictionary<Type, IPlayerData> getPlayerDataBase(int playerId)
        {
            Dictionary<Type, IPlayerData> result;

            if (!dataBase.TryGetValue(playerId, out result))
            {
                result = new Dictionary<Type, IPlayerData>();
                dataBase.Add(playerId, result);
            }
            return result;
        }

        public static T Get<T>(Player player) where T : IPlayerData
        {
            Dictionary<Type, IPlayerData> PlayerGeneralBase = getPlayerDataBase(player.Id);
            T result;

            if (!PlayerGeneralBase.ContainsKey(typeof(T)))
            {
                result = (T)Activator.CreateInstance(typeof(T));
                result.Owner = player;

                PlayerGeneralBase.Add(typeof(T), result);
                result.Created();
            }
            else
            {
                result = (T)PlayerGeneralBase[typeof(T)];
            }
            return result;
        }
    }
}
