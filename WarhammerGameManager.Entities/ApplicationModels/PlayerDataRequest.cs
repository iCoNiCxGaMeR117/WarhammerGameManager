using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerGameManager.Entities.ApplicationModels
{
    public class PlayerDataRequest
    {
        public long? ExistingPlayerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IList<CheckboxModel> Sub_Factions { get; set; }
    }
}
