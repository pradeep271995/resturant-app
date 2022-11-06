using RestaurantApp.Models;
using RestaurantApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantApp.Repositories
{
    public class OrderRepository
    {
        private RestaurantAppEntities objRestaurantAppEntities;
        public OrderRepository()
        {
            objRestaurantAppEntities = new RestaurantAppEntities();
        }

        public bool AddOrder(OrderViewModel objOrderViewModel)
        {
            Order objOrder = new Order();
            objOrder.CustomerId = objOrderViewModel.CustomerId;
            objOrder.FinalTotal = objOrderViewModel.FinalTotal;
            objOrder.OrderDate = DateTime.Now;
            objOrder.OrderNumber = String.Format("{0:ddmmyyyyhhmmss}", DateTime.Now);
            objOrder.PaymentTypeId = objOrderViewModel.PaymentTypeId;
            objRestaurantAppEntities.Orders.Add(objOrder);
            objRestaurantAppEntities.SaveChanges();
            int OrderId = objOrder.OrderId;

            foreach (var item in objOrderViewModel.listOrderDetailViewModel)
            {
                OrderDetail objOrderDetail = new OrderDetail();
                objOrderDetail.OrderId = OrderId;
                objOrderDetail.Discount = item.Discount;
                objOrderDetail.ItemId = item.ItemId;
                objOrderDetail.Total = item.Total;
                objOrderDetail.UnitPrice = item.UnitPrice;
                objOrderDetail.Quantity = item.Quantity;
                objRestaurantAppEntities.OrderDetails.Add(objOrderDetail);
                objRestaurantAppEntities.SaveChanges();

                Trasanction objTrasanction = new Trasanction();
                objTrasanction.ItemId = item.ItemId;
                objTrasanction.Quantity = (-1)*item.Quantity;
                objTrasanction.TrasanctionDate = DateTime.Now;
                objTrasanction.TypeId = 2;
                objRestaurantAppEntities.Trasanctions.Add(objTrasanction);
                objRestaurantAppEntities.SaveChanges();

            }
            return true;
        }
    }
}