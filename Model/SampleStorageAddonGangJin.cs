using System;
using System.Collections.Generic;

namespace th.onlineconsign.Model
{
    public partial class SampleStorageAddonGangJin
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Memo { get; set; }
        public string GjRuPiHao { get; set; }
        public string GjBianMiaoBiaoShi { get; set; }
        public int JianCeShuLiang { get; set; }
        public byte[] GjBianMiaoBiaoShiImage { get; set; }
        public int GjWanQuShuLiang { get; set; }
        public int GjZhongLiangShuLiang { get; set; }
        public int GjTiaoZhiFangshi { get; set; }
        public string GjXkNo { get; set; }
        public string GjXkName { get; set; }
    }
}
