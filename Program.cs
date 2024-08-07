using Backend;
using Backend.ManejoDeRepositorios;
using Blazor.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ChartJs.Blazor;
using ChartJs.Blazor.BarChart;
using Microsoft.EntityFrameworkCore;
using Backend.SQL;
using AppDataContext = Backend.Context.AppDataContext;
using Backend.Controllers;
using Backend.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<Backend.MemoryDatabase>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<Backend.MemoryDatabase>();
builder.Services.AddSingleton<Backend.ManejoDeRepositorios.RepositorioReserva>();
builder.Services.AddSingleton<Backend.ManejoDeRepositorios.RepositorioDeposito>();
builder.Services.AddSingleton<Backend.ManejoDeRepositorios.RepositorioPromocion>();
builder.Services.AddSingleton<Backend.ManejoDeRepositorios.RepositorioValoracion>();
builder.Services.AddSingleton<Backend.ManejoDeRepositorios.RepositorioUsuario>();
builder.Services.AddScoped<Backend.ManejoDeRepositorios.RepositorioDeposito>();
builder.Services.AddSingleton<Backend.ManejoDeRepositorios.SessionLogic>();
builder.Services.AddSingleton<Backend.ManejoDeRepositorios.RepositorioRegistroAcciones>();

// SqlRepositorios
builder.Services.AddScoped<Backend.SQL.SqlRepositorioReserva>();
builder.Services.AddScoped<Backend.SQL.SqlRepositorioDeposito>();
builder.Services.AddScoped<Backend.SQL.SqlRepositorioPromocion>();
builder.Services.AddScoped<Backend.SQL.SqlRepositorioValoracion>();
builder.Services.AddScoped<Backend.SQL.SqlRepositorioUsuario>();
builder.Services.AddScoped<Backend.SQL.SqlRepositorioRegistroAcciones>();
builder.Services.AddScoped<Backend.SQL.SqlRepositorioNotificacion>();

// Servicios
builder.Services.AddScoped<Backend.Services.ServicioReserva>();
builder.Services.AddScoped<Backend.Services.ServicioUsuario>();
builder.Services.AddScoped<Backend.Services.ServicioPromocion>();
builder.Services.AddScoped<Backend.Services.ServicioRegistroAccion>();
builder.Services.AddScoped<Backend.Services.ServicioValoracion>();
builder.Services.AddScoped<Backend.Services.ServicioDeposito>();
builder.Services.AddScoped<Backend.Services.ServicioSessionLogic>();
builder.Services.AddScoped<Backend.Services.ServicioNotificacion>();

// Controladores
builder.Services.AddScoped<Backend.Controllers.ControllerReserva>();
builder.Services.AddScoped<Backend.Controllers.ControllerAdministrador>();
builder.Services.AddScoped<Backend.Controllers.ControllerUsuario>();
builder.Services.AddScoped<Backend.Controllers.ControllerPromocion>();
builder.Services.AddScoped<Backend.Controllers.ControllerRegistroAccion>();
builder.Services.AddScoped<Backend.Controllers.ControllerValoracion>();
builder.Services.AddScoped<Backend.Controllers.ControllerDeposito>();
builder.Services.AddScoped<Backend.Controllers.ControllerNotificacion>();

builder.Services.AddDbContextFactory<AppDataContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        providerOptions => providerOptions.EnableRetryOnFailure())
    );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
