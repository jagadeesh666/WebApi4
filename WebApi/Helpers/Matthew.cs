using System;
using System.Collections.Generic;

namespace WebApi.Helpers
{
    /// Deserialise from JSON
    [Serializable]
    public class Matthew
    {
        public string Name { get; set; }
        public string Profile_Name { get; set; }
        public string Profile_Url { get; set; }
        public string Gravatar_Url { get; set; }
        public string Gravatar_Hash { get; set; }
        public List<Badge> Badges { get; set; }
    }


    public class Badge
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon_Url { get; set; }
        public string Earned_Date { get; set; }

        public List<Course> Courses { get; set; }
    }

    public class Course
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public int Badge_Count { get; set; }
    }
}