/*

   Copyright (C) 2019. rollrat All Rights Reserved.

   Author: Jeong HyunJun

*/

using Swiss_Tournament.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Swiss_Tournament.Core
{
    public class SwissTournamentRound
    {
        public Dictionary<int, SwissTournamentPlayer> players = new Dictionary<int, SwissTournamentPlayer>();
        public List<KeyValuePair<int, int>> rank = new List<KeyValuePair<int, int>>();
        public Dictionary<int, List<int>> log = new Dictionary<int, List<int>>();
        public const int bye_win = -1;
        public const int bye_lose = -2;
        private int rank_count = 0;

        private int get_t0_score(int p) =>
            (players[p].Win - players[p].Lose);

        private int get_t1_score(int p) =>
            players[p].Games.Sum(delegate (SwissTournamentGame r)
            {
                int num = r.OpponentPlayer(players[p].Id);
                return ((num != -1) ? ((num != -2) ? players[num].Score : -1) : 0);
            });

        private int get_t2_score(int p) =>
            players[p].Games.Sum(delegate (SwissTournamentGame r)
            {
                int opp1 = r.OpponentPlayer(players[p].Id);
                return ((opp1 != -1) ? ((opp1 != -2) ? players[opp1].Games.Sum(delegate (SwissTournamentGame rr)
                {
                    int num = rr.OpponentPlayer(opp1);
                    return ((num != -1) ? ((num != -2) ? players[num].Score : -1) : 0);
                }) : -1) : 0);
            });

        private int get_t3_score(int p)
        {
            int num = 0;
            foreach (SwissTournamentGame game in players[p].Games)
            {
                if (game.PlayerScore(players[p].Id) == -1)
                {
                    num += game.Round * game.Round;
                }
            }
            return num;
        }

        public Dictionary<int, Tuple<int, int, int, int>> GetTScore()
        {
            Dictionary<int, Tuple<int, int, int, int>> dictionary = new Dictionary<int, Tuple<int, int, int, int>>();
            foreach (KeyValuePair<int, SwissTournamentPlayer> pair in players)
            {
                dictionary.Add(pair.Key, new Tuple<int, int, int, int>(get_t0_score(pair.Key), get_t1_score(pair.Key), get_t2_score(pair.Key), get_t3_score(pair.Key)));
            }
            return dictionary;
        }

        private void insert_log(int pp, int what)
        {
            if (!log.ContainsKey(pp))
            {
                log.Add(pp, new List<int>());
            }
            log[pp].Add(what);
        }

        public void InsertPlayer(int id)
        {
            players.Add(id, new SwissTournamentPlayer(id));
        }

        public void InsertRoundResult(int p1, int p2, Status status, int round)
        {
            SwissTournamentGame item = new SwissTournamentGame
            {
                Player1 = p1,
                Player2 = p2,
                Status = status,
                Round = round
            };
            if (status == Status.Draw)
            {
                SwissTournamentPlayer local1 = players[p1];
                local1.Draw++;
                players[p1].Games.Add(item);
                SwissTournamentPlayer local2 = players[p2];
                local2.Draw++;
                players[p2].Games.Add(item);
            }
            else if ((status == Status.Player1Win) || (status == Status.Player2Win))
            {
                if (status == Status.Player2Win)
                {
                    int num = p2;
                    p2 = p1;
                    p1 = num;
                }
                SwissTournamentPlayer local3 = players[p1];
                local3.Win++;
                players[p1].Games.Add(item);
                SwissTournamentPlayer local4 = players[p2];
                local4.Lose++;
                players[p2].Games.Add(item);
            }
            else if (status == Status.ByeWin)
            {
                item.Player2 = -1;
                item.Status = Status.Player1Win;
                SwissTournamentPlayer local5 = players[p1];
                local5.Win++;
                players[p1].Games.Add(item);
            }
            else if (status == Status.ByeLose)
            {
                item.Player2 = -2;
                item.Status = Status.Player2Win;
                SwissTournamentPlayer local6 = players[p1];
                local6.Lose++;
                players[p1].Games.Add(item);
            }
        }

        public void Sort()
        {
            Dictionary<int, int> source = new Dictionary<int, int>();
            rank_count = 0;
            Dictionary<int, List<int>> dictionary2 = new Dictionary<int, List<int>>();
            foreach (KeyValuePair<int, SwissTournamentPlayer> pair in players)
            {
                if (!dictionary2.ContainsKey(get_t0_score(pair.Key)))
                {
                    dictionary2.Add(get_t0_score(pair.Key), new List<int>());
                }
                dictionary2[get_t0_score(pair.Key)].Add(pair.Key);
            }
            List<KeyValuePair<int, List<int>>> list = dictionary2.ToList<KeyValuePair<int, List<int>>>();
            list.Sort((x, y) => y.Key.CompareTo(x.Key));
            list.ForEach(ps =>
            {
                ps.Value.ForEach(x => insert_log(x, ps.Key));
                if (ps.Value.Count == 1)
                {
                    int num = rank_count;
                    rank_count = num + 1;
                    source.Add(ps.Value[0], num);
                    return;
                }
                foreach (KeyValuePair<int, int> pair2 in SortT1(ps.Value))
                {
                    source.Add(pair2.Key, pair2.Value);
                }
            });
            rank = source.ToList<KeyValuePair<int, int>>();
            rank.Sort((x, y) => x.Value.CompareTo(y.Value));
        }

        private Dictionary<int, int> SortT1(List<int> ll)
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            Dictionary<int, List<int>> source = new Dictionary<int, List<int>>();
            foreach (int num in ll)
            {
                if (!source.ContainsKey(get_t1_score(num)))
                {
                    source.Add(get_t1_score(num), new List<int>());
                }
                source[get_t1_score(num)].Add(num);
            }
            List<KeyValuePair<int, List<int>>> list = source.ToList<KeyValuePair<int, List<int>>>();
            list.Sort((x, y) => y.Key.CompareTo(x.Key));
            list.ForEach(ps =>
            {
                ps.Value.ForEach(delegate (int x)
                {
                    insert_log(x, ps.Key);
                });
                if (ps.Value.Count == 1)
                {
                    int num2 = rank_count;
                    rank_count = num2 + 1;
                    dictionary.Add(ps.Value[0], num2);
                    return;
                }
                foreach (KeyValuePair<int, int> pair in SortT2(ps.Value))
                {
                    dictionary.Add(pair.Key, pair.Value);
                }
            });
            return dictionary;
        }

        private Dictionary<int, int> SortT2(List<int> ll)
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            Dictionary<int, List<int>> source = new Dictionary<int, List<int>>();
            foreach (int num in ll)
            {
                if (!source.ContainsKey(get_t2_score(num)))
                {
                    source.Add(get_t2_score(num), new List<int>());
                }
                source[get_t2_score(num)].Add(num);
            }
            List<KeyValuePair<int, List<int>>> list = source.ToList<KeyValuePair<int, List<int>>>();
            list.Sort((x, y) => y.Key.CompareTo(x.Key));
            list.ForEach(ps =>
            {
                ps.Value.ForEach(delegate (int x)
                {
                    insert_log(x, ps.Key);
                });
                if (ps.Value.Count == 1)
                {
                    int num2 = rank_count;
                    rank_count = num2 + 1;
                    dictionary.Add(ps.Value[0], num2);
                    return;
                }
                foreach (KeyValuePair<int, int> pair in SortT3(ps.Value))
                {
                    dictionary.Add(pair.Key, pair.Value);
                }
            });
            return dictionary;
        }

        private Dictionary<int, int> SortT3(List<int> ll)
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            Dictionary<int, List<int>> source = new Dictionary<int, List<int>>();
            foreach (int num in ll)
            {
                if (!source.ContainsKey(get_t3_score(num)))
                {
                    source.Add(get_t3_score(num), new List<int>());
                }
                source[get_t3_score(num)].Add(num);
            }
            List<KeyValuePair<int, List<int>>> list = source.ToList<KeyValuePair<int, List<int>>>();
            list.Sort((x, y) => y.Key.CompareTo(x.Key));
            list.ForEach(ps =>
            {
                ps.Value.ForEach(delegate (int x)
                {
                    insert_log(x, ps.Key);
                });
                foreach (int num2 in ps.Value)
                {
                    int num3 = rank_count;
                    rank_count = num3 + 1;
                    dictionary.Add(num2, num3);
                }
            });
            return dictionary;
        }
    }
}

