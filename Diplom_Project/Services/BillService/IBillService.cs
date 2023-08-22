using System;

namespace Diplom_Project
{
    public interface IBillService
    {
        Task<List<Bill>?> GetAllBills();
        Task<List<Bill>?> GetBillByName(string name);
        Task<Bill>? GetBillById(int id);
        Task<Bill> CreateBill(Bill newBillRespond);
        Task<List<Bill>?> RemoveBillByName(string name);
        Task<List<Bill>?> RemoveBillById(int id);
        Task<Bill> UpdateBillById(int id, Bill request);
        Task<List<Bill>?> Clear();
    }
}
