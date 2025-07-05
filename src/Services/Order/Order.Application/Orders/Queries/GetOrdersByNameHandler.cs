
using Microsoft.EntityFrameworkCore;
using Order.Application.Extensions;

namespace Order.Application.Orders.Queries;

public class GetOrdersByNameHandler
    (IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        //get orders by name
        // return result
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking() // Use AsNoTracking for read-only queries to improve performance
            .Where(o => o.OrderName.Value.Contains(query.Name, StringComparison.OrdinalIgnoreCase))
            .OrderBy(o => o.OrderName)
            .ToListAsync(cancellationToken);
        return new GetOrdersByNameResult(orders.ToOrderDtoList());
    }    
}
