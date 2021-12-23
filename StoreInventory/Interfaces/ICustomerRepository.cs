﻿using System.Collections.Generic;

namespace StoreInventory.Interfaces
{
    public interface ICustomerRepository
    {
        void AddNewCustomer(ICustomer newUiCustomer);
        void DeletingCustomer(int customerId);
        List<ICustomer> GetCustomers();
        List<IOrder> GetCustomersOrder(int Id);
        int GetLastCustomerId();
        void UpdateCustomer(ICustomer customerToEdit);
    }
}