using BuildingBlocks.Application.Abstractions;
using FootballBuddy.Auth.Contracts.IntegrationEvents;
using FootballBuddy.Profiles.Application.Abstractions;
using FootballBuddy.Profiles.Domain.Aggregates;
using FootballBuddy.Profiles.Domain.Repositories;
using FootballBuddy.Shared.Domain.Users;

namespace FootballBuddy.Profiles.Application.IntegrationEvents;

public sealed class UserRegisteredHandler : IIntegrationEventHandler<UserRegisteredV1>
{
    
    private readonly IProfileRepository _profiles; 
    private readonly IProfileUnitOfWork _unitOfWork;

    public UserRegisteredHandler(IProfileRepository profileRepository, IProfileUnitOfWork unitOfWork)
    {
        _profiles = profileRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task HandleAsync(UserRegisteredV1 integrationEvent, CancellationToken cancellationToken = default)
    {
        var userId = new UserId(integrationEvent.UserId);

        if (await _profiles.ExistsAsync(userId, cancellationToken))
        {
            return;
        }
        
        var profile = Profile.Create(userId);
        
        await _profiles.AddAsync(profile, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}