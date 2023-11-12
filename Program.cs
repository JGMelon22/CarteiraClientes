using System.Data;
using CarteiraClientes.Infrastructure.Data;
using CarteiraClientes.Infrastructure.Repository;
using CarteiraClientes.Infrastructure.Validators.Client;
using CarteiraClientes.Infrastructure.Validators.Company;
using CarteiraClientes.Interfaces;
using CarteiraClientes.ViewModels.Client;
using CarteiraClientes.ViewModels.Company;
using FluentValidation;
using FluentValidation.AspNetCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// IDBConnection to help with Dapper
builder.Services.AddScoped<IDbConnection>(x =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("Default"))
);

// DbContext - EFCore
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Fluent Validator Service 
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<AddClientViewModel>, AddClientValidador>();
builder.Services.AddScoped<IValidator<UpdateClientViewModel>, UpdateClientValidator>();
builder.Services.AddScoped<IValidator<AddCompanyViewModel>, AddCompanyValidator>();
builder.Services.AddScoped<IValidator<UpdateCompanyViewModel>, UpdateCompanyValidator>();

// Interface & Repository register
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();