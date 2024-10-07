using AutoMapper;
using StoreManagement.Application.DTOs.Auth;
using StoreManagement.Application.DTOs.Request;
using StoreManagement.Application.DTOs.Response;
using StoreManagement.Domain.Models;

namespace StoreManagement.Application.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() {
            //Request
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

            //Response
            CreateMap<User, UserResponse>().ReverseMap();

            /*.ForMember(u => u.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(u => u.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(u => u.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(u => u.Phones, opt => opt.MapFrom(src => src.Phones))*/
            CreateMap<Store, StoreResponse>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<Food, FoodResponse>().ReverseMap();
            CreateMap<Table, TableResponse>().ReverseMap();
            CreateMap<Voucher, VoucherResponse>().ReverseMap();
            CreateMap<PaymentType, PaymentTypeResponse>().ReverseMap();
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailResponse>().ReverseMap();
            CreateMap<Invoice, InvoiceResponse>().ReverseMap();
            CreateMap<ProductSell, ProductSellResponse>().ReverseMap();
        }
    }
}
