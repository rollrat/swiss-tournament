namespace Swiss_Tournament.Core
{
    using Swiss_Tournament.DB;
    using System;

    public class SwissTournamentGame
    {
        public int Player1;
        public int Player2;
        public int Round;
        public Swiss_Tournament.DB.Status Status;

        public int OpponentPlayer(int p) => 
            ((this.Player1 != p) ? this.Player1 : this.Player2);

        public int PlayerScore(int p) => 
            ((this.Status != Swiss_Tournament.DB.Status.Draw) ? (((this.Player1 != p) || (this.Status != Swiss_Tournament.DB.Status.Player1Win)) ? (((this.Player2 != p) || (this.Status != Swiss_Tournament.DB.Status.Player2Win)) ? -1 : 1) : 1) : 0);
    }
}

