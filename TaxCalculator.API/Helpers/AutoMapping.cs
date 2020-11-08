using AutoMapper;

namespace TaxCalculator.MVC.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Entities.TaxCalculationType, Models.TaxCalculationType>();
            CreateMap<Entities.TaxBracket, Models.TaxBracket>();
            CreateMap<Entities.TaxFlatValue, Models.TaxFlatValue>();
            CreateMap<Entities.TaxFlatRate, Models.TaxFlatRate>();
            CreateMap<Entities.TaxCalculationResult, Models.TaxCalculationResult>().ReverseMap();
        }
    }
}
