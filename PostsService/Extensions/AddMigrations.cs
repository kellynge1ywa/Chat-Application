namespace PostsService;
using Microsoft.EntityFrameworkCore;

public static class AddMigrations
{
    public static IApplicationBuilder UseMigrations(this IApplicationBuilder app){
        using(var scope =app.ApplicationServices.CreateScope()){
            var db=scope.ServiceProvider.GetRequiredService<ChatDbContext>();
            if(db.Database.GetPendingMigrations().Count()>0){
                db.Database.Migrate();
            }
        }
        return app;
    }

}
