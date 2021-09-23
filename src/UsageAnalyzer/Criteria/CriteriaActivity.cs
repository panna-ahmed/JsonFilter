using System.Collections.Generic;
using System.Linq;

namespace UsageAnalyzer.Criteria
{
    public class CriteriaActivity : ICriteria
    {
        private UserType? _userType;
        public CriteriaActivity(UserType? userType)
        {
            _userType = userType;
        }
        public List<TimelineUser> MeetCriteria(List<TimelineUser> timelineUsers)
        {
            List<TimelineUser> filteredUsers = new List<TimelineUser>();
            foreach (var timelineUser in timelineUsers)
            {
                if (timelineUser.WithinRangeCount > 10 && _userType == UserType.Superactive)
                {
                    filteredUsers.Add(new TimelineUser { User = timelineUser.User });
                }
                else if (timelineUser.WithinRangeCount > 5 && _userType == UserType.Active)
                {
                    filteredUsers.Add(new TimelineUser { User = timelineUser.User });
                }
                else if (timelineUser.PreviousCount > 0 && _userType == UserType.Bored)
                {
                    filteredUsers.Add(new TimelineUser { User = timelineUser.User });
                }
            }

            return filteredUsers;
        }
    }

}
