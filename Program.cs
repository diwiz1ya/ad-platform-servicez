using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AdPlatformServiceApp;               // Для IAdPlatformService и AdPlatformService
using AdPlatformServiceApp.Filters;       // Для FileUploadOperationFilter

var builder = WebApplication.CreateBuilder(args);

// Регистрируем сервис для работы с рекламными площадками
builder.Services.AddSingleton<IAdPlatformService, AdPlatformService>();

// Добавляем контроллеры
builder.Services.AddControllers();

// Настраиваем Swagger и регистрируем фильтр для загрузки файлов
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<FileUploadOperationFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
