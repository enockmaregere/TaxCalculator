using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using System.Threading.Tasks;

using TaxCalculator.Factories;
using TaxCalculator.RESTAPI.Constants;
using TaxCalculator.RESTAPI.Controllers;
using TaxCalculator.RESTAPI.Exceptions;
using TaxCalculator.UnitTests.Mocks;
using TaxCalculator.Workflows;

namespace TaxCalculator.UnitTests
{
    [TestFixture]
    public class TaxCalculationControllerUnitTests
    {
        private TaxController _taxController;

        public TaxCalculationControllerUnitTests()
        {
        }

        [SetUp]
        public void SetUp()
        {
            var mockTaxCalculationRepository = new MockTaxCalculationRepository();
            var taxCalculatorFactory = new TaxCalculatorFactory(mockTaxCalculationRepository);
            var taxCalculationWorkflow = new TaxCalculationWorkflow(mockTaxCalculationRepository, taxCalculatorFactory);

            _taxController = new TaxController(taxCalculationWorkflow);
        }

        [Test]
        public async Task CalculateProgressiveTax_FirstBracket_LowerLimit()
        {
            var result = await _taxController.CalculateTax(0.00M, PostalCodes.PostalCode7441, TaxTypes.Progressive);
            Assert.AreEqual(0.00, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateProgressiveTax_AnnualIncomeInFirstBracket()
        {
            var result = await _taxController.CalculateTax(8000.00M, PostalCodes.PostalCode1000, TaxTypes.Progressive);
            Assert.AreEqual(800.00, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateProgressiveTax_FirstBracket_UpperLimit()
        {
            var result = await _taxController.CalculateTax(8350, PostalCodes.PostalCode7441, TaxTypes.Progressive);
            Assert.AreEqual(835.00, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateProgressiveTax_SecondBracket_LowerLimit()
        {
            var result = await _taxController.CalculateTax(8351.00M, PostalCodes.PostalCode1000, TaxTypes.Progressive);
            Assert.AreEqual(835.15, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateProgressiveTax_AnnualIncomeInSecondBracket()
        {
            var result = await _taxController.CalculateTax(25000.00M, PostalCodes.PostalCode7441, TaxTypes.Progressive);
            Assert.AreEqual(3332.50, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateProgressiveTax_SecondBracket_UpperLimit()
        {
            var result = await _taxController.CalculateTax(33950.00M, PostalCodes.PostalCode1000, TaxTypes.Progressive);
            Assert.AreEqual(4675.00, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateProgressiveTax_ThirdBracket_LowerLimit()
        {
            var result = await _taxController.CalculateTax(33951.00M, PostalCodes.PostalCode7441, TaxTypes.Progressive);
            Assert.AreEqual(4675.10, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateProgressiveTax_AnnualIncomeInThirdBracket()
        {
            var result = await _taxController.CalculateTax(72532.53M, PostalCodes.PostalCode1000, TaxTypes.Progressive);
            Assert.AreEqual(14320.48, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateProgressiveTax_ThirdBracket_UpperLimit()
        {
            var result = await _taxController.CalculateTax(82250.00M, PostalCodes.PostalCode7441, TaxTypes.Progressive);
            Assert.AreEqual(16749.85, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateProgressiveTax_FourthBracket_LowerLimit()
        {
            var result = await _taxController.CalculateTax(82251.00M, PostalCodes.PostalCode1000, TaxTypes.Progressive);
            Assert.AreEqual(16749.88, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateProgressiveTax_AnnualIncomeInFourthBracket()
        {
            var result = await _taxController.CalculateTax(92532.22M, PostalCodes.PostalCode7441, TaxTypes.Progressive);
            Assert.AreEqual(19628.62, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateProgressiveTax_FourthBracket_UpperLimit()
        {
            var result = await _taxController.CalculateTax(171550.00M, PostalCodes.PostalCode1000, TaxTypes.Progressive);
            Assert.AreEqual(41753.60, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateProgressiveTax_FifthBracket_LowerLimit()
        {
            var result = await _taxController.CalculateTax(171551.00M, PostalCodes.PostalCode7441, TaxTypes.Progressive);
            Assert.AreEqual(41753.65, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateProgressiveTax_AnnualIncomeInFifthBracket()
        {
            var result = await _taxController.CalculateTax(211825.16M, PostalCodes.PostalCode1000, TaxTypes.Progressive);
            Assert.AreEqual(55044.12, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateProgressiveTax_FifthBracket_UpperLimit()
        {
            var result = await _taxController.CalculateTax(372950.00M, PostalCodes.PostalCode7441, TaxTypes.Progressive);
            Assert.AreEqual(108215.32, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateProgressiveTax_LastBracket_LowerLimit()
        {
            var result = await _taxController.CalculateTax(372951.00M, PostalCodes.PostalCode7441, TaxTypes.Progressive);
            Assert.AreEqual(108215.34, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateProgressiveTax_AnnualIncomeInLastBracket()
        {
            var result = await _taxController.CalculateTax(500000.00M, PostalCodes.PostalCode1000, TaxTypes.Progressive);
            Assert.AreEqual(152682.49, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateFlatValueTax_LessThanThresholdIncome()
        {
            var result = await _taxController.CalculateTax(150000.00M, PostalCodes.PostalCodeA100, TaxTypes.FlatValue);
            Assert.AreEqual(7500.00, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateFlatValueTax_MoreThanThresholdIncome()
        {
            var result = await _taxController.CalculateTax(250000.00M, PostalCodes.PostalCodeA100, TaxTypes.FlatValue);
            Assert.AreEqual(10000.00, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateFlatValueTax_ThresholdIncome()
        {
            var result = await _taxController.CalculateTax(200000.00M, PostalCodes.PostalCodeA100, TaxTypes.FlatValue);
            Assert.AreEqual(10000.00, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public async Task CalculateFlatRateTax_MoreThanThresholdIncome()
        {
            var result = await _taxController.CalculateTax(250000.00M, PostalCodes.PostalCode7000, TaxTypes.FlatRate);
            Assert.AreEqual(43750.00, (result.Result as OkObjectResult).Value);
        }

        [Test]
        public void NegativeIncomeIsPassed_ExceptionIsThrown()
        {
            var exception = Assert.Throws<CustomException>(() => _taxController.CalculateTax(-8000.00M, PostalCodes.PostalCode7000, TaxTypes.FlatRate).GetAwaiter().GetResult());
            Assert.That(exception.Message, Is.EqualTo("Annual income should be greater than zero"));
        }

        [Test]
        public void UnknownCalculationTypeIsPassed_ExceptionIsThrown()
        {
            var exception = Assert.Throws<CustomException>(() => _taxController.CalculateTax(8000.00M, PostalCodes.PostalCode7000, "Special rate").GetAwaiter().GetResult());
            Assert.That(exception.Message, Is.EqualTo("Unknown Tax Type"));
        }
    }
}
