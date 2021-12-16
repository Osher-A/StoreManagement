﻿using MyLibrary.Utilities;
using StoreInventory.DAL;
using StoreInventory.DTO;
using StoreInventory.Services.OrderControllerServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace StoreInventory.ViewModel
{
    public class OrdersControllerViewModel : INotifyPropertyChanged
    {
        private Order _selectedOrder = new Order();
        private OrderSearchService _orderSearchService = new OrderSearchService(new OrderRepository());
        private ObservableCollection<Order> searchOrders;

        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }
        public ObservableCollection<Order> AllOrders { get; private set; } = new ObservableCollection<Order>();
        public ObservableCollection<Order> UnpaidOrders { get; private set; } = new ObservableCollection<Order> { };

        public ObservableCollection<Order> SearchOrders
        {
            get => searchOrders;
            private set
            {
                searchOrders = value;
                OnPropertyChanged(nameof(SearchOrders));
            }
        }
        public string Date { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public ICommand UpdateOrderCommand { get; private set; }
        public ICommand ClearSelectedOrderCommand { get; private set; }
        public ICommand EmailReminderCommand { get; private set; }
        public ICommand SearchByDateCommand { get; private set; }
        public ICommand SearchByNameCommand { get; private set; }
        public ICommand SearchByAddressCommand { get; private set; }
        public ICommand SearchByEmailCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public OrdersControllerViewModel()
        {
            FieldsDefaultSetting();
            SearchOrders = new ObservableCollection<Order>();
            UpdateOrderCommand = new CustomCommand(UpdateOrder, CanUpdateOrder);
            ClearSelectedOrderCommand = new CustomCommand(ClearFields, CanClearFields);
            EmailReminderCommand = new CustomCommand(SendEmail, CanSendEmail);
            SearchByDateCommand = new CustomCommand(SearchByDate, CanSearchByDate);
            SearchByNameCommand = new CustomCommand(SearchByName, CanSearchByName);
            SearchByAddressCommand = new CustomCommand(SearchByAddress, CanSearchByAddress);
            SearchByEmailCommand = new CustomCommand(SearchByEmail, CanSearchByEmail);
            LoadData();
        }

        private void SendEmail(object obj)
        {
           // var email = new EmailService();
            //email.SendEmail(SelectedOrder);
            EmailSmtpService.SendEmail(SelectedOrder);
        }

        private bool CanSendEmail(object obj)
        {
            return SelectedOrder.AmountOwed > 0;
        }

        private void ClearFields(object obj)
        {
            FieldsDefaultSetting();
        }

        private bool CanClearFields(object obj)
        {
            return SelectedOrder.Id != 0;
        }

        private void UpdateOrder(object obj)
        {
            var orderUpdateService = new OrderUpdateService(new OrderRepository());
            orderUpdateService.UpdateOrder(SelectedOrder);
            LoadData();
        }

        private bool CanUpdateOrder(object obj)
        {
           return SelectedOrder.Id != 0;
        }

        private void SearchByDate(object obj)
        {
            // Clearing out other fields
            Name = null; Address = null; Email = null; 
            SearchOrders = _orderSearchService.SearchByDate(Convert.ToDateTime(Date));
        }

        private bool CanSearchByDate(object obj)
        {
            return !String.IsNullOrWhiteSpace(Date);
        }
        private void SearchByName(object obj)
        {
            Date = null; Address = null; Email = null;
            SearchOrders = _orderSearchService.SearchByName(Name);
        }

        private bool CanSearchByName(object obj)
        {
            return !String.IsNullOrEmpty(Name);
        }
        private void SearchByAddress(object obj)
        {
            Date = null; Name = null; Email = null;
            SearchOrders = _orderSearchService.SearchByAddress(Address);
        }

        private bool CanSearchByAddress(object obj)
        {
            return !string.IsNullOrWhiteSpace(Address);
        }

        private void SearchByEmail(object obj)
        {
            Date = null;  Name = null; Address = null;
            SearchOrders = _orderSearchService.SearchByEmail(Email);
        }

        private bool CanSearchByEmail(object obj)
        {
            return !String.IsNullOrWhiteSpace(Email);
        }

        private void LoadData()
        {
            AllOrders = _orderSearchService.AllOrders;
            UnpaidOrders = _orderSearchService.GetUnPaidOrders();
        }

        private void FieldsDefaultSetting()
        {
            SelectedOrder = new Order() { Customer = new Customer() { Address = new Address() } };
        }
        private void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
