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
    }
}
