﻿using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.IO;

namespace TEditXNA.Terraria
{
    public class Bestiary : ObservableObject
    {
        const int KillMax = 9999;
        public Dictionary<string,int> NPCKills = new Dictionary<string, int>();
        public HashSet<string> NPCNear = new HashSet<string>();
        public HashSet<string> NPCChat = new HashSet<string>();

        public void Save(BinaryWriter w)
        {
            // Kill Counts
            w.Write(NPCKills.Count);
            foreach (var item in NPCKills)
            {
                w.Write(item.Key);
                w.Write(item.Value);
            }

            // Seen
            w.Write(NPCNear.Count);
            foreach (var item in NPCNear)
            {
                w.Write(item);
            }

            // Chatted
            w.Write(NPCChat.Count);
            foreach (var item in NPCChat)
            {
                w.Write(item);
            }
        }

        public void Load(BinaryReader r, uint version)
        {
            NPCKills.Clear();
            NPCNear.Clear();
            NPCChat.Clear();
            int killCount;
            int nearCount;
            int chatCount;

            killCount = r.ReadInt32();
            for (int i = 0; i < killCount; i++)
            {
                NPCKills[r.ReadString()] = r.ReadInt32();
            }

            nearCount = r.ReadInt32();
            for (int i = 0; i < nearCount; i++)
            {
                NPCNear.Add(r.ReadString());
            }

            chatCount = r.ReadInt32();
            for (int i = 0; i < chatCount; i++)
            {
                NPCChat.Add(r.ReadString());
            }
        }

    }
}
