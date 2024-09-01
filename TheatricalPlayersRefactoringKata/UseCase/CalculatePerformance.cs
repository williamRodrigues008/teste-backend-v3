using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.UseCase.Interfaces;

namespace TheatricalPlayersRefactoringKata.UseCase
{
    public class CalculatePerformance : AmountInterface
    {
        public decimal CalculateAmount(int audience, Play play)
        {
            return play.Type switch
            {
                "comedy" => AmountComedy(audience, play.Lines),
                "history" => AmountHistory(audience, play.Lines),
                "tragedy" => AmountTragedy(audience, play.Lines),
                _ => throw new Exception("unknown type: " + play.Type)
            };
        }

        public decimal AmountComedy(int audience, int lines)
        {
            decimal amount = 0;
            if (audience > 20) amount = 100 + 5 * (audience - 20);
            amount += 3 * audience; 
            return amount + lines / 10;
        }

        public decimal AmountTragedy(int audience, int lines)
        {
            decimal amount = 0;

            if (audience > 30) amount += 10 * (audience - 30);

            return amount + lines / 10;
        }

        public decimal AmountHistory(int audience, int lines)
        {
            return AmountTragedy(audience, lines) + AmountComedy(audience, lines);
        }
    }
}
