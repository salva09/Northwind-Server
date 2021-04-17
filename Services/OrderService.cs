using System;
using System.Linq;
using sample.DataAccess;

class OrderService : NorthwindService
{
    public Order GetOrderById(int id)
    {
        if (!Exists(id)) return null;

        var order = DbContext.Orders.First(e => e.OrderId == id);
        return new Order
        {
            OrderId = order.OrderId,
            CustomerId = order.CustomerId,
            ShipName = order.ShipName,
            ShipAddress = order.ShipAddress,
            ShipCity = order.ShipCity,
            ShipRegion = order.ShipRegion,
            ShipPostalCode = order.ShipPostalCode,
            ShipCountry = order.ShipCountry
        };
    }

    public bool Exists(int id)
    {
        try
        {
            DbContext.Orders.First(e => e.OrderId == id);
        }
        catch
        {
            return false;
        }
        return true;
    }
}
