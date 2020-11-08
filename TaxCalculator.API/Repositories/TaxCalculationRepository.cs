using AutoMapper;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

using TaxCalculator.DataContext;
using TaxCalculator.Interfaces;

namespace TaxCalculator.Repositories
{
    public class TaxCalculationRepository : ITaxCalculationRepository
    {
        private readonly TaxCalculatorContext _context;
        private readonly IMapper _mapper;

        public TaxCalculationRepository(TaxCalculatorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Models.TaxBracket>> GetProgressiveTaxBrackets()
        {
            return _mapper.Map<List<Models.TaxBracket>>(await _context.TaxBrackets.AsNoTracking().ToListAsync());
        }

        public async Task<List<Models.TaxCalculationType>> GetTaxCalculationTypes()
        {
            return _mapper.Map<List<Models.TaxCalculationType>>(await _context.TaxCalculationTypes.AsNoTracking().ToListAsync());
        }

        public async Task<Models.TaxFlatValue> GetTaxFlatValue()
        {
            return _mapper.Map<Models.TaxFlatValue>(await _context.TaxFlatValues.AsNoTracking().FirstOrDefaultAsync());
        }

        public async Task<Models.TaxFlatRate> GetTaxFlatRate()
        {
            return _mapper.Map<Models.TaxFlatRate>(await _context.TaxFlatRates.AsNoTracking().FirstOrDefaultAsync());
        }

        public async Task SaveCalculatedTax(Models.TaxCalculationResult taxCalculationResult)
        {
            await _context.AddAsync(_mapper.Map<Entities.TaxCalculationResult>(taxCalculationResult));
            await _context.SaveChangesAsync();
        }
    }
}
