using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.BuildingBlocks.Tests;

public class BaseWebIntegrationTest<TTestFactory>
{
    public TTestFactory Factory { get; }

    public BaseWebIntegrationTest(TTestFactory factory)
    {
        Factory = factory;
    }

    public static ControllerContext BuildContext(string id)
    {
        return new ControllerContext()
        {
            HttpContext = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim("personId", id)
                }))
            }
        };
    }
}