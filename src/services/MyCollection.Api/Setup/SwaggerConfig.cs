using Microsoft.OpenApi.Models;

namespace MyCollection.Api.Setup
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MyCollection API",
                    Description = "Api developed to control certain items that can be loaned to a contact.",
                    Contact = new OpenApiContact() { Name = "Jonathan Amaral", Email = "jhouamaral95@gmail.com" },
                });
                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer"
                //});

                //c.CustomSchemaIds(type => type.ToString());

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                //{
                //    new OpenApiSecurityScheme{
                //        Reference = new OpenApiReference{
                //            Id = "Bearer",
                //            Type = ReferenceType.SecurityScheme
                //        }
                //    },new List<string>()
                //}});
            });

        }
    }

}
