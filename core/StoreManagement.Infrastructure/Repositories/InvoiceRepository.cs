using StoreManagement.Infrastructure.Data;
using StoreManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using StoreManagement.Domain.IRepositories;

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
            if (order == null)
            {
                throw new InvalidOperationException("Order không tồn tại");
            }
            var paymentType = await _dataContext.PaymentTypes.FirstOrDefaultAsync(x => x.Id == invoice.IdPaymentType);
            if (paymentType == null)
            {
                throw new InvalidOperationException("Thể loại thanh toán không tồn tại");
            }
            if (invoice.Voucher != null)
            {
                var voucher = await _dataContext.Vouchers.FirstOrDefaultAsync(x => x.Id == invoice.IdVoucher);
                if (voucher == null)
                {
                    invoice.IdVoucher = null;
                }
            }
            var newInvoice = await _dataContext.Invoices.AddAsync(invoice); ;
            await _dataContext.SaveChangesAsync();
            return newInvoice.Entity;
        }

        public async Task<Invoice> DeleteAsync(int id, bool incluDeleted = false)
        {
            var invoice = await _dataContext.Invoices.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (invoice == null)
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
        public Task<List<Invoice>> GetAllByIdStoreAsync(int idStore, int currentPage = 1, int pageSize = 5, string sortCol = "", bool ascSort = true, bool incluDeleted = false)
        {
            var invoice = _dataContext.Invoices.Include(x => x.PaymentType).Include(x => x.Order).Include(x => x.Voucher).Where(x => x.Order.Table.IdStore == idStore && x.IsDeleted == incluDeleted).AsQueryable();
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
            var list = invoice.Skip(currentPage * pageSize - pageSize).Take(pageSize).ToListAsync();
            return list;
        }

        public async Task<Invoice> GetByIdAsync(int id, bool incluDeleted = false)
        {
            var invoice = await _dataContext.Invoices.Include(x => x.PaymentType).Include(x => x.Order).Include(x => x.Voucher).FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == incluDeleted);
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
            invoiceUpdate.IdVoucher = invoice.IdVoucher;
            _dataContext.Invoices.Update(invoiceUpdate);
            await _dataContext.SaveChangesAsync();
            return invoiceUpdate;
        }
    }
}
