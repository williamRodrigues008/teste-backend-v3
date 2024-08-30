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
        public decimal CalculateAmount(int audience, Play play, decimal amount)
        {
            return play.Type switch
            {
                "comedy" => AmountComedy(audience, amount),
                "history" => AmountHistory(audience, amount),
                "tragedy" => AmountTragedy(audience, amount),
                _ => throw new Exception("unknown type: " + play.Type)
            };
        }

        public decimal AmountComedy(int audience, decimal amount)
        {
            if (audience > 20) amount += 100 + 5 * (audience - 20);
            amount += 3 * audience; 
            return amount;
        }

        public decimal AmountTragedy(int audience, decimal amount)
        {
            if (audience > 30) amount += 10 * (audience - 30);

            return amount;
        }

        public decimal AmountHistory(int audience, decimal amount)
        {
            var tragedy = AmountTragedy(audience, amount);
            var comedy = AmountComedy(audience, amount);
            var history = tragedy + comedy;
            return history;


        }
    }
}
