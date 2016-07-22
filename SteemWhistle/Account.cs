using System;

namespace SteemWhistle
{
    public class Account
    {
        public string id { get; set; }
        public string name { get; set; }
        public Owner owner { get; set; }
        public Active active { get; set; }
        public Posting posting { get; set; }
        public string memo_key { get; set; }
        public string json_metadata { get; set; }
        public string proxy { get; set; }
        public DateTime last_owner_update { get; set; }
        public DateTime created { get; set; }
        public bool mined { get; set; }
        public bool owner_challenged { get; set; }
        public bool active_challenged { get; set; }
        public DateTime last_owner_proved { get; set; }
        public DateTime last_active_proved { get; set; }
        public string recovery_account { get; set; }
        public int comment_count { get; set; }
        public int lifetime_vote_count { get; set; }
        public int post_count { get; set; }
        public int voting_power { get; set; }
        public DateTime last_vote_time { get; set; }
        public string balance { get; set; }
        public string sbd_balance { get; set; }
        public string sbd_seconds { get; set; }
        public DateTime sbd_seconds_last_update { get; set; }
        public DateTime sbd_last_interest_payment { get; set; }
        public string vesting_shares { get; set; }
        public string vesting_withdraw_rate { get; set; }
        public DateTime next_vesting_withdrawal { get; set; }
        public object withdrawn { get; set; }
        public object to_withdraw { get; set; }
        public int withdraw_routes { get; set; }
        public int curation_rewards { get; set; }
        public int posting_rewards { get; set; }
        public int[] proxied_vsf_votes { get; set; }
        public int witnesses_voted_for { get; set; }
        public int average_bandwidth { get; set; }
        public string lifetime_bandwidth { get; set; }
        public DateTime last_bandwidth_update { get; set; }
        public int average_market_bandwidth { get; set; }
        public DateTime last_market_bandwidth_update { get; set; }
        public DateTime last_post { get; set; }
        public DateTime last_active { get; set; }
        public string activity_shares { get; set; }
        public DateTime last_activity_payout { get; set; }
        public string vesting_balance { get; set; }
        public object[] transfer_history { get; set; }
        public object[] market_history { get; set; }
        public object[] post_history { get; set; }
        public object[] vote_history { get; set; }
        public object[] other_history { get; set; }
        public object[] witness_votes { get; set; }
        public BlogCategory blog_category { get; set; }
    }
}
