﻿using Library.Infrastructure.InjectionFramework;
using Library.Service.Identity.Domain;
using Library.Service.Identity.Domain.DataAccessors;
using Library.Service.Identity.Domain.ViewModels;
using Library.Service.Identity.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Library.Service.Identity
{
    [Route("api/identities")]
    public class IdentityController : Controller
    {
        private IIdentityReportDataAccessor _dataAccessor = null;
        private IPasswordHasher _passwordHasher = null;

        public IdentityController()
        {
            _dataAccessor = InjectContainer.GetInstance<IIdentityReportDataAccessor>();
            _passwordHasher = InjectContainer.GetInstance<IPasswordHasher>();
        }

        [Route("")]
        [HttpPost]
        public IdentityViewModel GetIdentity(IdentityDTO dto)
        {
            if (_passwordHasher == null)
            {
                throw new Exception("The password hasher is not initialized.");
            }

            return _dataAccessor.GetIdentity(dto.UserName, dto.Password);
        }

        [HttpGet("~/api/customers")]
        public List<CustomerListViewModel> GetCustomers()
        {
            return _dataAccessor.GetCustomerList();
        }

        [HttpGet("~/api/customers/{customerId}")]
        public CustomerListViewModel GetCustomerDetails(Guid customerId)
        {
            return _dataAccessor.GetCustomerSingleListViewModel(customerId);
        }

        [HttpGet("~/api/accounts/{accountId}")]
        public IdentityDetailsViewModel GetAccount(Guid accountId)
        {
            return _dataAccessor.GetAccountDetails(accountId);
        }
    }
}