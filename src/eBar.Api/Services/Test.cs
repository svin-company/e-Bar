using eBar.Api.Interfaces;

namespace eBar.Api.Services;

public class Test : ITest
{
    public string GetData()
    {
        return "Test data";
    }
}