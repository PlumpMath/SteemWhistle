using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteemWhistle
{
    public static class PostsObservable
    {
        public static IObservable<Comment> Create(IObservable<Comment> postsAndCommentsObservable)
        {
            return from c in postsAndCommentsObservable
                   where !string.IsNullOrEmpty(c.title)
                   select c;
        }
    }
}
