using System;
using System.Text.Json.Serialization;

namespace API.Entities
{
    [Serializable]
    public struct User
    {
        public Guid Id;
        public string Username;
        public override string ToString() { return $"{{ ID: {Id} | Username: {Username} }}"; }
    }
}