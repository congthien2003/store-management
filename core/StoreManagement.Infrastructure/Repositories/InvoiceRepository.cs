using StoreManagement.Infrastructure.Data;
using StoreManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using StoreManagement.Domain.IRepositories;
using DocumentFormat.OpenXml.Bibliography;

namespace StoreManagement.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository<Invoice>
    {
        private readonly DataContext _dataContext;

        public InvoiceRepository(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }
        public async Task<Invoice> CreateAsync(Invoice invoice)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(x => x.Id == invoice.IdOrder);
            if(order == null)
            {
                throw new InvalidOperationException("Order không tồn tại");
            }
            var paymentType = await _dataContext.PaymentTypes.FirstOrDefaultAsync(x => x.Id == invoice.IdPaymentType);
            if(paymentType == null)
            {
                throw new InvalidOperationException("Thể loại thanh toán không tồn tại");
            }
            var existsInvoice = await _dataContext.Invoices.FirstOrDefaultAsync(x => x.IdOrder == invoice.IdOrder);
            if (existsInvoice != null)
            {
                throw new InvalidOperationException("Đơn đặt hàng này đã được tạo hóa đơn");
            }
            var newInvoice = await _dataContext.Invoices.AddAsync(invoice);
            await _dataContext.SaveChangesAsync();
            return newInvoice.Entity;
        }

        public async Task<Invoice> DeleteAsync(int id, bool incluDeleted = false)
        {
            var invoice = await _dataContext.Invoices.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if(invoice == null)
            {
                throw new InvalidOperationException("Hóa đơn này không tồn tại");
            }
            _dataContext.Invoices.Remove(invoice);
            await _dataContext.SaveChangesAsync();
            return invoice;
        }
        public Expression<Func<Invoice, object>> GetSortColumnExpression(string sortColumn)
        {
            switch (sortColumn)
            {
                default:
                    return x => x.Id;
            }
        }
        public Task<List<Invoice>> GetAllByIdStoreAsync(int idStore, string sortCol = "", bool ascSort = true, bool incluDeleted = false)
        {
            var invoice = _dataContext.Invoices.Include("Order").Include("PaymentType").Where(x => x.Order.Table.IdStore == idStore && x.IsDeleted == incluDeleted).AsQueryable();
            if (!incluDeleted)
            {
                invoice = invoice.Where(t => t.IsDeleted == incluDeleted);
            }
            if (!string.IsNullOrEmpty(sortCol))
            {
                if (ascSort)
                {
                    invoice = invoice.OrderByDescending(GetSortColumnExpression(sortCol.ToLower()));
                }
                else
                {
                    invoice = invoice.OrderBy(GetSortColumnExpression(sortCol.ToLower()));

                }
            }
            return invoice.ToListAsync();
        }

        public async Task<Invoice> GetByIdAsync(int id, bool incluDeleted = false)
        {
            var invoice = await _dataContext.Invoices.Include("Order").Include("PaymentType").FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == incluDeleted);
            if (invoice == null)
            {
                throw new KeyNotFoundException("Không tìm thấy hóa đơn");
            }
            return invoice;
        }

        public async Task<int> GetCountAsync(int idStore, bool incluDeleted = false)
        {
            var invoice = _dataContext.Invoices.Where(x => x.Order.Table.IdStore == idStore && x.IsDeleted == incluDeleted).AsQueryable();
            if (!incluDeleted)
            {
                invoice = invoice.Where(t => t.IsDeleted == incluDeleted);
            }
            var searchInvoice = await invoice.ToListAsync();
            return searchInvoice.Count();
        }

        public async Task<Invoice> UpdateAsync(int id, Invoice invoice, bool incluDeleted = false)
        {
            var invoiceUpdate = await _dataContext.Invoices.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == incluDeleted);
            if (invoiceUpdate == null)
            {
                throw new KeyNotFoundException("Không tìm thấy hóa đơn");
            }
            invoiceUpdate.Status = invoice.Status;
            _dataContext.Invoices.Update(invoiceUpdate);
            await _dataContext.SaveChangesAsync();
            return invoiceUpdate;
        }

        public async Task<double> GetDailyRevenueService(int idStore, DateTime dateTime, bool incluDeleted = false)
        {
            var invoice = _dataContext.Invoices.Where(x => x.Order.Table.IdStore == idStore && x.CreatedAt.Date == dateTime.Date && x.IsDeleted == incluDeleted)
                                                .Include(x => x.Order)
                                                .ThenInclude(x => x.Table);
            double totalRevenue = await invoice.SumAsync(i => (double)i.Total);
            return totalRevenue;
        }

        public async Task<double> GetMonthRevenueAsync(int idStore,int month, int year, bool incluDeleted = false)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            var monthlyInvoices = await _dataContext.Invoices
                .Where(x => x.Order.Table.IdStore == idStore &&
                            x.CreatedAt >= startDate &&
                            x.CreatedAt <= endDate &&
                            x.IsDeleted == incluDeleted)
                .ToListAsync();

            double totalRevenue = monthlyInvoices.Sum(invoice => (double)invoice.Total);

            return totalRevenue;
        }
    }
}
