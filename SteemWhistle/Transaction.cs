using System;

namespace SteemWhistle
{
    public class Transaction
    {
        public Int64 ref_block_num { get; set; }
        public Int64 ref_block_prefix { get; set; }
        public DateTime expiration { get; set; }
        public object[][] operations { get; set; }
        public object[] extensions { get; set; }
        public string[] signatures { get; set; }
    }
}