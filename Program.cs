using Communication;
using DBservice;
using DBService;
using Dto;
using Service;
using Structure;
using UserService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UserLoginDBService>();

builder.Services.AddScoped<UserLoginService>();


builder.Services.AddScoped<UserRegistrationDBService>();

builder.Services.AddScoped<UserRegistrationService>();
builder.Services.AddScoped<IInstitute, InstitutionRegistration>();
builder.Services.AddScoped<RegisterInstituteService>();


builder.Services.AddScoped<IInstituteFilters, InstituteFilters>();
builder.Services.AddScoped<FilterInstituteData>();

builder.Services.AddScoped<IGovernanceType, GovernanceType>();
builder.Services.AddScoped<GovernanceTypeService>();

builder.Services.AddScoped<IStatus,InstitutionStatus>();
builder.Services.AddScoped<StatusService>();

builder.Services.AddScoped<IUserInstitution, UserInstitute>();
builder.Services.AddScoped<UserInstituteService>();

builder.Services.AddScoped<IGovernance, GovernanceDB>();
builder.Services.AddScoped<GovernanceService>();

//------------------------ Facility ---------------------------

builder.Services.AddScoped<FacilityDBService>();
builder.Services.AddScoped<IFacilityService, FacilityDBService>();
builder.Services.AddScoped<FacilityService>();

 builder.Services.AddHttpClient();

 // ----------------------------  COMMUNICATION SERVICE ------------------
 
builder.Services.AddScoped<CommunicationServiceRequest>();
builder.Services.AddScoped<UtilityServiceRequest>();

//--------------------------------- USER DETAILS --------------------------
builder.Services.AddScoped<UserDetailsDBService>();
builder.Services.AddScoped<UserDetailsService>();

//-----------------------------  URL -----------------------------------

builder.Services.AddScoped<UrlDBService>();
builder.Services.AddScoped<IUrl, UrlDBService>();
builder.Services.AddScoped<UrlService>();

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
