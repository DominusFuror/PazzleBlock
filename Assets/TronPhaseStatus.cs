using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
   public enum TronPhaseStatus
    {
        DEACTIVATE_DEVICE = 100,
        TRON_GAME_DEFAULT_PHASE = 1,
        TRON_GAME_PUZZLE_PHASE = 2,
        TRON_GAME_PUZZLE_PHASE_FINISH = 12,
        TRON_GAME_WEAPON_LOAD_PHASE = 3,
        TRON_GAME_WEAPON_UNLOAD_PHASE = 4,
        TRON_GAME_CODE_PHASE = 5 ,
        TRON_GAME_CODE_PHASE_FINISH = 15,
        TRON_GAME_VIRUS_PHASE = 6,
        TRON_GAME_VIRUS_PHASE_FINISH = 16,
        TRON_GAME_VICTORY_PHASE = 7,
        TRON_GAME_DEFEAT_PHASE = 8




    }
}
