using Microsoft.AspNetCore.Mvc;
using Rest.Contracts;
using Rest.Services.Interface;
using System.ComponentModel.DataAnnotations;

namespace Rest.PowerPlant.Controllers;

[Produces("application/json")]
[ApiController]
[Route("productionplan")]
public class PowerPlantController : ControllerBase
{
    public PowerPlantController(ILogger<PowerPlantController> logger, IPowerPlantService powerPlantService)
    {
        PowerPlantService = powerPlantService ?? throw new ArgumentNullException(nameof(powerPlantService));
    }

    public IPowerPlantService PowerPlantService { get; }

    /// <summary>
    ///     Calculate how much power each of a multitude of different powerplants need to produce 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("")]
    [ProducesResponseType(typeof(List<ProductionPlanResponse>), 200)]
    public List<ProductionPlanResponse> ProductionPlan(
    [FromBody][Required] ProductionPlanRequest request)
    {
        return PowerPlantService.ProductionPlan(request);


    }
}
