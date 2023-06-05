using Rest.Contracts;
using Rest.Services.Core;
using Rest.Services.Interface;

namespace Rest.Services.Service;

public class PowerPlantService : IPowerPlantService
{
    public List<ProductionPlanResponse> ProductionPlan(ProductionPlanRequest productionPlanRequest)
    {

        return Calculator.Calculate(productionPlanRequest);

    }
}
