using System;

namespace SteemWhistle
{
    public class Block
    {
        public string previous { get; set; }
        public DateTime timestamp { get; set; }
        public string witness { get; set; }
        public string transaction_merkle_root { get; set; }
        public object[] extensions { get; set; }
        public string witness_signature { get; set; }
        public Transaction[] transactions { get; set; }
    }
}