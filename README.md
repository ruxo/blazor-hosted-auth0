# Auth0 with Blazor Hosted solution - Abridge version

The hosted solution essentially consists of a Blazor WASM plus a Blazor
Server. However, the authentication setup is more like the Blazor WASM
setup. There is some server-side linkage code necessary for cooperation.

# Setup

## Client project

Setting up for the client project is much like the WASM standalone project 
setup (see https://github.com/ruxo/blazor-wasm-auth0). However, the only 
difference is that instead of calling `AddOidcAuthentication` in
`Program.cs`, you simply add one line of this registration:

```csharp
builder.Services.AddApiAuthorization();
```

This extension method is from the `WebAssembly.Authentication` package. Instead
of loading OIDC configuration from `appsettings.*` in the `wwwroot` folder,
like the standalone version, it loads the settings through an HTTP call to 
the server on the path `/_configuration/{clientId}` where `clientId` is 
the assembly name of the Blazor WASM project.

## Server project

The only implementation required from the server side is a controller to 
serve `/_configuration/{clientId}`, which provides the OIDC settings to the
client. See the `OidcConfigurationController.cs` file for an example.

# See also
* [Blazor Server with Auth0 abridge version](https://github.com/ruxo/blazor-server-auth0)
* [Auth0 with Blazor Server using OpenIdConnect library](https://github.com/ruxo/blazor-server-oidc-auth0)
* [Auth0 (or any OIDC) with Blazor WASM standalone](https://github.com/ruxo/blazor-wasm-auth0)