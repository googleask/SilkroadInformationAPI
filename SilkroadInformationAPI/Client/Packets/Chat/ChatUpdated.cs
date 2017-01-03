﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadSecurityApi;

namespace SilkroadInformationAPI.Client.Packets.Chat
{
    class ChatUpdated
    {
        public static void Parse(Packet p)
        {
            uint uniqueID = 0;
            string name = "";

            byte chatFlag = p.ReadUInt8();
            var chatType = (ChatType)chatFlag;
            if (chatType == ChatType.All ||
                chatType == ChatType.AllGM ||
                chatType == ChatType.NPC)
            {
                uniqueID = p.ReadUInt32();
                
            }
            else if (chatType == ChatType.PM ||
                     chatType == ChatType.Party ||
                     chatType == ChatType.Guild ||
                     chatType == ChatType.Global ||
                     chatType == ChatType.Stall ||
                     chatType == ChatType.Union ||
                     chatType == ChatType.Accademy)
            {
                name = p.ReadAscii();
            }

            string message = p.ReadAscii();

            if(name == "")
                name = Actions.Mapping.GetCharNameFromUID(uniqueID);

            Client.Chat.Add(chatType, new Information.Chat.ChatMessage(message, name, uniqueID, chatType));
            Client.Chat.Add(chatType, new Information.Chat.ChatMessage(message, name, uniqueID, ChatType.All));

        }
    }
}
