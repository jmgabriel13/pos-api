using Application.Interfaces;
using Domain.Orders;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Orders.Queries.GetOrderSummary;
internal sealed class GetOrderSummaryQueryHandler : IQueryHandler<GetOrderSummaryQuery, OrderSummary>
{
    private readonly IOrderSummaryRepository _orderSummaryRepository;

    public GetOrderSummaryQueryHandler(IOrderSummaryRepository orderSummaryRepository)
    {
        _orderSummaryRepository = orderSummaryRepository;
    }

    public async Task<Result<OrderSummary>> Handle(GetOrderSummaryQuery request, CancellationToken cancellationToken)
    {
        var summary = await _orderSummaryRepository
            .GetSummaryByIdAsync(request.OrderId, cancellationToken);

        if (summary is null)
        {
            return Result.Failure<OrderSummary>(new Error("OrderSummary.NotFound", "There is no summary for this order"));
        }

        return summary;
    }
}
