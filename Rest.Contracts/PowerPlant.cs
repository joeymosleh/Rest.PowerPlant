using System.Text.Json.Serialization;

namespace Rest.Contracts;

public class PowerPlant
{
    public string Name { get; set; }
    public PowerPlantTypeEnum Type { get; set; }
    public double Efficiency { get; set; }
    public int PMin { get; set; }
    public int PMax { get; set; }
    [JsonIgnore]
    public double Cost { get; set; }
}


public enum PowerPlantTypeEnum
{
    GasFired = 0,
    TurboJet = 1,
    WindTurbine = 2
}
