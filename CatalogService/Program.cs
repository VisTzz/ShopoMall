using CatalogService.Application.Common.Behaviors;
using CatalogService.Application.Common.Validators;
using CatalogService.Domain.Repositories;
using CatalogService.Infrastructure.Data;
using CatalogService.Infrastructure.Middleware;
using CatalogService.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Подключаем БД
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Регистрируем репозитории
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// 3. Подключаем MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CatalogService.Application.Categories.Commands.CreateCategory.CreateCategoryCommand).Assembly);
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});

// 4. Добавляем FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(CreateCategoryCommandValidator).Assembly);

// 5. Контроллеры
builder.Services.AddControllers();

// 6. Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Catalog Service", Version = "v1" });
});

var app = builder.Build();

// 7. Middleware для обработки ошибок валидации
app.UseMiddleware<ValidationExceptionMiddleware>();

// 8. Применяем миграции при запуске (только для Development)
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.EnsureCreated();
        Console.WriteLine("Database ensured created");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
