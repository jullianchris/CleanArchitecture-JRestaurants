using JRestaurant.Domain.Common.Models;
using JRestaurant.Domain.Guests.ValueObjects;

namespace JRestaurant.Domain.Guests;

public sealed class Guest : AggregateRoot<GuestId, Guid>
{
    public Guest(GuestId id) : base(id)
    {
    }

#pragma warning disable CS8618
    private Guest() { }
#pragma warning restore CS8618
}