public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder Use保哥專用的例外處理程式(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            try
            {
                // Routing

                await next();

                // Routing
            }
            catch (Exception ex)
            {
                // Log the exception
            }
        });

        return app;
    }
}
