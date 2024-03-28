using MediatR;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Events.Queries;
using OrganizingEventsService.Application.Models.Dto.Feedback;

namespace OrganizingEventsService.Application.Events.Handlers;

public class GetFeedbacksEventHandler : IRequestHandler<GetFeedbacksEventQuery, IEnumerable<FeedbackDto>>
{
    private readonly IFeedbackRepository _feedbackRepository;

    public GetFeedbacksEventHandler(IFeedbackRepository feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }


    public async Task<IEnumerable<FeedbackDto>> Handle(GetFeedbacksEventQuery request, CancellationToken cancellationToken)
    {
        var feedbackList = await _feedbackRepository.GetListByQuery(new FeedbackQuery().WithEventId(request.EventId)).ToListAsync();
        List<FeedbackDto> feedbackDtos = feedbackList.Select(feedbackEntity => new FeedbackDto() { Id = feedbackEntity.Id, Rating = feedbackEntity.Rating, Text = feedbackEntity.Text }).ToList();
        return feedbackDtos;
    }
}