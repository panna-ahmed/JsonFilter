using System;
using System.Collections.Generic;
using System.Linq;

namespace UsageAnalyzer.Criteria
{
    public class CriteriaDateRange : ICriteria
    {
        private DateTime? _from;
        private DateTime? _to;
        public CriteriaDateRange(DateTime? from, DateTime? to)
        {
            _from = from;
            _to = to;
        }
        public List<TimelineUser> MeetCriteria(List<TimelineUser> users)
        {
            List<TimelineUser> filteredUsers = new List<TimelineUser>();
            foreach (var user in users)
            {
                var count = user.User.MealList.Count(m => DateTime.Parse(m["date"].ToString()) >= _from && DateTime.Parse(m["date"].ToString()) < _to);
                var previousCount = user.User.MealList.Count(m => DateTime.Parse(m["date"].ToString()) < _from);

                filteredUsers.Add(new TimelineUser { User = user.User, WithinRangeCount = count, PreviousCount = previousCount });
            }

            return filteredUsers;
        }
    }
}
