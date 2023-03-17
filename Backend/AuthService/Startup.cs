using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthService
{
    public class Startup
    {
     //   public Startup(IConfiguration configuration)
     //   {
     //       Configuration = configuration;
     //   }

     //   public IConfiguration Configuration { get; }    

     //   public void ConfigureServices(IServiceCollection services)
     //   {
     //       services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     //.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
     //    options => options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
     //    {
     //        ValidateIssuer = false,
     //        ValidateAudience = false
     //    });
     //   }

        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        }
    }
}
