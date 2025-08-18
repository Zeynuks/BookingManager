using BookingManager.Extensions;
using BookingManager.Middlewares;
using Microsoft.OpenApi.Models;

namespace BookingManager
{
    internal static class Program
    {
        public static void Main( string[] args )
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen( c =>
            {
                c.EnableAnnotations();

                c.SwaggerDoc( "properties", new OpenApiInfo
                {
                    Title = "Properties API",
                    Version = "v1",
                } );
                c.SwaggerDoc( "reservations", new OpenApiInfo
                {
                    Title = "Reservations API",
                    Version = "v1",
                } );

                c.DocInclusionPredicate( ( doc, api ) =>
                    string.Equals( api.GroupName, doc, StringComparison.OrdinalIgnoreCase ) );
            } );

            builder.Services.AddControllers();

            builder.Services.AddBindings();
            builder.Services.AddBookingManagerDatabase( builder.Configuration );

            WebApplication app = builder.Build();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            if ( app.Environment.IsDevelopment() )
            {
                app.UseSwagger();
                app.UseSwaggerUI( c =>
                {
                    c.SwaggerEndpoint( "/swagger/properties/swagger.json", "Properties API" );
                    c.SwaggerEndpoint( "/swagger/reservations/swagger.json", "Reservations API" );
                } );
            }

            app.MapControllers();
            app.InitBookingManagerDatabase();

            app.Run();
        }
    }
}