using DentalClinic.DTOs.PatientDTO;
using DentalClinic.Services.PatientService;
using DentalClinic.Services.PaymentTypeService;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeController : Controller
    {
        private readonly IPaymentTypeService _paymentTypeService;
        public PaymentTypeController(IPaymentTypeService paymentTypeService)
        {
            _paymentTypeService = paymentTypeService;
        }
        [HttpPost]
        public async Task<ActionResult> AddPaymentType(string str)
        {
            try
            {

                return Ok(await _paymentTypeService.AddPaymentType(str));
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the payment Type.");
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetPaymentTypes()
        {
            try
            {

                return Ok(await _paymentTypeService.GetAllPaymentTypes());
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while returning the payment types.");
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeletePaymentTypes(int id)
        {
            try
            {

                return Ok(await _paymentTypeService.RemovePaymentType(id));
            }
            //return Ok(await _employeeService.AddEmployee(employeeDTO));            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while removing the Payment Type.");
            }
        }

    }
}
