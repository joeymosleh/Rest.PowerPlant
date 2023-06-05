using Rest.Contracts;

namespace Rest.Services.Core;

public class Calculator
{
    public static List<ProductionPlanResponse> Calculate(ProductionPlanRequest request)
    {

        var sortedPowerPlants = PowerPlantsSortByCost(request.PowerPlants, request.Fuels);

        var bestCombination = Combination.FindBestCombination(sortedPowerPlants, request.Load);

        return bestCombination.Where(x=>x.Production != 0).ToList();

    }

    private static List<PowerPlant> PowerPlantsSortByCost(List<PowerPlant> powerPlants, Fuel fuels)
    {
        foreach (var powerPlant in powerPlants)
        {
            switch (powerPlant.Type)
            {
                case PowerPlantTypeEnum.GasFired:
                    powerPlant.Cost = fuels.GasPrice / powerPlant.Efficiency;
                    break;
                case PowerPlantTypeEnum.TurboJet:
                    powerPlant.Cost = fuels.KerosinePrice / powerPlant.Efficiency;
                    break;
                case PowerPlantTypeEnum.WindTurbine:
                    powerPlant.Cost = 0;
                    powerPlant.PMax = powerPlant.PMax * fuels.WindPercentage / 100;
                    break;
                default:
                    throw new ArgumentException("Power Plant Type not available ");
            }
        }

        return powerPlants.OrderBy(pp => pp.Cost).ToList();
    }


}
