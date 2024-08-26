using StoreManagement.DataAccess.Data;
using StoreManagement.Domain.Interfaces;
using StoreManagement.Domain.Interfaces.IRepositories;
using StoreManagement.Domain.Models;
using StoreManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context) {
            _context = context;
            
        }

        public IUserRepository<User> UserRepository { get; private set; }

        public IStoreRepository<Store> StoreRepository { get; private set; }

        public ICategoryRepository<Category> CategoryRepository { get; private set; }

        public IFoodRepository<Food> FoodRepository { get; private set; }

        public ITableRepository<Table> TableRepository { get; private set; }

        public IVoucherRepository<Voucher> VoucherRepository { get; private set; }

        public IPaymentTypeRepository<PaymentType> PaymentTypeRepository { get; private set; }

        public IOrderRepository<Order> OrderRepositoy { get; private set; }

        public IOrderDetailRepository<OrderDetail> OrderDetailRepository { get; private set; }

        public IInvoiceRepository<Invoice> InvoiceRepository { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges(); 
        }
    }
}
