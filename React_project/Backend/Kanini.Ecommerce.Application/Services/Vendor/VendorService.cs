using AutoMapper;
using Kanini.Ecommerce.Application.DTOs;
using Kanini.Ecommerce.Application.Services.Email;
using Kanini.Ecommerce.Common;
using Kanini.Ecommerce.Data.Repositories.Vendor;
using Kanini.Ecommerce.Domain.Entities;
using Kanini.Ecommerce.Domain.Enums;
using Microsoft.Extensions.Logging;
using DomainVendor = Kanini.Ecommerce.Domain.Entities.Vendor;

namespace Kanini.Ecommerce.Application.Services.Vendor;

public class VendorService : IVendorService
{
    private readonly IVendorRepository _vendorRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<VendorService> _logger;
    private readonly IEmailService _emailService;

    public VendorService(
        IVendorRepository vendorRepository,
        IMapper mapper,
        ILogger<VendorService> logger,
        IEmailService emailService
    )
    {
        _vendorRepository = vendorRepository;
        _mapper = mapper;
        _logger = logger;
        _emailService = emailService;
    }



    public async Task<Result<VendorProfileDto>> GetVendorProfileAsync(int vendorId)
    {
        try
        {
            _logger.LogInformation(
                "Getting vendor profile started for VendorId: {VendorId}",
                vendorId
            );

            var vendorResult = await _vendorRepository.GetVendorByIdAsync(vendorId);
            if (vendorResult.IsFailure)
            {
                _logger.LogError(
                    MagicStrings.LogMessages.VendorProfileUpdateFailed,
                    vendorResult.Error.Description
                );
                return Result.Failure<VendorProfileDto>(vendorResult.Error);
            }

            _logger.LogInformation(
                "Vendor profile retrieved successfully for VendorId: {VendorId}",
                vendorId
            );

            var vendorProfileDto = _mapper.Map<VendorProfileDto>(vendorResult.Value);
            return vendorProfileDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, MagicStrings.ErrorMessages.DatabaseError);
            return Result.Failure<VendorProfileDto>(
                Error.Database(
                    MagicStrings.ErrorCodes.DatabaseError,
                    MagicStrings.ErrorMessages.DatabaseError
                )
            );
        }
    }

    public async Task<Result<List<SubscriptionPlanDto>>> GetSubscriptionPlansAsync()
    {
        try
        {
            _logger.LogInformation("Getting subscription plans started");

            var plansResult = await _vendorRepository.GetSubscriptionPlansAsync();
            if (plansResult.IsFailure)
                return Result.Failure<List<SubscriptionPlanDto>>(plansResult.Error);

            _logger.LogInformation(
                "Subscription plans retrieved successfully. Found {Count} plans",
                plansResult.Value.Count
            );

            var planDtos = _mapper.Map<List<SubscriptionPlanDto>>(plansResult.Value);
            return planDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, MagicStrings.ErrorMessages.DatabaseError);
            return Result.Failure<List<SubscriptionPlanDto>>(
                Error.Database(
                    MagicStrings.ErrorCodes.DatabaseError,
                    MagicStrings.ErrorMessages.DatabaseError
                )
            );
        }
    }

    public async Task<Result> SelectSubscriptionPlanAsync(int vendorId, int planId)
    {
        try
        {
            _logger.LogInformation(MagicStrings.LogMessages.SubscriptionSelectionStarted, vendorId);

            var vendorResult = await _vendorRepository.GetVendorByIdAsync(vendorId);
            if (vendorResult.IsFailure)
            {
                _logger.LogError(
                    MagicStrings.LogMessages.SubscriptionSelectionFailed,
                    MagicStrings.ErrorMessages.VendorNotFound
                );
                return Result.Failure(vendorResult.Error);
            }

            // Create subscription record
            var subscription = new Subscription
            {
                StartDate = DateTime.UtcNow.Date,
                EndDate = DateTime.UtcNow.Date.AddDays(30),
                IsActive = true,
                PlanId = planId,
                VendorId = vendorId,
                CreatedBy = "System",
                CreatedOn = DateTime.UtcNow,
                TenantId = vendorResult.Value.TenantId,
            };

            var subscriptionResult = await _vendorRepository.CreateSubscriptionAsync(subscription);
            if (subscriptionResult.IsFailure)
                return Result.Failure(subscriptionResult.Error);

            // Update vendor's current plan
            var vendor = vendorResult.Value;
            vendor.CurrentPlan = (Domain.Enums.SubscriptionPlan)planId;
            vendor.UpdatedBy = "System";
            vendor.UpdatedOn = DateTime.UtcNow;

            var updateResult = await _vendorRepository.UpdateVendorAsync(vendor);
            if (updateResult.IsFailure)
                return Result.Failure(updateResult.Error);

            _logger.LogInformation(
                MagicStrings.LogMessages.SubscriptionSelectionCompleted,
                vendorId,
                planId
            );
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, MagicStrings.LogMessages.SubscriptionSelectionFailed, ex.Message);
            return Result.Failure(
                Error.Unexpected(
                    MagicStrings.ErrorCodes.UnexpectedError,
                    MagicStrings.ErrorMessages.UnexpectedError
                )
            );
        }
    }

    public async Task<Result> UploadDocumentAsync(int vendorId, string documentPath)
    {
        try
        {
            _logger.LogInformation(MagicStrings.LogMessages.DocumentUploadStarted, vendorId);

            var vendorResult = await _vendorRepository.GetVendorByIdAsync(vendorId);
            if (vendorResult.IsFailure)
            {
                _logger.LogError(
                    MagicStrings.LogMessages.DocumentUploadFailed,
                    MagicStrings.ErrorMessages.VendorNotFound
                );
                return Result.Failure(vendorResult.Error);
            }

            // Update vendor document path
            var vendor = vendorResult.Value;
            vendor.DocumentPath = documentPath;
            vendor.DocumentStatus = DocumentStatus.Pending;
            vendor.UpdatedBy = "System";
            vendor.UpdatedOn = DateTime.UtcNow;

            var updateResult = await _vendorRepository.UpdateVendorAsync(vendor);
            if (updateResult.IsFailure)
                return Result.Failure(updateResult.Error);

            _logger.LogInformation(MagicStrings.LogMessages.DocumentUploadCompleted, vendorId);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, MagicStrings.LogMessages.DocumentUploadFailed, ex.Message);
            return Result.Failure(
                Error.Unexpected(
                    MagicStrings.ErrorCodes.UnexpectedError,
                    MagicStrings.ErrorMessages.UnexpectedError
                )
            );
        }
    }

    public async Task<Result<VendorProfileDto>> GetVendorProfileByUserIdAsync(int userId)
    {
        try
        {
            var vendorResult = await _vendorRepository.GetVendorByUserIdAsync(userId);
            if (vendorResult.IsFailure)
                return Result.Failure<VendorProfileDto>(vendorResult.Error);

            var vendorProfileDto = _mapper.Map<VendorProfileDto>(vendorResult.Value);
            return vendorProfileDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, MagicStrings.ErrorMessages.DatabaseError);
            return Result.Failure<VendorProfileDto>(
                Error.Database(
                    MagicStrings.ErrorCodes.DatabaseError,
                    MagicStrings.ErrorMessages.DatabaseError
                )
            );
        }
    }

    public async Task<Result<VendorProfileDto>> UpdateVendorProfileAsync(int vendorId, VendorProfileUpdateDto request)
    {
        try
        {
            _logger.LogInformation(MagicStrings.LogMessages.VendorProfileUpdateStarted, vendorId);

            var vendorResult = await _vendorRepository.GetVendorByIdAsync(vendorId);
            if (vendorResult.IsFailure)
                return Result.Failure<VendorProfileDto>(vendorResult.Error);

            var vendor = vendorResult.Value;
            _mapper.Map(request, vendor);
            vendor.UpdatedBy = "User";
            vendor.UpdatedOn = DateTime.UtcNow;

            var updateResult = await _vendorRepository.UpdateVendorAsync(vendor);
            if (updateResult.IsFailure)
                return Result.Failure<VendorProfileDto>(updateResult.Error);

            var vendorProfileDto = _mapper.Map<VendorProfileDto>(vendor);
            _logger.LogInformation(MagicStrings.LogMessages.VendorProfileUpdateCompleted, vendorId);
            return vendorProfileDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, MagicStrings.LogMessages.VendorProfileUpdateFailed, ex.Message);
            return Result.Failure<VendorProfileDto>(
                Error.Unexpected(
                    MagicStrings.ErrorCodes.UnexpectedError,
                    MagicStrings.ErrorMessages.UnexpectedError
                )
            );
        }
    }

    public async Task<Result<VendorProfileDto>> CreateVendorProfileAsync(int userId, VendorProfileCreateDto request)
    {
        try
        {
            _logger.LogInformation("Creating vendor profile for UserId: {UserId}", userId);

            // Get user details for email
            var userResult = await _vendorRepository.GetUserByIdAsync(userId);
            if (userResult.IsFailure)
            {
                return Result.Failure<VendorProfileDto>(
                    Error.NotFound("USER_NOT_FOUND", "User not found")
                );
            }

            // Check if vendor profile already exists
            var existingVendorResult = await _vendorRepository.GetVendorByUserIdAsync(userId);
            if (existingVendorResult.IsSuccess)
            {
                _logger.LogWarning("Vendor profile already exists for UserId: {UserId}", userId);
                return Result.Failure<VendorProfileDto>(
                    Error.Conflict(
                        "VENDOR_PROFILE_EXISTS",
                        "Vendor profile already exists for this user"
                    )
                );
            }

            var vendor = new DomainVendor
            {
                BusinessName = request.BusinessName,
                OwnerName = request.OwnerName,
                BusinessLicenseNumber = request.BusinessLicenseNumber,
                BusinessAddress = request.BusinessAddress,
                City = request.City,
                State = request.State,
                PinCode = request.PinCode,
                TaxRegistrationNumber = request.TaxRegistrationNumber,
                DocumentPath = "/temp/pending",
                DocumentStatus = DocumentStatus.Pending,
                CurrentPlan = (Domain.Enums.SubscriptionPlan)request.SubscriptionPlanId,
                Status = VendorStatus.PendingApproval,
                IsActive = false,
                UserId = userId,
                CreatedBy = "User",
                CreatedOn = DateTime.UtcNow,
                TenantId = Guid.NewGuid().ToString()
            };

            var createResult = await _vendorRepository.CreateVendorAsync(vendor);
            if (createResult.IsFailure)
                return Result.Failure<VendorProfileDto>(createResult.Error);

            // Create subscription record for the selected plan
            var subscription = new Subscription
            {
                StartDate = DateTime.UtcNow.Date,
                EndDate = DateTime.UtcNow.Date.AddDays(30),
                IsActive = true,
                PlanId = request.SubscriptionPlanId,
                VendorId = createResult.Value.VendorId,
                CreatedBy = "System",
                CreatedOn = DateTime.UtcNow,
                TenantId = createResult.Value.TenantId,
            };

            var subscriptionResult = await _vendorRepository.CreateSubscriptionAsync(subscription);
            if (subscriptionResult.IsFailure)
            {
                _logger.LogWarning("Failed to create subscription for VendorId: {VendorId}", createResult.Value.VendorId);
            }

            var vendorProfileDto = _mapper.Map<VendorProfileDto>(createResult.Value);
            
            // Send confirmation email to vendor
            try
            {
                await _emailService.SendVendorProfilePendingApprovalAsync(
                    userResult.Value.Email,
                    request.BusinessName,
                    request.OwnerName
                );
            }
            catch (Exception emailEx)
            {
                _logger.LogWarning(emailEx, "Failed to send vendor profile creation email for VendorId: {VendorId}", createResult.Value.VendorId);
            }
            
            _logger.LogInformation("Vendor profile created successfully for UserId: {UserId}, VendorId: {VendorId}", userId, createResult.Value.VendorId);
            return vendorProfileDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create vendor profile for UserId: {UserId}", userId);
            return Result.Failure<VendorProfileDto>(
                Error.Unexpected(
                    MagicStrings.ErrorCodes.UnexpectedError,
                    MagicStrings.ErrorMessages.UnexpectedError
                )
            );
        }
    }


}
