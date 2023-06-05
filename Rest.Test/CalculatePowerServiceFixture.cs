using FluentAssertions;
using Newtonsoft.Json;
using Rest.Contracts;
using Rest.Services.Core;
using Xunit;

namespace Rest.Test;

public class CalculatePowerServiceFixture
{
    [Theory]
    [InlineData("payload/payload1.json")]
    [InlineData("payload/payload2.json")]
    [InlineData("payload/payload3.json")]
    public void WhenCalled_ReturnsSumAmountSameAsLoad(string payloadFile)
    {

        var request = JsonConvert.DeserializeObject<ProductionPlanRequest>(File.ReadAllText(payloadFile));

        var calculated = Calculator.Calculate(request);

        calculated.Sum(x => x.Production).Should().Be(request.Load);
    }


    [Theory]
    [InlineData("payload/payload4.json")]
    public void WhenCalled_ReturnsEmptyList(string payloadFile)
    {

        var request = JsonConvert.DeserializeObject<ProductionPlanRequest>(File.ReadAllText(payloadFile));

        var calculated = Calculator.Calculate(request);

        calculated.Count.Should().Be(0);
    }
}