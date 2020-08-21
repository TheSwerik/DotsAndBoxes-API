using System;
using System.Text.Json.Serialization;

namespace API.Entities
{
    [Serializable]
    public struct User
    {
        [JsonPropertyName("Id")] public Guid Id { get; set; }
        [JsonPropertyName("Username")] public string Username { get; set; }
        public override string ToString() { return $"{{ ID: {Id} | Username: {Username} }}"; }
    }
}