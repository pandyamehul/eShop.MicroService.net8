using Grpc.Net.Client;
using Discount.Grpc;

// create a new gRPC channel to connect to the server
using var channel = GrpcChannel.ForAddress("https://localhost:5052");
// create a new gRPC client for the DiscountProtoService
var client = new DiscountProtoService.DiscountProtoServiceClient(channel);

// -- GET DISCOUNT --
// create a request and call the GetDiscountAsync method
var request = new GetDiscountRequest { ProductName = "IPhone X" };
// Calling the GetDiscountAsync method to get product discount information
var response = await client.GetDiscountAsync(request);
Console.WriteLine("\nGrpc Message Response: " + response);
Console.WriteLine($"\nProduct: {response.ProductName}, \nDescription: {response.Description}, \nAmount: {response.Amount}\n");

// --CREATE DISCOUNT--
var createDiscount = new CreateDiscountRequest
{
    Coupon = new CouponModel
    {
        ProductName = "Nokia 3310",
        Description = "Launch discount",
        Amount = 75
    }
};
var createdDiscountResponse = await client.CreateDiscountAsync(createDiscount);
Console.WriteLine("\nGrpc Message Response: " + createdDiscountResponse + ". \n");

// -- UPDATE DISCOUNT --
var updateDiscount = new UpdateDiscountRequest
{
    Coupon = new CouponModel
    {
        Id = 4,
        ProductName = "IPhone 16 Pro",
        Description = "Launch discount updated",
        Amount = 190
    }
};
var updatedDiscountResponse = await client.UpdateDiscountAsync(updateDiscount);
Console.WriteLine("\nGrpc Message Response: " + updatedDiscountResponse + ". \n");

// -- DELETE DISCOUNT --
var deleteDiscount = new DeleteDiscountRequest { ProductName = "Nokia 3310" };
var deletedDiscountResponse = await client.DeleteDiscountAsync(deleteDiscount);
Console.WriteLine("\nGrpc Message Response: " + deletedDiscountResponse + ". \n");
