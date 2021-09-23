using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UsageAnalyzer.Criteria;

namespace UsageAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            UserType? typeToFind = null;
            DateTime? fromDate = null;
            DateTime? toDate = null;
            string filePath = null;

            try
            {
                if (args.Count() > 2)
                    toDate = DateTime.Parse(args[2]);

                if (args.Count() > 1)
                    fromDate = DateTime.Parse(args[1]);

                if (args.Count() > 0)
                    typeToFind = (UserType)Enum.Parse(typeof(UserType), args[0]);
            
                // resolve file path
                filePath = Path.Combine(
                    VisualStudioProvider.TryGetSolutionDirectoryInfo()
                    .Parent.Parent.FullName, "data");

                Console.WriteLine($"Data from {filePath}");
            }
            catch
            {
                Console.WriteLine("Error initializing the application.");
                throw;
            }

            var timelineUsers = new List<TimelineUser>();
            var files = Directory.GetFiles(filePath, "*.json");
            foreach (var file in files)
            {
                using (StreamReader streamReader = File.OpenText(file))
                using (JsonTextReader reader = new JsonTextReader(streamReader))
                {
                    var mealList = JToken.ReadFrom(reader).SelectTokens("calendar.daysWithDetails..mealsWithDetails..details")
                                    .Cast<JObject>().ToList();
                    var user = new User { UserId = Path.GetFileNameWithoutExtension(file), MealList = mealList };
                    timelineUsers.Add(new TimelineUser { User = user });
                 }
            }

            ICriteria criteriaTimeline = new CriteriaDateRange(fromDate, toDate);
            ICriteria criteriaActivity = new CriteriaActivity(typeToFind);
            ICriteria timelineActivity = new AndCriteria(criteriaActivity, criteriaTimeline);
            
            foreach(var user in timelineActivity.MeetCriteria(timelineUsers))
                Console.Write($"{user.User.UserId},");
        }
    }
    
}
