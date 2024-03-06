using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Infrastructure.Persistence.Models;

namespace OrganizingEventsService.Infrastructure.Persistence.Mapping;

public class FeedbackMapper
{
    public static Feedback ToEntity(FeedbackModel feedbackModel)
    {
        Feedback entity = new Feedback
        {
            Id = feedbackModel.Id,
            Rating = feedbackModel.Rating,
            Text = feedbackModel.Text,
            EventParticipantId = feedbackModel.EventParticipantId,
            EventParticipantIdNavigation = EventParticipantMapper.ToEntity(feedbackModel.EventParticipantIdNavigation)
        };

        entity.EventParticipantIdNavigation.AccountIdNavigation =
            AccountMapper.ToEntity(feedbackModel.EventParticipantIdNavigation.AccountIdNavigation);
        
        return entity;
    }

    public static FeedbackModel ToModel(Feedback feedback)
    {
        FeedbackModel model = new FeedbackModel
        {
            Id = feedback.Id,
            Rating = feedback.Rating,
            Text = feedback.Text,
            EventParticipantId = feedback.EventParticipantId,
            EventParticipantIdNavigation = EventParticipantMapper.ToModel(feedback.EventParticipantIdNavigation)
        };

        model.EventParticipantIdNavigation.AccountIdNavigation =
            AccountMapper.ToModel(feedback.EventParticipantIdNavigation.AccountIdNavigation);

        return model;
    }
}