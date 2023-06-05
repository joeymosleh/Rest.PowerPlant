using System.Text.Json.Serialization;

namespace Rest.Contracts;

public class ProductionPlanResponse
{
    [JsonPropertyName("name")]
    public string name { get; set; }

    [JsonPropertyName("p")]
    public decimal Production { get; set; }
}
