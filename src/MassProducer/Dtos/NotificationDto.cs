using MassShared.Events;

namespace MassProducer.Dtos;

public record NotificationDto(DateTime Date,string? Message,NotificationType NotificationType);