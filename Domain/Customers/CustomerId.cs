namespace Domain.Customers;

//Objeto tipado
public record CustomerId(Guid Value)
{
    public static implicit operator string(CustomerId value)
    {
        throw new NotImplementedException();
    }
}
