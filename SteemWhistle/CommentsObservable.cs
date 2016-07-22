using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace SteemWhistle
{
    public static class CommentsObservable
    {
        public static IObservable<Comment> Create(IObservable<Comment> postsAndCommentsObservable)
        {
            return from c in postsAndCommentsObservable
                   where string.IsNullOrEmpty(c.title)
                   select c;
        }
    }
}

