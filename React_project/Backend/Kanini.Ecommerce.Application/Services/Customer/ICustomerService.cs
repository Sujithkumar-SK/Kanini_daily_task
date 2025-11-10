using Kanini.Ecommerce.Application.DTOs;
using Kanini.Ecommerce.Common;

namespace Kanini.Ecommerce.Application.Services.Customer;

public interface ICustomerService
{
    Task<Result<CustomerProfileDto>> GetCustomerProfileAsync(int customerId);
    Task<Result<List<CustomerProfileDto>>> GetAllCustomersAsync();
    Task<Result<CustomerProfileDto>> UpdateCustomerProfileAsync(
        int customerId,
        CustomerProfileUpdateDto request
    );
    Task<Result> DeleteCustomerAsync(int customerId);
    Task<Result<int>> GetCustomerIdByUserIdAsync(int userId);
}
