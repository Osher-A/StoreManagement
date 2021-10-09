﻿using MyLibrary.Utilities;
using StoreInventory.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreInventory.Model
{
    public class Order : IOrder
    {
        public int Id { get; set; }
        public ICustomer Customer { get; set; } 
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public float Total { get; set; }
        public float AmountPaid { get; set; }
        public List<OrderProduct> OrdersProducts { get; set; }

        public Order()
        {
            OrdersProducts = new List<OrderProduct>();
        }
    }
}
