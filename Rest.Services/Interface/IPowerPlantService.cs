using Rest.Contracts;
namespace Rest.Services.Interface;

public interface IPowerPlantService
{    List<ProductionPlanResponse> ProductionPlan(ProductionPlanRequest ProductionPlan);
}
