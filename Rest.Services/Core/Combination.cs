
using Rest.Contracts;

namespace Rest.Services.Core;

public static class Combination
{
    public static Dictionary<string, int> FindBestCombination(List<PowerPlant> powerPlants, int load)
    {
        var validCombinations = new List<Dictionary<string, int>>();
        var bestCost = double.MaxValue;
        var remainingLoad = load;

        // Recursive function to find the best combination
        FindCombination(0, new Dictionary<string, int>(), powerPlants, remainingLoad, validCombinations, ref bestCost);


        var bestCombination = validCombinations.OrderBy(d => d.Count).FirstOrDefault();


        if (bestCombination != null && bestCombination.Values.Sum() == load)
            return bestCombination;
        else
            return new Dictionary<string, int>();
    }



    private static void FindCombination(int index, Dictionary<string, int> combination, List<PowerPlant> powerPlants, int remainingLoad, List<Dictionary<string, int>> validCombinations, ref double bestCost)
    {
        if (remainingLoad == 0)
        {
            var currentCost = CalculateCombinationCost(combination, powerPlants);
            if (currentCost < bestCost)
            {
                bestCost = currentCost;
                validCombinations.Clear();
            }
            if (currentCost == bestCost)
            {
                validCombinations.Add(new Dictionary<string, int>(combination));
            }
            return;
        }

        if (index >= powerPlants.Count || remainingLoad < 0)
        {
            return;
        }

        var powerPlant = powerPlants[index];
        var pmin = powerPlant.PMin;
        var pmax = powerPlant.PMax;

        for (int power = pmax; power >= pmin; power--)
        {
            if (power <= remainingLoad)
            {
                combination[powerPlant.Name] = power;
                remainingLoad -= power;

                FindCombination(index + 1, combination, powerPlants, remainingLoad, validCombinations, ref bestCost);

                combination.Remove(powerPlant.Name);
                remainingLoad += power;
            }
        }

        FindCombination(index + 1, combination, powerPlants, remainingLoad, validCombinations, ref bestCost);
    }

    private static double CalculateCombinationCost(Dictionary<string, int> combination, List<PowerPlant> powerPlants)
    {
        double cost = 0;
        foreach (var kvp in combination)
        {
            var powerPlant = powerPlants.Find(p => p.Name == kvp.Key);
            if (powerPlant == null) continue;
            cost += powerPlant.Cost * kvp.Value;
        }
        return cost;
    }
}
