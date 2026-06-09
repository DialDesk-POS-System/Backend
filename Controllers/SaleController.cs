using AutoMapper;
using DialDesk.Server.DTOs;
using DialDesk.Server.DTOs.Sale;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace DialDesk.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public SaleController(ISaleService saleService, IEmailService emailService, IMapper mapper)
        {
            _saleService = saleService;
            _emailService = emailService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<SaleOutDto>>> GetAll()
        {
            var sales = await _saleService.GetAllSalesAsync();
            return Ok(_mapper.Map<List<SaleOutDto>>(sales));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SaleOutDto>> GetById(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null) return NotFound();
            return Ok(_mapper.Map<SaleOutDto>(sale));
        }

        [HttpGet("by-invoice/{invoiceNo}")]
        public async Task<ActionResult<SaleOutDto>> GetByInvoice(string invoiceNo)
        {
            var sale = await _saleService.GetSaleByInvoiceNo(invoiceNo);
            if (sale == null) return NotFound();
            return Ok(_mapper.Map<SaleOutDto>(sale));
        }

        [HttpPost("search")]
        public async Task<ActionResult<List<SaleOutDto>>> Search([FromBody] SaleSearchDto filter)
        {
            var sales = await _saleService.SearchSalesAsync(filter);
            return Ok(_mapper.Map<List<SaleOutDto>>(sales));
        }

        [HttpPost]
        public async Task<ActionResult<SaleOutDto>> Create([FromBody] SaleCreateDto dto)
        {
            var sale = _mapper.Map<Sale>(dto);
            var created = await _saleService.CreateSaleAsync(sale);
            return CreatedAtAction(nameof(GetById), new { id = created!.Id }, _mapper.Map<SaleOutDto>(created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<SaleOutDto>> Update(int id, [FromBody] SaleUpdateDto dto)
        {
            var updated = await _saleService.UpdateSaleAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(_mapper.Map<SaleOutDto>(updated));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _saleService.DeleteSaleAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPost("{id:int}/send-email")]
        public async Task<IActionResult> SendInvoiceEmail(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null) return NotFound();

            if (string.IsNullOrWhiteSpace(sale.CustomerEmail))
                return BadRequest("Customer email is not provided for this sale.");

            await _emailService.SendInvoiceEmailAsync(
                sale.CustomerEmail,
                sale.CustomerName ?? "Valued Customer",
                sale.InvoiceNo,
                sale.TotalAmount,
                sale.SaleItems
            );

            return Ok(new { message = $"Invoice email sent to {sale.CustomerEmail}" });
        }
    }
}

