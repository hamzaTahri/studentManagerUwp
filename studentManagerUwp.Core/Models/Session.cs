
using SQLite;
using System;
using System.Collections.Generic;

namespace studentManagerUwp.Core.Models
{
    public class Session
    {
        
        public int Id { get; set; }
        public DateTime date { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int courseId { get; set; }
        public int fieldId { get; set; }
    } 
}
