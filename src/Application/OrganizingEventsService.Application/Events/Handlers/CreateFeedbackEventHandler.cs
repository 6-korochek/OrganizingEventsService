using MediatR;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Events.Commands;
using OrganizingEventsService.Application.Models.Entities;

namespace OrganizingEventsService.Application.Events.Handlers;

public class CreateFeedbackEventHandler : IRequestHandler<CreateFeedbackEventCommand, Guid>
{
    private readonly IFeedbackRepository _feedbackRepository;

    public CreateFeedbackEventHandler(IFeedbackRepository feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }

    public async Task<Guid> Handle(CreateFeedbackEventCommand request, CancellationToken cancellationToken)
    {
        var feedbackEntity = new Feedback()
        {
            EventParticipantId = request.EventId,
            Id = Guid.NewGuid(),
            Rating = request.EventRequest.Rating,
            Text = request.EventRequest.Text
        };
        await _feedbackRepository.Add(feedbackEntity);
        return feedbackEntity.Id;
    }
}