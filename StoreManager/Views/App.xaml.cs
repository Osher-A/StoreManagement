﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;
using StoreManager.DAL;
using StoreManager.Interfaces;
using StoreManager.Services.MessageService;
using StoreManager.Services.OrderControllerServices;
using StoreManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StoreManager.Views
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMessageService>(new MessageService());
            services.AddSingleton<IOrderRepository>(new OrderRepository());
            services.AddSingleton<ICustomerRepository>(new CustomerRepository());
            services.AddSingleton<IOrderProductRepository>(new OrderProductRepository());
            services.AddSingleton<IEmailSmtpService>(new EmailSmtpService());
            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}



 

        