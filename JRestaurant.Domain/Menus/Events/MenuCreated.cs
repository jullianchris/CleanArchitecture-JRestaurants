using JRestaurant.Domain.Common.Models;

namespace JRestaurant.Domain.Menus.Events;

public record MenuCreated(Menu Menu) : IDomainEvent;