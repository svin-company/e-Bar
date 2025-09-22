using eBar.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eBar.Api.Controllers;

[Route("api/[controller]")]
public class TestController : BaseController
{
    private readonly ITest _test;

    public TestController(ITest test)
    {
        _test = test;
    }

    [HttpGet("get")]
    public IActionResult Get()
    {
        return Ok(_test.GetData());
    }

    [HttpPost("post")]
    public string Post([FromBody]object message)
    {
        return "Test complete";
    }
    
}