using CommentsService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add swagger extension
builder.AddSwaggenGenExtension();

//Add controller services
builder.Services.AddControllers();

//Add automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Configure base url
builder.Services.AddHttpClient("Posts",k=>k.BaseAddress=new Uri(builder.Configuration.GetValue<string>("ServiceURL:PostService")));

//
builder.Services.AddScoped<IComment, CommentsServices>();
builder.Services.AddScoped<IPosts,PostServices>();
builder.Services.AddScoped<IcommentImage,ImagesServices>();
//Configure database
builder.Services.AddDbContext<ChatDbContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myConnections"));
});
//Custome services - from extensions folder
builder.AddAuth();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();




//Add migration class
app.UseMigrations();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

