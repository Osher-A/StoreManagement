﻿using StoreInventory.Interfaces;
using StoreInventory.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreInventory.DAL
{
    public class OrderProductRepository
    {
        public void UpdateOrderProducts(List<IOrderProduct> orderProducts)
        {
            using var db = new StoreContext();
            foreach (var op in orderProducts)
            {
                var orderProduct = new Model.OrderProduct() 
                {
                    OrderId = op.OrderId, ProductId = op.ProductId, Quantity = op.Quantity 
                };
                db.OrdersProducts.Add(orderProduct);
            }
            db.SaveChanges();
        }
    }
}


































