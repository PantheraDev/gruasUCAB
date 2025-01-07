using MediatR;
using ProviderMs.Application.Queries;
using ProviderMs.Common.dto.Response;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Database;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Application.Handlers.Queries
{
    public class GetProviderDepartmentQueryHandler : IRequestHandler<GetProviderDepartmentQuery, GetProviderDepartment>
    {
        public IProviderDepartmentRepository _providerDepartmentRepository;
        private readonly IApplicationDbContext _dbContext;

        public GetProviderDepartmentQueryHandler(IProviderDepartmentRepository providerDepartmentRepository, IApplicationDbContext dbContext)
        {
            _providerDepartmentRepository = providerDepartmentRepository;
            _dbContext = dbContext;
        }

        public async Task<GetProviderDepartment> Handle(GetProviderDepartmentQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty) throw new NullAttributeException("ProviderDepartment id is required");
            var providerDepartmentId = ProviderDepartmentId.Create(request.Id);
            var providerDepartment = await _providerDepartmentRepository.GetByIdAsync(providerDepartmentId!);

            return new GetProviderDepartment(
                providerDepartment.Id.Value,
                providerDepartment.ProviderId.Value,
                providerDepartment.DepartmentId.Value
            );
        }
    }
}