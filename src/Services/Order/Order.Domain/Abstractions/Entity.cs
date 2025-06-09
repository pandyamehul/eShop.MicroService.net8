namespace Order.Domain.Abstractions;
public abstract class Entity<T> : Entity<T>
{
    public T Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? ModifiedBy { get; set; }
}