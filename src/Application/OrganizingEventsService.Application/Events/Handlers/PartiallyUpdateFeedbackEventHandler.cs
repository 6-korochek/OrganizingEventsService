using MediatR;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Events.Commands;
using OrganizingEventsService.Application.Models.Dto.Feedback;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Events.Handlers;

public class PartiallyUpdateFeedbackEventHandler : IRequestHandler<PartiallyUpdateFeedbackEventCommand, FeedbackDto>
{
    private readonly IFeedbackRepository _feedbackRepository;

    public PartiallyUpdateFeedbackEventHandler(IFeedbackRepository feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }


    public async Task<FeedbackDto> Handle(PartiallyUpdateFeedbackEventCommand request, CancellationToken cancellationToken)
    {
        var feedbackEntity = await _feedbackRepository.GetById(request.FeedbackId);
        feedbackEntity.Rating = request.EventRequest.Rating ?? feedbackEntity.Rating;
        feedbackEntity.Text = request.EventRequest.Text ?? feedbackEntity.Text;
        await _feedbackRepository.Update(feedbackEntity);
        return new FeedbackDto()
        {
            Id = feedbackEntity.Id,
            Rating = feedbackEntity.Rating,
            Text = feedbackEntity.Text
        };
    }
}