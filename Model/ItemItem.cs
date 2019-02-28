using System;
using System.Collections.Generic;

namespace th.onlineconsign.Model
{
    public partial class ItemItem
    {
        public Guid Id { get; set; }
        public int KindId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int? SampleSn { get; set; }
        public string ItemDescription { get; set; }
        public string ItemUc { get; set; }
        public int? CanConsign { get; set; }
        public string Ord { get; set; }
        public string AssessItemId { get; set; }
    }
}
