/*

   Copyright (C) 2019. rollrat All Rights Reserved.

   Author: Jeong HyunJun

*/

using System;
using System.Collections.Generic;

namespace Swiss_Tournament.Core
{
    /// <summary>
    /// Participant Informations
    /// </summary>
    public class SwissTournamentPlayer
    {
        public int Id;
        public int Win;
        public int Lose;
        public int Draw;
        public List<SwissTournamentGame> Games;

        public SwissTournamentPlayer(int id)
        { Id = id; Games = new List<SwissTournamentGame>(); }

        /// <summary>
        /// Get swiss-system tournament player score
        /// </summary>
        public int Score => Win - Draw;
    }
}

