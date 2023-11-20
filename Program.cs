using System.Data;
using CarteiraClientes.Infrastructure.Mappling;
using CarteiraClientes.Infrastructure.Repository;
using CarteiraClientes.Infrastructure.Validators.Client;
using CarteiraClientes.Infrastructure.Validators.Company;
using CarteiraClientes.Services;
using CarteiraClientes.ViewModels.Client;
using CarteiraClientes.ViewModels.Company;
using FluentValidation.AspNetCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// IDBConnection to help with Dapper
builder.Services.AddScoped<IDbConnection>(x =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("Default"))
);

// DbContext - EFCore
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Mapster Service
builder.Services.RegisterMapsterConfiguration();

// Pagination Service
builder.Services.AddScoped<IPaginationService, PaginationService>();
builder.Services.AddCloudscribePagination();

// Fluent Validator Service 
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<AddClientViewModel>, AddClientValidador>();
builder.Services.AddScoped<IValidator<UpdateClientViewModel>, UpdateClientValidator>();
builder.Services.AddScoped<IValidator<AddCompanyViewModel>, AddCompanyValidator>();
builder.Services.AddScoped<IValidator<UpdateCompanyViewModel>, UpdateCompanyValidator>();

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