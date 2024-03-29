﻿using StoreManager.DTO;
using StoreManager.Enums;
using StoreManager.Interfaces;
using StoreManager.Services.MessageService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.Services.OrderServices
{
    public class PaymentService
    {
        private readonly IMessageService _messageService;

        public bool IsDetailsValid { get; private set; }

        public PaymentService(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public PaymentAmounts PaymentController(PaymentStatus? ps, float totalCost)
        {
            PaymentAmounts pa = new PaymentAmounts();
            switch (ps)
            {
                case PaymentStatus.FullyPaid:
                    pa.AmountPaid = totalCost;
                    break;
                case PaymentStatus.NotPaid:
                    pa.AmountOwed = totalCost;
                    break;
            }
            return pa;
        }

        public void CustomersDetailsValidated(PaymentStatus? paymentStatus, ICustomer customer)
        {
            if ((paymentStatus == PaymentStatus.NotPaid || paymentStatus == PaymentStatus.PartlyPaid) && ((string.IsNullOrWhiteSpace(customer.Address.House) || string.IsNullOrWhiteSpace(customer.Address.Zip))
                && string.IsNullOrWhiteSpace(customer.Email)))
            {
               _messageService.CustomersAddressDetailsMissingAlert();
                IsDetailsValid = false;
            }
            else
                IsDetailsValid = true;
        }

        public PaymentStatus? UpdatePaymentStatus(bool addingItem, PaymentStatus? paymentStatus)
        {
            if (addingItem)
                if (paymentStatus == PaymentStatus.FullyPaid)
                    return PaymentStatus.PartlyPaid;
                else
                    return paymentStatus;
            else
                if (paymentStatus == PaymentStatus.FullyPaid)
                    return paymentStatus;

                return null; // this would make sure there is no checked radio button.
        }

        public float OutStandingBallance(float amountPaid, float totalCost)
        {
            return totalCost - amountPaid;
        }
    }
}


