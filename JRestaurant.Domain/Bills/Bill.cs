using BuberDinner.Domain.Bills.ValueObjects;
using JRestaurant.Domain.Bills.ValueObjects;
using JRestaurant.Domain.Common.Models;
using JRestaurant.Domain.Dinners.ValueObjects;
using JRestaurant.Domain.Guests.ValueObjects;
using JRestaurant.Domain.Hosts.ValueObjects;

namespace JRestaurant.Domain.Bills;

public sealed class Bill : AggregateRoot<BillId, Guid>
{
    public DinnerId DinnerId { get; }
    public GuestId GuestId { get; }
    public HostId HostId { get; }
    public Price Price { get; }
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

    private Bill(
        BillId id,
        GuestId guestId,
        HostId hostId,
        Price price,
        DateTime createdDateTime,
        DateTime updatedDateTime)
        : base(id)
    {
        GuestId = guestId;
        HostId = hostId;
        Price = price;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Bill Create(
        GuestId guestId,
        HostId hostId,
        Price price)
    {
        return new(
            BillId.CreateUnique(),
            guestId,
            hostId,
            price,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }

#pragma warning disable CS8618
    private Bill() { }
#pragma warning restore CS8618
}