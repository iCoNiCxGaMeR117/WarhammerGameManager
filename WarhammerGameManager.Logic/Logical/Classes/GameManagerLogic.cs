using WarhammerGameManager.Entities.ApplicationModels;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;
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

        public DiceEvent QuickRollDice(RollDiceRequest request)
        {
            int[] hitRoll = RollDice(request.DiceCount);
            var hitCount = hitRoll.Count(x => x >= request.HitThreshold);

            int[] woundRoll = RollDice(hitCount);
            var woundCount = woundRoll.Count(x => x >= request.WoundThreshold);

            int[] saveRoll = RollDice(woundCount);
            var saveCount = saveRoll.Count(x => x >= request.SaveThreshold);

            if (request.FeelNoPainFlag)
            {
                int[] fnpRoll = RollDice(woundCount - saveCount);
                var fnpCount = fnpRoll.Count(x => x >= request.FeelNoPainThreshold);
            }
        }

        public DiceEvent GameRoll(RollDiceRequest request, long GameId)
        {
            throw new NotImplementedException();
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
