namespace SteemWhistle
{
    public class Comment
    {
        public string parent_author { get; set; }
        public string parent_permlink { get; set; }
        public string author { get; set; }
        public string permlink { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string json_metadata { get; set; }
    }
}
