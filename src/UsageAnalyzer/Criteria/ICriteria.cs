using System.Collections.Generic;

namespace UsageAnalyzer.Criteria
{
    public interface ICriteria
    {
        List<TimelineUser> MeetCriteria(List<TimelineUser> users);
    }
}
