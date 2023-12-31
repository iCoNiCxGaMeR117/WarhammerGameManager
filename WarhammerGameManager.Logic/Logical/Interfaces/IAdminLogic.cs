﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhammerGameManager.Entities.ApplicationModels;
using WarhammerGameManager.Entities.EntityFramework.WarhammerNarrative.TableModels;
using WarhammerGameManager.Entities.ViewModels;

namespace WarhammerGameManager.Logic.Logical.Interfaces
{
    public interface IAdminLogic
    {
        Task<IList<Faction>> GetAllFactions();
        Task<IList<Player>> GetAllPlayers();
        Task<IList<SubFaction>> GetAllSubFactions();
        Task<PlayerEditorViewModel> GetPlayerEditorViewModel();
        Task PlayerInfoUpdater(PlayerDataRequest request);
    }
}
