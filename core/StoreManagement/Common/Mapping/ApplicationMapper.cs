﻿using AutoMapper;
using StoreManagement.Application.DTOs.Auth;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Request.Combo;
using StoreManagement.Application.DTOs.Request.ComboItem;
using StoreManagement.Application.DTOs.Request.OrderDetail;
using StoreManagement.Application.DTOs.Request.Store;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Application.DTOs.Response.BankInfo;
using StoreManagement.Application.DTOs.Response.Combo;
using StoreManagement.Application.DTOs.Response.KPI;
using StoreManagement.Application.DTOs.Response.OrderDetail;
using StoreManagement.Domain.Models;

namespace StoreManagement.Application.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<Store, StoreDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Food, FoodDTO>().ReverseMap();
            CreateMap<Table, TableDTO>().ReverseMap();
            CreateMap<PaymentType, PaymentTypeDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<Invoice, InvoiceDTO>().ReverseMap();
            CreateMap<ProductSell, ProductSellDTO>().ReverseMap();
            CreateMap<OrderAccessToken, OrderAccessTokenDTO>().ReverseMap();
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<Invoice, InvoiceResponse>().ReverseMap();
            CreateMap<Table, TableResponse>().ReverseMap();
            CreateMap<Store, StoreResponse>().ReverseMap();
            CreateMap<KPI, KPIResponse>().ReverseMap();
            CreateMap<OrderAccessToken, OrderAccessTokenResponse>().ReverseMap();
            CreateMap<BankInfo, BankInfoDTO>().ReverseMap();
            CreateMap<BankInfo, BankInfoResponse>().ReverseMap();
            CreateMap<OrderDetail, DataByIdStoreRes>().ReverseMap();
            CreateMap<Combo, ComboDTO>().ReverseMap();
            CreateMap<ComboItem, ComboItemDTO>().ReverseMap();
            CreateMap<Combo, CreateComboReq>().ReverseMap();
            CreateMap<Combo, ComboWithFood>().ReverseMap();
            CreateMap<Ticket, TicketDTO>().ReverseMap();
            CreateMap<Ticket, TicketResponse>().ReverseMap();
            CreateMap<Staff, StaffDTO>().ReverseMap();
            CreateMap<Staff, StaffResponse>().ReverseMap();
        }
    }
}
