using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

using TaxCalculator.Models;
using TaxCalculator.Workflows;

namespace TaxCalculator.RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : Controller
    {
        private readonly TaxCalculationWorkflow _taxCalculationWorkflow;

        public TaxController(TaxCalculationWorkflow taxCalculationWorkflow)
        {
            _taxCalculationWorkflow = taxCalculationWorkflow;
        }

        [HttpGet]
        [Route("tax-payable")]
        public async Task<ActionResult<decimal>> CalculateTax(decimal annualIncome, string postalCode, string calculationType)
        {
            var taxPayable = await _taxCalculationWorkflow.CalculateTax(calculationType, annualIncome, postalCode);
            await _taxCalculationWorkflow.SaveCalculatedTax(taxPayable, annualIncome, postalCode);

            return Ok(taxPayable);
        }

        [HttpGet]
        [Route("tax-calculation-types")]
        public async Task<ActionResult<List<TaxCalculationType>>> GetTaxCalculationTypes()
        {
            var taxCalculationTypes = await _taxCalculationWorkflow.GetTaxCalculationTypes();
            return Ok(taxCalculationTypes);
        }
    }
}
