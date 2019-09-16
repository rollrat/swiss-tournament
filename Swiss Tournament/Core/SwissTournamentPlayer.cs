namespace Swiss_Tournament.Core
{
    using System;
    using System.Collections.Generic;

    public class SwissTournamentPlayer
    {
        public int Id;
        public int Win;
        public int Lose;
        public int Draw;
        public List<SwissTournamentGame> Games;

        public SwissTournamentPlayer(int id)
        {
            this.Id = id;
            this.Games = new List<SwissTournamentGame>();
        }

        public int Score =>
            (this.Win - this.Draw);
    }
}

