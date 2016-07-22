using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace SteemWhistle
{
    public static class TransactionsObservable
    {
        public static IObservable<Transaction> Create(IObservable<Block> blockObservable)
        {
            return  from b in blockObservable
                    from t in b.transactions
                    select t;
        }
    }
}
