namespace Order.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler
    (IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        // get orders with pagination
        // return result

        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .OrderBy(o => o.OrderName.Value)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetOrdersResult
        (
            new paginatedResult<OrderDto>
            (
                totalCount: totalCount,
                pageIndex: pageIndex,
                pageSize: pageSize,
                orders: orders.ToOrderDtoList()
            )
        );
    }
}
