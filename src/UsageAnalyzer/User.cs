using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace UsageAnalyzer
{
    public class User
    {
        public string UserId { get; set; }
        
        public List<JObject> MealList { get; set; }
    }
}
