using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brackets
{
    class Bracket
    {
        public const int NUM_GAMES_ROUND_1 = 4;
        public const int NUM_GAMES_ROUND_2 = NUM_GAMES_ROUND_1 / 2;
        public const int NUM_GAMES_ROUND_3 = NUM_GAMES_ROUND_2 / 2;
        public List<Round> Rounds { get; set; }

        public Bracket()
        {
            Rounds = new List<Round>();

            Round Round1 = new Round(NUM_GAMES_ROUND_1); // 4 games
            Round Round2 = new Round(NUM_GAMES_ROUND_2);
            Round Round3 = new Round(NUM_GAMES_ROUND_3);
            Rounds.Add(Round1);
            Rounds.Add(Round2);
            Rounds.Add(Round3);
        }
    }

    class Round
    {
        public List<Game> Games { get; set; }

        // Creates a new round with no games 
        public Round()
        {
            Games = new List<Game>();
        }
        // Creates a new round with the the number of Games specified by the user added on
        public Round(int numGames) : this()
        {
            for(int i = 0; i < numGames; i++)
            {
                Game g = new Game();
                Games.Add(g);
            }
        }

    }

    class Game
    {
        public Game()
        {
            GamePlayers = new List<Player>();
        }
        public Game(Player player1, Player player2) : this()
        {
            GamePlayers.Add(player1);
            GamePlayers.Add(player2);
        }

        public List<Player> GamePlayers { get; set; }
        public Player Winner { get; set; }
        public Player Loser { get; set; }
    }
}
