using System;
using System.Collections.Generic;

namespace th.onlineconsign.Model
{
    public partial class ItemGrade
    {
        public Guid Id { get; set; }
        public int GradeId { get; set; }
        public int? SampleId { get; set; }
        public string GradeType { get; set; }
        public string GradeName { get; set; }
        public string Description { get; set; }
    }
}
