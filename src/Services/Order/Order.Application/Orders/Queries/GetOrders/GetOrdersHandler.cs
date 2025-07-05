using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Order.Application.Extensions;

namespace Order.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler
    (IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
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
            new PaginatedResult<OrderDto>
            (
                count: totalCount,
                pageIndex: pageIndex,
                pageSize: pageSize,
                data: orders.ToOrderDtoList()
            )
        );
    }
}
