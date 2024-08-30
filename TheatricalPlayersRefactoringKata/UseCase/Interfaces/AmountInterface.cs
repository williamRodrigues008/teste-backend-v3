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
        decimal AmountComedy(int audience, decimal amount);
        decimal AmountHistory(int audience, decimal amount);
        decimal AmountTragedy(int audience, decimal amount);
    }
}
