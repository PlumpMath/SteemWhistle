using SteemWhistle;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using WebSocketSharp;

namespace SteemVoteBot
{
    class Program
    {
        const string ACCOUNT_NAME = "auxon";
        const string ACCOUNT_NAME_2 = "auxonoxua";
        
        static void Main(string[] args)
        {
            var wallet = new Wallet("ws://localhost:8093");
            wallet.Connect().Wait();

            var authorsToVoteUp = new[] { "jl777", "roelandp", "stellabella", "cryptoctopus", "dan", "bitcube", "xeroc", "auxon" };

            CancellationToken cancellationToken = new CancellationToken(false);

            var globalPropertiesObservable = GlobalPropertiesObservable.Create("ws://localhost:8090").Publish();
            var blockObservable = BlockObservable.Create("ws://localhost:8090", globalPropertiesObservable).Publish();
            var transactionsObservable = TransactionsObservable.Create(blockObservable).Publish();
            var postsAndCommentsObservable = PostsAndCommentsObservable.Create(transactionsObservable).Publish();
            var commentsObservable = CommentsObservable.Create(postsAndCommentsObservable).Publish();
            var postsObservable = PostsObservable.Create(postsAndCommentsObservable).Publish();
            var votesObservable = VotesObservable.Create(transactionsObservable).Publish();

            globalPropertiesObservable.Connect();
            blockObservable.Connect();
            transactionsObservable.Connect();
            postsAndCommentsObservable.Connect();
            commentsObservable.Connect();
            postsObservable.Connect();
            votesObservable.Connect();

            globalPropertiesObservable.Subscribe((globalProperties) =>
            {
                Console.WriteLine("head_block_number = " + globalProperties.head_block_number);
            }, (ex) => Console.WriteLine(ex.Message));

            blockObservable.Subscribe(block =>
            {
                Console.WriteLine("Block arrived.  # of transactions = " + block.transactions?.Count());
            }, (ex) => Console.WriteLine(ex.Message));

            transactionsObservable.Subscribe(t => Console.WriteLine(t.operations[0][0].ToString()));

            votesObservable.Subscribe(vote =>
            {
                Console.WriteLine("=> Vote:");
                Console.WriteLine("   author: {0}", vote.author);
                Console.WriteLine("   voter: {0}", vote.voter);
                Console.WriteLine("   weight: {0}", vote.weight);
                Console.WriteLine("   permlink: {0}", vote.permlink);
            });

            commentsObservable.Subscribe(comment =>
            {
                Console.WriteLine("=> Comment:");
                Console.WriteLine("   title: {0}", comment.title);
                Console.WriteLine("   parent author: {0}", comment.parent_author);
                Console.WriteLine("   author: {0}", comment.author);
                Console.WriteLine("   body: {0}", comment.body);
                Console.WriteLine("   permlink: {0}", comment.permlink);
                Console.WriteLine("   parent permlink: {0}", comment.parent_permlink);
                Console.WriteLine("   json metadata: {0}", comment.json_metadata);
            });

            postsObservable.Subscribe(post =>
            {
                Console.WriteLine("===> New Post:");
                Console.WriteLine("   title: {0}", post.title);
                Console.WriteLine("   parent author: {0}", post.parent_author);
                Console.WriteLine("   author: {0}", post.author);
                Console.WriteLine("   body: {0}", post.body);
                Console.WriteLine("   permlink: {0}", post.permlink);
                Console.WriteLine("   parent permlink: {0}", post.parent_permlink);
                Console.WriteLine("   json metadata: {0}", post.json_metadata);
            });           

            postsObservable.Subscribe(c => 
            { 
                // Upvote all introduceyourself posts
                if (c.json_metadata.Contains("introduceyourself"))
                {
                    Upvote(c, wallet);
                }
                if (c.json_metadata.Contains("science"))
                {
                    Upvote(c, wallet);
                }
                if (c.json_metadata.Contains("bitcoin"))
                {
                    Upvote(c, wallet);
                }
                if (c.json_metadata.Contains("programming"))
                {
                    Upvote(c, wallet);
                }
            });

            postsAndCommentsObservable.Subscribe(c =>
            {
                if (authorsToVoteUp.Contains(c.author))
                {
                    Upvote(c, wallet);
                }
            });

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            wallet?.Dispose();
        }

        private static async void Upvote(Comment c, Wallet wallet)
        {
            await wallet.Upvote(ACCOUNT_NAME, c);
            await wallet.Upvote(ACCOUNT_NAME_2, c);
        }
    }
}
