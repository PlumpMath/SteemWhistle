using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace SteemWhistle
{
    public static class PostsAndCommentsObservable
    {
        public static IObservable<Comment> Create(IObservable<Transaction> transactionsObservable)
        {
            return (from t in transactionsObservable
                    from o in t.operations
                    where o[0].ToString() == "comment"
                    select JsonConvert.DeserializeObject<Comment>(o[1].ToString()));
        }
    }
}
