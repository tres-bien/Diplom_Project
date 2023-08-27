using Azure.Core;
using FluentValidation;
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

        public async Task<Bill> CreateBill(Bill newBillRespond)
        {
            _context.Bill.Add(newBillRespond);

            await _context.SaveChangesAsync();
            return newBillRespond;
        }

        public async Task<List<Bill>?> GetAllBills()
        {
            return await _context.Bill.Include(x => x.Members).ToListAsync();
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
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();
            await _context.SaveChangesAsync();

            return await _context.Bill.ToListAsync();
        }

        public async Task<Bill> UpdateBillById(int id, Bill request)
        {
            var bill = await GetById(id);

            bill!.Name = request.Name;
            bill.Total = request.Total;

            bill.Members.Clear();

            foreach (var memberRequest in request.Members)
            {
                bill.Members.Add(memberRequest);
            }

            await _context.SaveChangesAsync();

            return bill;
        }

        private async Task<List<Bill>> GetByName(string name)
        {
            var bill = await _context.Bill.Include(x => x.Members)
                                          .Where(x => x.Name == name)
                                          .ToListAsync();
            if (bill == null) return null!;
            return bill;
        }

        private async Task<Bill?> GetById(int id)
        {
            var bill = await _context.Bill.Include(b => b.Members)
                                          .FirstOrDefaultAsync(b => b.Id == id);

            if (bill == null)
            {
                return null;
            }

            return bill;
        }
    }
}
