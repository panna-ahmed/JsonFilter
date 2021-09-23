using System.Collections.Generic;

namespace UsageAnalyzer.Criteria
{
    public class AndCriteria: ICriteria
    {
        private ICriteria _criteria;
        private ICriteria _otherCriteria;

        public AndCriteria(ICriteria criteria, ICriteria otherCriteria)
        {
            _criteria = criteria;
            _otherCriteria = otherCriteria;
        }

        public List<TimelineUser> MeetCriteria(List<TimelineUser> users)
        {
            List<TimelineUser> firstCriteriaPersons = _otherCriteria.MeetCriteria(users);
            return _criteria.MeetCriteria(firstCriteriaPersons);
        }
    }
}
