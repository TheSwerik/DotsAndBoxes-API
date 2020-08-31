using System;
using System.Collections.Generic;
using API.Entities;

namespace API.Gamelogic
{
    public class Lobby
    {
        public Guid Id { get; set; }

        public string name { get; set; }

        public int maxPlayers = 2;

        public LinkedList<User> players { get; set; }
    }
}