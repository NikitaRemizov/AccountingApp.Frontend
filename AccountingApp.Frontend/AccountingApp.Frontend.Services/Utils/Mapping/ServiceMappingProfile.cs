using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Frontend.Services.Models;
using AccountingApp.Shared.Converters;
using AutoMapper;
using System;
using AutoMapper.Extensions.EnumMapping;

namespace AccountingApp.Frontend.Utils.Mapping
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<BudgetType, Shared.Models.BudgetType>()
                .ReverseMap();

            CreateMap<BudgetType, Shared.Models.BudgetChange>()
                .ForMember(dest => dest.BudgetTypeId, opt => opt.MapFrom(src => src.Id))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<BudgetChange, Shared.Models.BudgetChange>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => DollarsToCents.Convert(src.AmountInDollars)))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date ?? DateTime.Today))
                .AfterMap((dest, src, ctx) => ctx.Mapper.Map<AccountingApp.Shared.Models.BudgetChange>(src));

            CreateMap<Shared.Models.BudgetChange, BudgetChange>()
                .ForMember(dest => dest.AmountInDollars, opt => opt.MapFrom(src => DollarsToCents.ConvertBack(src.Amount)))
                .ForMember(dest => dest.BudgetType, opt => opt.MapFrom(
                    src => new BudgetType
                    {
                        Id = src.BudgetTypeId,
                        Name = src.BudgetTypeName,
                    })
                );

            CreateMap<User, Shared.Models.User>()
                .ReverseMap();

            CreateMap<AccountingApiResult, ServiceResult>()
                .ConvertUsingEnumMapping(opt =>
                {
                    opt.MapValue(AccountingApiResult.ServerUnreachable, ServiceResult.Error);
                    opt.MapByName();
                });
        }
    }
}
