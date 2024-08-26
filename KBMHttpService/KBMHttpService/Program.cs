using KBMHttpService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcClient<KBMGrpcService.Protos.OrganizationService.OrganizationServiceClient>(option =>
{
    option.Address = new Uri("https://localhost:7048");
});
builder.Services.AddGrpcClient<KBMGrpcService.Protos.UserService.UserServiceClient>(option =>
{
    option.Address = new Uri("https://localhost:7048");
});

// Add services to the container.
builder.Services.AddScoped<IOrganizationService, KBMHttpService.Services.OrganizationService>();
builder.Services.AddScoped<IUserService, KBMHttpService.Services.UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
