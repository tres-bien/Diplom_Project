using Microsoft.EntityFrameworkCore;
using System;

namespace Diplom_Project
{
    public class BillService : IBillService
    {
        private readonly DataContext _context;

        public BillService(DataContext context)
        {
            _context = context;
        }

        public async Task<Bill> CreateBill(CreateBillRespond newBillRespond)
        {
            string members = string.Empty;
            string amountPaid = string.Empty;

            foreach (var member in newBillRespond.Members)
            {
                members += member.Member + ";";
                amountPaid += member.AmountPaid + ";";
            }

            var bill = new Bill
            {
                Name = newBillRespond.Name,
                Member = members,
                AmountPaid = amountPaid,
                Total = newBillRespond.Total
            };
            _context.Bill.Add(bill);

            await _context.SaveChangesAsync();
            return bill;
        }

        public async Task<List<Bill>?> GetAllBills()
        {
            return await _context.Bill.ToListAsync();
        }

        public async Task<List<Bill>?> GetBillByName(string name)
        {
            var bill = await GetByName(name);
            return bill;
        }

        public async Task<Bill>? GetBillById(int id)
        {
            var bill = await GetById(id);
            return bill!;
        }

        public async Task<List<Bill>?> RemoveBillByName(string name)
        {
            var bills = await GetByName(name);
            foreach (var bill in bills)
            {
                _context.Bill.Remove(bill);
            }
            await _context.SaveChangesAsync();

            return await _context.Bill.ToListAsync();
        }

        public async Task<List<Bill>?> RemoveBillById(int id)
        {
            var bill = await GetById(id);
            _context.Bill.Remove(bill!);
            await _context.SaveChangesAsync();

            return await _context.Bill.ToListAsync();
        }

        public async Task<List<Bill>?> Clear()
        {
            var dbSet = _context.Set<Bill>();
            dbSet.RemoveRange(dbSet);
            await _context.SaveChangesAsync();

            return await _context.Bill.ToListAsync();
        }

        public async Task<Bill> UpdateBillById(int id, CreateBillRespond request)
        {
            string members = string.Empty;
            string amountPaid = string.Empty;

            var bill = await GetById(id);

            foreach (var member in request.Members)
            {
                members += member.Member + ";";
                amountPaid += member.AmountPaid + ";";
            }

            bill!.Name = request.Name;
            bill.Member = members;
            bill.AmountPaid = amountPaid;
            bill.Total = request.Total;

            await _context.SaveChangesAsync();

            return bill;
        }

        private async Task<List<Bill>> GetByName(string name)
        {
            var bill = await _context.Bill.Where(x => x.Name == name).ToListAsync();
            if (bill == null) return null!;
            return bill;
        }

        private async Task<Bill?> GetById(int id)
        {
            var bill = await _context.Bill.FindAsync(id);
            if (bill == null) return null;
            return bill;
        }
    }
}
