
using Rest.Contracts;

namespace Rest.Services.Core;

public static class Combination
{
    public static List<ProductionPlanResponse> FindBestCombination(List<PowerPlant> powerPlants, int load)
    {
        var memo = new Dictionary<string, List<ProductionPlanResponse>>();
        var bestCombination = FindCombination(0, powerPlants, load, memo);
        return bestCombination ?? new List<ProductionPlanResponse>();
    }

    private static List<ProductionPlanResponse> FindCombination(int index, List<PowerPlant> powerPlants, int remainingLoad, Dictionary<string, List<ProductionPlanResponse>> memo)
    {
        if (remainingLoad == 0)
        {
            return new List<ProductionPlanResponse>();
        }

        if (index >= powerPlants.Count || remainingLoad < 0)
        {
            return null;
        }

        var memoKey = $"{index}:{remainingLoad}";
        if (memo.ContainsKey(memoKey))
        {
            return memo[memoKey];
        }

        var powerPlant = powerPlants[index];
        var pmin = powerPlant.PMin;
        var pmax = powerPlant.PMax;

        List<ProductionPlanResponse> bestCombination = null;
        double bestCost = double.MaxValue;

        for (int power = pmax; power >= pmin; power--)
        {
            if (power <= remainingLoad)
            {
                var remainingCombination = FindCombination(index + 1, powerPlants, remainingLoad - power, memo);
                if (remainingCombination != null)
                {
                    var currentCombination = new List<ProductionPlanResponse>(remainingCombination)
                {
                    new ProductionPlanResponse { name = powerPlant.Name, Production = power }
                };
                    var currentCost = CalculateCombinationCost(currentCombination, powerPlants);

                    if (currentCost < bestCost)
                    {
                        bestCost = currentCost;
                        bestCombination = currentCombination;
                    }
                }
            }
        }

        memo[memoKey] = bestCombination;
        return bestCombination;
    }

    private static double CalculateCombinationCost(List<ProductionPlanResponse> combination, List<PowerPlant> powerPlants)
    {
        double cost = 0;
        foreach (var response in combination)
        {
            var powerPlant = powerPlants.Find(p => p.Name == response.name);
            if (powerPlant == null) continue;
            cost += powerPlant.Cost * (double)response.Production;
        }
        return cost;
    }
}
