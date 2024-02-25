using ChickenDinnerV2.Core.Interfaces;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Dictionary<Type, IPlayerData> PlayerDataBase = getPlayerDataBase(player.Id);

            if (!PlayerDataBase.ContainsKey(typeof(T)))
            {
                T result = default;

                PlayerDataBase.Add(typeof(T), result);
                result.Created(player);
                return result;
            }
            return (T)PlayerDataBase[typeof(T)];
        }
    }
}
