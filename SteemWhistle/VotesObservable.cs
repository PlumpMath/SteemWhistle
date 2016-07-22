using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace SteemWhistle
{
    public static class VotesObservable
    {
        public static IObservable<Vote> Create(IObservable<Transaction> transactionsObservable)
        {
            return from t in transactionsObservable
                   from o in t.operations
                   where o[0].ToString() == "vote"
                   select JsonConvert.DeserializeObject<Vote>(o[1].ToString());
        }
    }
}
