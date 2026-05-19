using AutoMapper;
using DialDesk.Server.DTOs;
using DialDesk.Server.DTOs.Brand;
using DialDesk.Server.DTOs.Model;
using DialDesk.Server.DTOs.ModelPriceRecord;
using DialDesk.Server.DTOs.Sale;
using DialDesk.Server.DTOs.Watch;
using DialDesk.Server.Models;
using DialDesk.Server.Pagination;

namespace DialDesk.Server.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Brand
            CreateMap<Brand, BrandOutDto>();
            CreateMap<BrandCreateDto, Brand>();
            CreateMap<BrandUpdateDto, Brand>();

            // Model
            CreateMap<Models.Model, ModelOutDto>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : null));
            CreateMap<ModelCreateDto, Models.Model>();
            CreateMap<ModelUpdateDto, Models.Model>();

            // Watch
            CreateMap<Watch, WatchOutDto>()
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model != null ? src.Model.ModelName : null))
                .ForMember(dest => dest.BrandName,
                    opt => opt.MapFrom(src =>
                        src.Model != null && src.Model.Brand != null
                            ? src.Model.Brand.Name
                            : null))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Model.Category))
                .ForMember(dest => dest.ModelNo, opt => opt.MapFrom(src => src.Model.ModelNo));
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
            CreateMap<ModelPriceHistory, ModelHistoryCreateDto>()
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model != null ? src.Model.ModelName : null));
            CreateMap<ModelHistoryCreateDto, ModelPriceHistory>();
            CreateMap<ModelHistoryUpdateDto, ModelPriceHistory>();

            // Pagination result
            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
        }
    }
}
