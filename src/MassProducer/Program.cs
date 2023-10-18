using MassProducer.Dtos;
using MassShared.Events;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
    {
        busFactoryConfigurator.Host("rabbitmq", hostConfigurator => { });
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapPost("/notification", async (
    NotificationDto notificationDto,
    IPublishEndpoint publishEndpoint) =>
{
    await publishEndpoint.Publish<INotificationCreated>(new
    {
        NotificationDate    = notificationDto.Date,
        NotificationMessage = notificationDto.Message,
        NotificationType    = notificationDto.NotificationType
    });

    return Results.Ok();
})
.WithOpenApi();

app.Run();

