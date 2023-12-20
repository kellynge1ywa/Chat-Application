using AuthService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connecting to database
builder.Services.AddDbContext<ChatDbContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myConnections"));
});

//Add automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//configure identity framework
builder.Services.AddIdentity<ChatUser, IdentityRole>().AddEntityFrameworkStores<ChatDbContext>();

//add controller services
builder.Services.AddControllers();

//add services
builder.Services.AddScoped<Iuser,UserServices>();
builder.Services.AddScoped<IJwt, JwtService>();

//configure JWT options class
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


