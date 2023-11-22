using WarhammerGameManager.Logic.Logical.Interfaces;

namespace WarhammerGameManager.Logic.Logical.Classes
{
    public class GameManagerLogic : IGameManagerLogic
    {
        private readonly Random _rand;

        public GameManagerLogic(Random rand)
        {
            _rand = rand;
        }

        private int[] RollDice(int diceCount)
        {
            var diceResults = new int[diceCount];

            for(var i = 0; i < diceCount; i++)
            {
                diceResults[i] = _rand.Next(1, 7);
            }

            return diceResults;
        }
    }
}
