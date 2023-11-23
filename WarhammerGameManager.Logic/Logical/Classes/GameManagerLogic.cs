using WarhammerGameManager.Entities.ApplicationModels;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.Contexts;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;
using WarhammerGameManager.Logic.Logical.Interfaces;

namespace WarhammerGameManager.Logic.Logical.Classes
{
    public class GameManagerLogic : IGameManagerLogic
    {
        private readonly Random _rand;
        private readonly WarhammerNarrative_Context _context;

        public GameManagerLogic(Random rand, WarhammerNarrative_Context context)
        {
            _rand = rand;
            _context = context;
        }

        public DiceEvent QuickRollDice(RollDiceRequest request)
        {
            var diceEvent = new DiceEvent();
            var rolls = new List<DiceRoll>();

            var rollTypes = _context.RollTypes.ToList();

            int[] hitRoll = RollDice(request.DiceCount);
            rolls.AddRange(hitRoll.ConvertToDiceRoll(rollTypes.Single(x => x.Name.ToUpper().Equals("HIT")), request.HitThreshold));
            var hitCount = hitRoll.Count(x => x >= request.HitThreshold);

            int[] woundRoll = RollDice(hitCount);
            rolls.AddRange(woundRoll.ConvertToDiceRoll(rollTypes.Single(x => x.Name.ToUpper().Equals("WOUND")), request.WoundThreshold));
            var woundCount = woundRoll.Count(x => x >= request.WoundThreshold);

            int[] saveRoll = RollDice(woundCount);
            rolls.AddRange(saveRoll.ConvertToDiceRoll(rollTypes.Single(x => x.Name.ToUpper().Equals("SAVE")), request.SaveThreshold));
            var saveCount = saveRoll.Count(x => x >= request.SaveThreshold);

            if (request.FeelNoPainFlag)
            {
                int[] fnpRoll = RollDice(woundCount - saveCount);
                rolls.AddRange(fnpRoll.ConvertToDiceRoll(rollTypes.Single(x => x.Name.ToUpper().Equals("FEEL NO PAIN")), request.FeelNoPainThreshold ?? 6));
            }

            diceEvent.Rolls = rolls;

            return diceEvent;
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
