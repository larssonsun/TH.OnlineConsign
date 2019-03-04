using System;
using System.Collections.Generic;

namespace th.onlineconsign.Model
{
    public partial class ItemSpec
    {
        public Guid Id { get; set; }
        public int SpecId { get; set; }
        public int? SampleId { get; set; }
        public string SpecType { get; set; }
        public string SpecName { get; set; }
        public string SpecDescription { get; set; }
    }
}
