using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.UseCase;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var calculate = new CalculatePerformance();

            var play = plays[perf.PlayId];
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            decimal thisAmount = lines / 10;
            
            thisAmount = calculate.CalculateAmount(perf.Audience, play, thisAmount);
            volumeCredits = CreateVolumeCredit(volumeCredits, perf, play);

            // print line for this order
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDouble(thisAmount), perf.Audience);
            totalAmount += thisAmount;
        }
        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
        result += string.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    private int CreateVolumeCredit(int volumeCredits, Performance perf, Play play)
    {
        // add volume credits
        volumeCredits += Math.Max(perf.Audience - 30, 0);
        // add extra credit for every ten comedy attendees
        if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
        return volumeCredits;
    }
}
