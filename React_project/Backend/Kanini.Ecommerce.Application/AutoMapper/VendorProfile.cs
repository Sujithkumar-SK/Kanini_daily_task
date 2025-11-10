using AutoMapper;
using Kanini.Ecommerce.Application.DTOs;
using Kanini.Ecommerce.Domain.Entities;
using DomainVendor = Kanini.Ecommerce.Domain.Entities.Vendor;

namespace Kanini.Ecommerce.Application.AutoMapper;

public class VendorProfile : Profile
{
    public VendorProfile()
    {
        CreateMap<DomainVendor, VendorProfileDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(
                dest => dest.CurrentPlan,
                opt => opt.MapFrom(src => src.CurrentPlan.ToString())
            );

        CreateMap<SubscriptionPlan, SubscriptionPlanDto>();
        
        CreateMap<VendorProfileUpdateDto, DomainVendor>()
            .ForMember(dest => dest.VendorId, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentPath, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentStatus, opt => opt.Ignore())
            .ForMember(dest => dest.VerifiedOn, opt => opt.Ignore())
            .ForMember(dest => dest.VerifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CurrentPlan, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.Subscriptions, opt => opt.Ignore())

            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.TenantId, opt => opt.Ignore());
    }
}