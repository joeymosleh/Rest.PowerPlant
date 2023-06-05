namespace Rest.Contracts;

public class ProductionPlanRequest
{
    public int Load { get; set; }
    public Fuel Fuels { get; set; }
    public List<PowerPlant> PowerPlants { get; set; }
}
