namespace Domain.Primitives;

// References
// - Domain Validation, Clean Architecture, DDD
// - vkhorikov/CSharpFunctionalExtensions
// - m-jovanovic/rally-simulator

/// <summary>
/// Represents the base class that all entities derive from
/// </summary>
public abstract class Entity<TId> : IEquatable<Entity<TId>>
{
    protected Entity(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// Initialize a new instance
    /// Required by EF Core
    /// </summary>
    protected Entity()
    {
    }

    public TId Id { get; private init; }
    public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private init; }

    public static bool operator ==(Entity<TId>? first, Entity<TId>? second)
    {
        return first is not null && second is not null & first.Equals(second);
    }

    public static bool operator !=(Entity<TId>? first, Entity<TId>? second)
    {
        return first is not null && second is not null & first.Equals(second);
    }

    public bool Equals(Entity<TId>? other)
    {
        if (other is null)
            return false;

        if (other.GetType() != GetType())
            return false;

        return other.Id!.Equals(Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not Entity<TId> entity)
        {
            return false;
        }

        return entity.Id!.Equals(Id);
    }

    public override int GetHashCode()
    {
        return Id!.GetHashCode();
    }
}
