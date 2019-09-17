/*

   Copyright (C) 2019. rollrat All Rights Reserved.

   Author: Jeong HyunJun

*/

using Swiss_Tournament.DB;
using System;

namespace Swiss_Tournament.Core
{
    public class SwissTournamentGame
    {
        public int Player1;
        public int Player2;
        public int Round;

        public Status Status;

        public int OpponentPlayer(int p)
        {
            if (Player1 == p) return Player2;
            return Player1;
        }

        public int PlayerScore(int p)
        {
            if (Status == Status.Draw)
                return 0;
            if (Player1 == p && Status == Status.Player1Win)
                return 1;
            if (Player2 == p && Status == Status.Player2Win)
                return 1;
            return -1;
        }
    }
}

