using System;
using System.Collections.Generic;

namespace th.onlineconsign.Model
{
    public partial class ItemKind
    {
        public Guid Id { get; set; }
        public int KindId { get; set; }
        public string KindName { get; set; }
        public int ItemSn { get; set; }
        public string KindDescription { get; set; }
        public int? CanConsign { get; set; }
        public int? IsCtrl { get; set; }
    }
}
