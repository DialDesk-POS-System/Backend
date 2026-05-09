using AutoMapper;
using DialDesk.Server.DTOs;
using DialDesk.Server.DTOs.Brand;
using DialDesk.Server.DTOs.Model;
using DialDesk.Server.DTOs.Sale;
using DialDesk.Server.DTOs.Watch;
using DialDesk.Server.Models;

namespace DialDesk.Server.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Brand
            CreateMap<Brand, BrandOutDto>();
            CreateMap<BrandInDto, Brand>();

            // Model
            CreateMap<Models.Model, ModelOutDto>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : null));
            CreateMap<ModelInDto, Models.Model>();

            // Watch
            CreateMap<Watch, WatchOutDto>()
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model != null ? src.Model.ModelName : null))
                .ForMember(dest => dest.BrandName,
                    opt => opt.MapFrom(src =>
                        src.Model != null && src.Model.Brand != null
                            ? src.Model.Brand.Name
                            : null));
            CreateMap<WatchCreateDto, Watch>();
            CreateMap<WatchUpdateDto, Watch>();

            // Sale
            CreateMap<Sale, SaleOutDto>();
            CreateMap<SaleCreateDto, Sale>();
            CreateMap<SaleUpdateDto, Sale>();

            // SaleItem
            CreateMap<SaleItem, SaleItemOutDto>();
            CreateMap<SaleItemCreateDto, SaleItem>();

            // ModelPriceHistory
            CreateMap<ModelPriceHistory, ModelHistoryDto>()
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model != null ? src.Model.ModelName : null));
        }
    }
}
