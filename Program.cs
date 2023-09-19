using FluentValidation;
using SSRRC1.Components;
using SSRRC1.Components.Pages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddServerComponents();

builder.Services.AddSingleton<Checkout.IProductStore, Checkout.ProductStore>();
builder.Services.AddTransient<IValidator<Checkout.PlaceOrderCommand>, PlaceOrderCommandValidator>();

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

app.MapRazorComponents<App>()
    .AddServerRenderMode();

app.Run();

public class PlaceOrderCommandValidator : AbstractValidator<Checkout.PlaceOrderCommand>
{
    public PlaceOrderCommandValidator()
    {
        RuleFor(x => x.BillingAddress.Name).NotEmpty().WithMessage("dumbass");
    }
}
