using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.UseCase;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays, string fileType, string path)
    {
        decimal totalAmount = 0;
        decimal volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var calculate = new CalculatePerformance();

            var play = plays[perf.PlayId];
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            decimal thisAmount = 0;
            
            thisAmount = calculate.CalculateAmount(perf.Audience, play);
            volumeCredits = CreateVolumeCredit(volumeCredits, perf.Audience, play.Type);

            // print line for this order
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDouble(thisAmount), perf.Audience);
            totalAmount += thisAmount;
        }
        if ( fileType == "xml" )
        {
           GenerateStatementXML(path, invoice, plays);
        }
        else 
        {
            result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
            result += string.Format("You earned {0} credits\n", volumeCredits);
        }
        return result;
    }

    public string GenerateStatementXML(string path, Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal amount = 0;
        decimal totalAmount = 0;
        var newXMLStatement = new XDocument(new XElement("Statement"));
        newXMLStatement.Element("Statement").Add(new XElement("Customer", invoice.Customer));
        decimal credits = 0;
        var calculate = new CalculatePerformance();

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            amount = calculate.CalculateAmount(perf.Audience, play);
            totalAmount += amount;
            credits += CreateVolumeCredit(0, perf.Audience, play.Type);
            newXMLStatement.Element("Statement").Add(new XElement("Items",
                                                        new XElement("Item",
                                                            new XElement("Name", play.Name),
                                                            new XElement("AmountOwed", amount),
                                                            new XElement("EarnedCredits", CreateVolumeCredit(0, perf.Audience, play.Type)),
                                                            new XElement("Seats", perf.Audience)
                                                            )
                                                        )
                                                    );
        }
        newXMLStatement.Element("Statement").Add(new XElement("AmountOwed", totalAmount),
                        new XElement("EarnedCredits", credits));

        newXMLStatement.Save( path + "StatementXML - " + invoice.Customer + ".xml" );
        return newXMLStatement.ToString();
    }

    private decimal CreateVolumeCredit(decimal volumeCredits, int audience, string play)
    {
        // add volume credits
        volumeCredits += Math.Max(audience - 30, 0);
        // add extra credit for every ten comedy attendees
        if ("comedy" == play) volumeCredits += (int)Math.Floor((decimal)audience / 5);
        return volumeCredits;
    }
}
