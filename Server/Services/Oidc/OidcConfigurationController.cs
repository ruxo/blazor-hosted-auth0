using Microsoft.AspNetCore.Mvc;

namespace Blazor.Example.Server.Services.Oidc;

[ApiController]
[Route("_configuration")]
public sealed class OidcConfigurationController : ControllerBase
{
    readonly IConfiguration configuration;
    const string BlazorClientAssemblyName = "BlazorHostedWithAuth0.Client";
    
    public OidcConfigurationController(IConfiguration configuration) {
        this.configuration = configuration;
    }

    [HttpGet("{clientId}")]
    public IActionResult GetClientRequestParameters([FromRoute] string clientId) {
        if (clientId == BlazorClientAssemblyName) {
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            return Ok(new{
                authority = configuration["Auth0:Authority"],
                client_id = configuration["Auth0:ClientId"],
                redirect_uri = $"{baseUrl}/authentication/login-callback",
                post_logout_redirect_uri = baseUrl,
                response_type = "code",
                scope = "openid profile",
                extraQueryParams = new Dictionary<string,string>{   // This is needed by Auth0
                    {"audience", configuration["Auth0:Audience"]!}
                }
            });
        }
        else
            return NotFound();
    }
}