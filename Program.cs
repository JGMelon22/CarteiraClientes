using CarteiraClientes.Infrastructure.Mappling;
using CarteiraClientes.Infrastructure.Repository;
using CarteiraClientes.Infrastructure.Validators.Client;
using CarteiraClientes.Infrastructure.Validators.Company;
using CarteiraClientes.Services;
using CarteiraClientes.ViewModels.Client;
using CarteiraClientes.ViewModels.Company;
using FluentValidation.AspNetCore;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DbContext - EFCore
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


// IDBConnection to help with Dapper
builder.Services.AddScoped<IDbConnection>(x =>
    new SqlConnection(builder.Configuration.GetConnectionString("Default")));

// Mapster Service
builder.Services.RegisterMapsterConfiguration();

// Pagination Service
builder.Services.AddScoped<IPaginationService, PaginationService>();
builder.Services.AddCloudscribePagination();

// Fluent Validator Service 
builder.Services.AddFluentValidationAutoValidation(); // Server Side
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters(); // Client Side
builder.Services.AddTransient<IValidator<AddClientViewModel>, AddClientValidador>();
builder.Services.AddTransient<IValidator<UpdateClientViewModel>, UpdateClientValidator>();
builder.Services.AddTransient<IValidator<AddCompanyViewModel>, AddCompanyValidator>();
builder.Services.AddTransient<IValidator<UpdateCompanyViewModel>, UpdateCompanyValidator>();

// Interface & Repository register
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IClientCompanyRepository, ClientCompanyRepository>();

builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService>();

// Service Pattern DI
builder.Services.AddScoped<ChartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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