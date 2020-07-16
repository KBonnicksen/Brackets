using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brackets
{
    public partial class FrmGetPlayers : Form
    {
        static Random rand = new Random();
        public const int player1 = 0;
        public const int player2 = 1;
        private string FilePath = string.Empty;
        public const string TxtFileFilters = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

        public FrmGetPlayers()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens a dialog box for the user to select a file to import
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = TxtFileFilters
            };

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = openDialog.FileName;
                txtFilePath.Text = FilePath;
            }
        }

        private void BtnStartTournament_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(FilePath))
                MessageBox.Show("Please choose a valid text file.");
            else
            {
                List<Player> Participants = InputTournamentData(FilePath); //Read in players
                PlayGame(Participants);
            }
        }

        /// <summary>
        /// Parses a text file of bowler first names and scores 
        /// and generates a list of all bracket tournament participants
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<Player> InputTournamentData(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            List<Player> Participants = new List<Player>();

            foreach (string line in lines)
            {
                string name = line.Split(' ')[0];
                Player p = new Player(name);
                for (int i = 1; i < 4; i++)
                {
                    Int32.TryParse(line.Split(' ')[i], out int score);
                    p.GameScores.Add(score);
                }
                Participants.Add(p);
            }
            return Participants;
        }


        private void PlayGame(List<Player> ActiveParticipants)
        {
            Bracket bracket = SeedBracketRound1(ActiveParticipants);
            Round curRound = bracket.Rounds[0];
            Game curGame = curRound.Games[0];
            // Calculate winners for bracket 1 and seed bracket 2

            for (int i = 1; i < bracket.Rounds.Count(); i++)
            {
                // Add games for round two and 3
                for (int g = 1; g < curRound.Games.Count; g++)
                {
                    //Get winner of each game
                    if (curGame.GamePlayers[player1].GameScores[i - 1] < curGame.GamePlayers[player2].GameScores[i - 1])
                    {
                        curGame.Winner = curGame.GamePlayers[player2];
                        curGame.Loser = curGame.GamePlayers[player1];
                    }
                    else
                    {
                        curGame.Winner = curGame.GamePlayers[player1];
                        curGame.Loser = curGame.GamePlayers[player2];
                    }

                    // Place winner in next round
                    // If the game being played is game 1 or 2, put winner in game 1 next round
                    if (i == 2)
                        MessageBox.Show("Winner of the $100 prize: " + curGame.Winner.Name + "\nWinner of the $25 prize: " + curGame.Loser.Name, "Results");

                    else if(g < 2)
                    {
                        bracket.Rounds[i].Games[0].GamePlayers.Add(curGame.Winner);
                    }
                    else
                    {
                        bracket.Rounds[i].Games[1].GamePlayers.Add(curGame.Winner);
                    }
                    curGame = curRound.Games[g];
                }
                curRound = bracket.Rounds[i];
            }
        }

        private Bracket SeedBracketRound1(List<Player> ActiveParticipants)
        {
            // Create bracket
            // Randomy seed players into 4 games for round 1
            Bracket bracket = new Bracket();
            for (int i = 0; i < 4; i++)
            {
                // Create game for bracket
                Game g = new Game();
                // Fill players for game
                for (int p = 0; p < 2; p++)
                {
                    int playerIndex = rand.Next(ActiveParticipants.Count);
                    g.GamePlayers.Add(ActiveParticipants[playerIndex]);
                    ActiveParticipants.RemoveAt(playerIndex);
                }

                //Put Game into round 1
                bracket.Rounds[0].Games[i] = g;
            }
            return bracket;
        }


        private void FrmImportPlayers_Load(object sender, EventArgs e)
        {
        }
    }
}
