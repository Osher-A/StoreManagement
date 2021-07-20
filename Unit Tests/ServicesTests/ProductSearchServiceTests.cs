﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using StoreInventory.DAL.Interfaces;
using StoreInventory;
using StoreInventory.Services;
using StoreInventory.DTO;

namespace Unit_Tests.ServicesTests
{
    [TestFixture]
    public class ProductSearchServiceTests
    {
        private Mock<IProductRepository> _mockRepository;
        private ProductService _productSearchService;
        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IProductRepository>();
            _mockRepository.Setup(mr => mr.GetProducts()).Returns(ModelProducts());
            _productSearchService = new ProductService(_mockRepository.Object);
        }

        [Test]
        public void AllProducts_WhenSelected_ReturnsAConvertedListOfDTOProducts()
        {
            //Arrange

            //Act
           var dtoProducts = _productSearchService.AllProducts;

            // Assert
            Assert.That(dtoProducts, Is.TypeOf<List<StoreInventory.DTO.Product>>());
            Assert.That(dtoProducts.Count == 3);
        }

        [Test]
        public void ValidProductToAdd_IfNewPropertyIsValid_ReturnTrue()
        {
            // Arrange
            var newProduct = new StoreInventory.DTO.Stock()
            {
                Product = new StoreInventory.DTO.Product
                {
                    Id = 1,
                    Category = new StoreInventory.DTO.Category { Name = "Food" },
                    Name = "Danish",
                    Description = "Yummy Danish",
                    Price = 1.50f
                },
                QuantityInStock = 8
            } ;

            // Act
           var isValid = _productSearchService.ValidProductToAdd(newProduct);

            //Assert
            Assert.That(isValid == true);
        }
        [Test]
        [TestCase(0, "", "Bun", "Yummy Bun", 1.05f, 3) ]
        [TestCase(0, "Home", "", "Large Clock", 12.05f, 2)]
        [TestCase(0, "Food", "Coke", "", 1.50f, 5)]
        [TestCase(0, "Home", "Bin", "Large Bin", 0, 1)]
        [TestCase(0, "Food", "Cheese", "Hard Cheese", 1.05f, 0)]

        public void ValidProductToAdd_IfNewPropertyHasNotBeenFullySet_ReturnFalse
            (int id,string categoryName, string name, string description, float price, int quantity)
        {
            // Arrange
            var newStock = new StoreInventory.DTO.Stock()
            { Product = new StoreInventory.DTO.Product
            {
                Id = id,
                Name = name,
                Description = description,
                Price = price,
                Category = new StoreInventory.DTO.Category { Name = categoryName }
            }, QuantityInStock = quantity };

            // Act
            var isValid = _productSearchService.ValidProductToAdd(newStock);

            //Assert
            Assert.That(isValid == false);
        }

        [Test]
        public void ExistingProduct_IfIdenticelToExistingProduct_ReturnTrue()
        {
            //Arrange
            var existingProduct = new Product()
            {
                Id = 0,
                Category = new Category { Name = "Food" },
                Name = "Danish",
                Description = "Yummy Danish",
                Price = 1.50f
            };

            //Act
            var productExists = _productSearchService.ExistingProduct(existingProduct);

            //Assert
            Assert.That(productExists, Is.True);
        }

        [Test]
        public void ExistingProduct_IfNewProduct_ReturnFalse()
        {
            //Arrange
            var newProduct = new Product()
            {
                Id = 0,
                Category = new Category { Name = "Food" },
                Name = "Danish",
                Description = "Large Danish",
                Price = 2.50f
            };

            //Act
           var productExists = _productSearchService.ExistingProduct(newProduct);

            //Assert
            Assert.That(productExists == false);
        }

        private List<StoreInventory.Model.Product> ModelProducts()
        {
            return new List<StoreInventory.Model.Product>
            {
                new StoreInventory.Model.Product
                {
                 Id = 1,
                 Category = new StoreInventory.Model.Category{Name = "Food"},
                 Name = "Danish",
                 Description = "Yummy Danish",
                 Price = 1.50f
                },
                new StoreInventory.Model.Product
                {
                 Id = 2,
                 Category = new StoreInventory.Model.Category{Name = "Clothes"},
                 Name = "Shirt",
                 Description = "Smart Shirt",
                 Price = 11.50f
                },
                new StoreInventory.Model.Product
                {
                 Id = 3,
                 Category = new StoreInventory.Model.Category{Name = "Home"},
                 Name = "Mirror",
                 Description = "Big Mirror",
                 Price = 23.00f
                },
            };
        }

    }
}