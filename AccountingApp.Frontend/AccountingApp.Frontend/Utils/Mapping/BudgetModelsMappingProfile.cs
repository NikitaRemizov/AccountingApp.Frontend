using AccountingApp.Frontend.Models;
using AccountingApp.Shared.Models;
using AccountingApp.Shared.Converters;
using AutoMapper;
using System;

namespace AccountingApp.Frontend.Utils.Mapping
{
    public class BudgetModelsMappingProfile : Profile
    {
        public BudgetModelsMappingProfile()
        {
            CreateMap<BudgetTypeViewModel, BudgetType>()
                .ReverseMap();

            CreateMap<BudgetTypeViewModel, BudgetChange>()
                .ForMember(dest => dest.BudgetTypeId, opt => opt.MapFrom(src => src.Id))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<BudgetChangeViewModel, BudgetChange>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => DollarsToCents.Convert(src.AmountInDollars)))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date ?? DateTime.Today))
                .AfterMap((dest, src, ctx) => ctx.Mapper.Map<BudgetChange>(src));

            CreateMap<BudgetChange, BudgetChangeViewModel>()
                .ForMember(dest => dest.AmountInDollars, opt => opt.MapFrom(src => DollarsToCents.ConvertBack(src.Amount)))
                .ForMember(dest => dest.BudgetType, opt => opt.MapFrom(
                    src => new BudgetTypeViewModel
                    {
                        Id = src.BudgetTypeId,
                        Name = src.BudgetTypeName,
                    })
                );
        }
    }
}
