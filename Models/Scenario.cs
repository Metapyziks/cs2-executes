using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Modules.Utils;
using ExecutesPlugin.Enums;

namespace ExecutesPlugin.Models;

public class Scenario
{
    public string? Name { get; set; }
    public string Description { get; set; } = "";
    public EBombsite Bombsite { get; set; }
    public bool DisableOtherBombsite { get; set; } = true;
    public int RoundTime { get; set; }
    public int MinPlayerCount { get; set; }
    public HashSet<int> SpawnIds { get; set; } = new();
    public HashSet<int> GrenadeIds { get; set; } = new();

    [JsonIgnore]
    public Dictionary<CsTeam, List<Spawn>> Spawns { get; set; } = new();

    [JsonIgnore] 
    public Dictionary<CsTeam, List<Grenade>> Grenades { get; set; } = new();

    public int GetTotalSpawnCount()
    {
        int totalSpawnCount = 0;

        if (Spawns.ContainsKey(CsTeam.CounterTerrorist))
        {
            totalSpawnCount += Spawns[CsTeam.CounterTerrorist].Count;
        }
        if (Spawns.ContainsKey(CsTeam.Terrorist))
        {
            totalSpawnCount += Spawns[CsTeam.Terrorist].Count;
        }

        return totalSpawnCount;
    }
}