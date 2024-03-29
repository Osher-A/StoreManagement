﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using StoreManager.Interfaces;
using StoreManager.Services.MessageService;
using StoreManager.Services.OrderControllerServices;
using StoreManager.Services.StockServices;
using StoreManager.ViewModel;
using StoreManager.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToastNotifications.Core;
using ToastNotifications.Lifetime.Clear;

namespace StoreManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private HomePage _homePage;
        private InventoryPage _InventoryPage;
        private OrderPage _orderPage;
        private OrdersControllerPage _orderControllerPage;
        private IMessageService _messageService;
        private ToastViewModel _toastVm;
        public MainWindow(IMessageService messageService, IOrderRepository orderRepository,
            ICustomerRepository customerRepository, IOrderProductRepository orderProductRepository, IEmailSmtpService emailSmtpService)
        {
            InitializeComponent();
            SetUpPages(messageService, orderRepository, customerRepository, orderProductRepository, emailSmtpService);
            SetUpMessageServices(messageService);

        }
        private void SetUpPages(IMessageService messageService, IOrderRepository orderRepository,
            ICustomerRepository customerRepository, IOrderProductRepository orderProductRepository, IEmailSmtpService emailSmtpService)
        {
            _homePage = new HomePage();
            _homePage.TileClick += OpenClickedPage;
            _InventoryPage = new InventoryPage();
            _InventoryPage.Navigator += NavigationButtonClicked;
            _InventoryPage.PageNavigator += OpenClickedPage;
            _orderPage = new OrderPage(messageService, orderRepository, customerRepository, orderProductRepository);
            _orderPage.Navigator += NavigationButtonClicked;
            _orderPage.PageNavigator += OpenClickedPage;
            _orderControllerPage = new OrdersControllerPage(emailSmtpService);
            _orderControllerPage.Navigator += NavigationButtonClicked;
            _orderControllerPage.PageNavigator += OpenClickedPage;
        }
        private void OpenClickedPage(string page)
        {
            switch(page.ToLower().Trim())
            {
                case "stock":
                      MainWindowFrame.Content = _InventoryPage;
                    break;
                case "order":
                    MainWindowFrame.Content = _orderPage;
                    break;
                case "ordercontroller":
                    MainWindowFrame.Content = _orderControllerPage;
                    break;
                case "users":
                case "settings":
                case "reports":
                    NotImplementedToast();
                    break;
                    default:
                    MainWindowFrame.Content = _homePage;
                    break;
            }
        }
        private void SetUpMessageServices(IMessageService messageService)
        {
            _messageService = messageService;
            _messageService.OkMessageBoxEvent += MahAppsOKMessageBox;
            _messageService.OkAndCancelMessageBoxEvent += MahAppsOkAndCancelMessageBox;

            _toastVm = new ToastViewModel();
            ToastService.ToastSuccessAction += ShowSuccess;
            ToastService.ToastErrorAction += ShowError;
            ToastService.ToastInfoAction += ShowInfo;
        }

        private void NotImplementedToast()
        {
            ShowInfo("This page has not been implemented yet!");
        }
        private async Task MahAppsOKMessageBox(string heading, string message)
        {
            await this.ShowMessageAsync(heading, message, MessageDialogStyle.Affirmative);
        }

        private async Task<bool> MahAppsOkAndCancelMessageBox(string heading, string message)
        {
            if (await this.ShowMessageAsync(heading, message, MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
                return true;
            else
                return false;
        }
        private void ShowError(string message)
        {
            _toastVm.ShowError("ERROR!" + "  " + message);
        }

        private void ShowSuccess()
        {
            _toastVm.ShowSuccess("Email Sent!");
        }

        private void ShowInfo(string message)
        {
            _toastVm.ShowInformation(message);
        }

        private void MainWindowFrame_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = _homePage;

        }

        private void NavigationButtonClicked(string direction)
        {
            if (MainWindowFrame.NavigationService.CanGoBack)
                if (direction.Trim().ToLower() == "backwards")
                MainWindowFrame.NavigationService.GoBack();
                else if(MainWindowFrame.NavigationService.CanGoForward)
                MainWindowFrame.NavigationService.GoForward();

        }

    }
}