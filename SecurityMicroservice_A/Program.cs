using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using SecurityMicroservice_A.Middlewares;
using System.Reflection.Metadata;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var allowSpecificOrigins = "_corsOrigins";
var defaultRouteSchema = string.Empty;
var environment = string.Empty;
var msftAuthorizationUrl = string.Empty;
var apiScope = string.Empty;
var openIdClientId = string.Empty;
var tenantId = string.Empty;
var azAuthLoginUrlSuffix = string.Empty;
var azAuthTokenUrlSuffix = string.Empty;
var stcUserRoleId = string.Empty;
var adminUserRoleId = string.Empty;
const string OAuth2Keyword = "oauth2";

// Add services to the container.

defaultRouteSchema = builder.Configuration.GetValueByKey(AppSettingKey.DEFAULT_ROUTE_SCHEMA);
msftAuthorizationUrl = builder.Configuration.GetValueByKey(AppSettingKey.AZURE_AD_INSTANCE_KEY);
apiScope = builder.Configuration.GetValueByKey(AppSettingKey.AZURE_AD_SCOPES_KEY);
openIdClientId = builder.Configuration.GetValueByKey(AppSettingKey.AZURE_AD_CLIENT_ID_KEY);
tenantId = builder.Configuration.GetValueByKey(AppSettingKey.AZURE_AD_TENANT_ID_KEY);
azAuthLoginUrlSuffix = builder.Configuration.GetValueByKey(AppSettingKey.MICROSOFT_AD_AUTH_URL_SUFFIX);
azAuthTokenUrlSuffix = builder.Configuration.GetValueByKey(AppSettingKey.MICROSOFT_AD_TOKEN_URL_SUFFIX);

#region Services CORS

var origins = builder.Configuration.GetSection(AppSettingKey.CORS_ORIGIN).Get<string[]>().ToArray();

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins(origins)
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});

#endregion Services CORS

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Read documentation from the XML for Swagger UI

builder.Services.AddSwaggerGen(options =>
{
    #region Swagger Authentication section

    #region Adding security definition

    options.AddSecurityDefinition(OAuth2Keyword, new OpenApiSecurityScheme
    {
        Description = "Application : Request Management API Core",
        Name = OAuth2Keyword,
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(new StringBuilder(msftAuthorizationUrl)
                                            .Append(tenantId)
                                            .Append(azAuthLoginUrlSuffix)
                                            .ToString()),
                TokenUrl = new Uri(new StringBuilder(msftAuthorizationUrl)
                                            .Append(tenantId)
                                            .Append(azAuthTokenUrlSuffix)
                                            .ToString()),
                Scopes = new Dictionary<string, string>
                    {
                        { apiScope, "Access Request Management Core API" }
                    }
            }
        }
    });

    #endregion Adding security definition

    #region Add security requirements to the SWAGGER

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Name = OAuth2Keyword,
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = OAuth2Keyword },
                    Scheme = OAuth2Keyword,
                    In = ParameterLocation.Header
                },
                new[] { apiScope }
            }
        });

    #endregion Add security requirements to the SWAGGER

    #endregion Swagger Authentication section

    #region Swagger document generation
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Security Microservice A", Version = "v1" });
    options.DocumentFilter<SwaggerPathPrefixFilter>(defaultRouteSchema);
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    #endregion Swagger document generation
});

#endregion Read documentation from the XML for Swagger UI

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = defaultRouteSchema + "/v1/swagger/{documentname}/swagger.json";
    });
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = defaultRouteSchema + "/v1/swagger";
        c.SwaggerEndpoint("/" + c.RoutePrefix + "/v1/swagger.json", "SecurityMicroservice.A");

        c.OAuthClientId(openIdClientId);
        c.OAuthScopeSeparator(" ");
        c.OAuthUsePkce();
    });
}

app.UsePathBase(new PathString("/" + defaultRouteSchema));
app.UseRouting();

app.UseCors(allowSpecificOrigins);

app.UseMiddleware<TcpSecMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
