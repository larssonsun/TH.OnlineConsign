using System;
using System.Collections.Generic;

namespace th.onlineconsign.Model
{
    public partial class DpProductionUnitType
    {
        public string Id { get; set; }
        public int ItemId { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
    }
}
