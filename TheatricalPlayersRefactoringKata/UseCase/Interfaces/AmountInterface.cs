using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.UseCase.Interfaces
{
    public interface AmountInterface
    {
        decimal AmountComedy(int audience, int lines);
        decimal AmountHistory(int audience, int lines);
        decimal AmountTragedy(int audience, int lines);
    }
}
