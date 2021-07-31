using System;
using System.Collections.Generic;
using System.Linq;
namespace DOL.GS

{
    public static class EntityManager
    {
        //todo - have server property with Max Player size?
        private static List<GamePlayer> _players = new List<GamePlayer>(4000);
        private static object _playersLock = new object();

        private static List<GameObject> _npcs = new List<GameObject>(50000);
        private static object _npcsLock = new object();
        
        public static GamePlayer[] GetAllPlayers()
        {
            lock (_players)
            {
                return _players.ToArray();
            }
        }

        public static void AddPlayer(GamePlayer p)
        {
            lock (_playersLock)
            {
                Console.WriteLine($"Added {p.Name}");
                _players.Add(p);
            }
        }

        public static void RemovePlayer(GamePlayer p)
        {
            lock (_playersLock)
            {
                Console.WriteLine($"Removed {p.Name}");
                _players.Remove(p);
            }
        }
        
        public static GameObject[] GetAllNpcs()
        {
            lock (_npcs)
            {
                return _npcs.ToArray();
            }
        }

        public static void AddNpc(GameObject o)
        {
            lock (_npcsLock)
            {
                _npcs.Add(o);
            }
        }

        public static void RemoveNpc(GameObject o)
        {
            lock (_npcsLock)
            {
                _npcs.Remove(o);
            }
        }
        
        


    }
}