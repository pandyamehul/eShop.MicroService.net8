using Discount.gRPC.Data;
using Discount.gRPC.Models;
using Discount.Grpc;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services;

public class DiscountService
    (DiscountContext discountDbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await discountDbContext
            .Coupones
            .FirstOrDefaultAsync(c => 
                c.ProductName == request.ProductName
            );
        if (coupon == null)
        {
            coupon = new Coupon { ProductName = "No Product", Amount = 0, Description = "No Discount"};
        }
        logger.LogInformation("Discount is retrieved for Product: {productName}, Amount : {amount} ", coupon.ProductName, coupon.Amount);
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon=request.Coupon.Adapt<Coupon>();
        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request Parameter : "));

        }

        discountDbContext.Coupones.Add(coupon);
        await discountDbContext.SaveChangesAsync();

        logger.LogInformation(
            "Discount is successfully created. Product: {productName}, Amount : {amount}"
            , coupon.ProductName
            , coupon.Amount
        );
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request Parameter : "));

        }

        discountDbContext.Coupones.Update(coupon);
        await discountDbContext.SaveChangesAsync();

        logger.LogInformation(
            "Discount is successfully updated. Product: {productName}, Amount : {amount}"
            , coupon.ProductName
            , coupon.Amount
        );
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await discountDbContext
            .Coupones
            .FirstOrDefaultAsync(c =>
                c.ProductName == request.ProductName
            );
        if (coupon == null)
        {
            coupon = new Coupon { ProductName = "No Product", Amount = 0, Description = "No Discount" };
        }
        discountDbContext.Coupones.Remove(coupon);
        await discountDbContext.SaveChangesAsync();

        logger.LogInformation(
            "Discount is successfully Deleted. Product: {productName}"
            , coupon.ProductName
        );
        return new DeleteDiscountResponse { Success = true };
    }
}
