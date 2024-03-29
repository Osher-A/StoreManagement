﻿using System.Collections.Generic;

namespace StoreManager.Interfaces
{
    public interface IProductRepository
    {
        void AddingProduct(IProduct newusersProduct);
        void DeletingProduct(int productId);
        void UpdateProduct(IProduct productToEdit);
        List<IProduct> GetAllProducts();
    }
}