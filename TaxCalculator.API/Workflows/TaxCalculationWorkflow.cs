using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using TaxCalculator.Interfaces;
using TaxCalculator.Models;
using TaxCalculator.RESTAPI.Exceptions;

namespace TaxCalculator.Workflows
{
    public class TaxCalculationWorkflow
    {
        private readonly ITaxCalculationRepository _taxCalculationRepository;
        private readonly ITaxCalculatorFactory _taxCalculatorFactory;

        public TaxCalculationWorkflow(ITaxCalculationRepository taxCalculationRepository,
            ITaxCalculatorFactory taxCalculatorFactory)
        {
            _taxCalculationRepository = taxCalculationRepository;
            _taxCalculatorFactory = taxCalculatorFactory;
        }

        public async Task<decimal> CalculateTax(string taxType, decimal annualIncome, string postalCode)
        {
            if (postalCode == "Select Postal Code")
            {
                throw new CustomException("Please select Postal Code", (int)HttpStatusCode.BadRequest);
            }

            if (annualIncome < 0)
            {
                throw new CustomException("Annual income should be greater than zero", (int)HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrEmpty(taxType))
            {
                throw new CustomException("Tax Type is required", (int)HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrEmpty(postalCode))
            {
                throw new CustomException("Postal Code is required", (int)HttpStatusCode.BadRequest);
            }

            var taxCalculator = _taxCalculatorFactory.GetTaxCalculator(taxType);

            if (taxCalculator != null)
            {
                return await taxCalculator.CalculateTax(annualIncome);
            }

            throw new CustomException("Unknown Tax Type", (int)HttpStatusCode.BadRequest);
        }

        public async Task SaveCalculatedTax(decimal taxPayable, decimal annualIncome, string postalCode)
        {
            var calculatedTaxResult = new TaxCalculationResult
            {
                Date = DateTime.Now,
                AnnualIncome = annualIncome,
                PostalCode = postalCode,
                CalculatedTax = taxPayable
            };

            await _taxCalculationRepository.SaveCalculatedTax(calculatedTaxResult);
        }

        public async Task<List<TaxCalculationType>> GetTaxCalculationTypes()
        {
            return await _taxCalculationRepository.GetTaxCalculationTypes();
        }
    }
}
