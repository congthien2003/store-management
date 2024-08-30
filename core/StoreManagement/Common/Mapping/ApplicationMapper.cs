using AutoMapper;
using StoreManagement.Application.DTOs;
using StoreManagement.Application.DTOs.Auth;
using StoreManagement.Domain.Models;

namespace StoreManagement.Application.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, RegisterDTO>().ReverseMap();

            /*.ForMember(u => u.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(u => u.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(u => u.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(u => u.Phones, opt => opt.MapFrom(src => src.Phones))*/
            CreateMap<Store, StoreDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Food, FoodDTO>().ReverseMap();
            CreateMap<Table, TableDTO>().ReverseMap();
            CreateMap<Voucher, VoucherDTO>().ReverseMap();
            CreateMap<PaymentType, PaymentTypeDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<Invoice, InvoiceDTO>().ReverseMap();
            CreateMap<ProductSell, ProductSellDTO>().ReverseMap();
        }
    }
}
