using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brackets
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
            GameScores = new List<int>();
        }

        public string Name;

        public List<int> GameScores;
    }
}
