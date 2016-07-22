namespace SteemWhistle
{
    public class Posting
    {
        public int weight_threshold { get; set; }
        public object[] account_auths { get; set; }
        public object[][] key_auths { get; set; }
    }
}
