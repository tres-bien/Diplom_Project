using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Diplom_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<Bill>?>> GetAllBills()
        {
            return await _billService.GetAllBills();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<List<Bill>>> GetBillByName(string name)
        {
            var bill = await _billService.GetBillByName(name);
            return bill!;
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Bill>> GetBillById(int id)
        {
            var bill = await _billService.GetBillById(id)!;
            return bill;
        }

        [HttpPost]
        [Route("CreateBill")]
        public async Task<ActionResult<List<Bill>>> CreateBill(CreateBillRespond newBillRespond)
        {
            await _billService.CreateBill(newBillRespond);
            return Ok(_billService);
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult<Bill>> RemoveBillByName(string name)
        {
            var bill = await _billService.RemoveBillByName(name);
            if (bill == null)
                return NotFound("Sorry, but this bill is does't exist");

            return Ok(_billService);
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Bill>> RemoveBillById(int id)
        {
            var bill = await _billService.RemoveBillById(id);
            if (bill == null)
                return NotFound("Sorry, but this bill is does't exist");

            return Ok(_billService);
        }

        [HttpDelete]
        [Route("Clear")]
        public async Task<ActionResult<Bill>> Clear()
        {
            await _billService.Clear();
            return Ok(_billService);
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Bill>> UpdateBillById(int id, CreateBillRespond request)
        {
            var bill = await _billService.UpdateBillById(id, request);
            if (bill == null)
                return NotFound("Sorry, but this bill is does't exist");

            return Ok(_billService);
        }
    }
}
