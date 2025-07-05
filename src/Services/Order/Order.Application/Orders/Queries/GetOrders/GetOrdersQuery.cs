﻿using BuildingBlocks.Pagination;

namespace Order.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery
    (PaginationRequest PaginationRequest)
    : IQuery<GetOrdersResult>;

public record GetOrdersResult(PaginatedResult<OrderDto> Orders);