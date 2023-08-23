using Microsoft.AspNetCore.Authorization;
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
        private readonly ILogger<BillController> _logger;

        public BillController(IBillService billService, ILogger<BillController> logger)
        {
            _billService = billService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<Bill>?>> GetAllBills()
        {
            _logger.LogInformation("GET ALL BILLS");
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
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<Bill>> CreateBill(Bill newBillRespond)
        {
            var bill = await _billService.CreateBill(newBillRespond);
            return CreatedAtAction(nameof(GetBillById), new { id = bill.Id }, bill);
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Bill>> Clear()
        {
            await _billService.Clear();
            return Ok(_billService);
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Bill>> UpdateBillById(int id, Bill request)
        {
            var bill = await _billService.UpdateBillById(id, request);
            if (bill == null)
                return NotFound("Sorry, but this bill is does't exist");

            return Ok(_billService);
        }
    }
}
