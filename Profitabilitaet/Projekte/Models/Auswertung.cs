using Profitabilitaet.Database.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Profitabilitaet.Projekte.Models
{
    internal readonly record struct ProjektProfitabilitaet(int ProjektId, string Bezeichung, decimal Profitabilitaet)
    {

    }

    public static class Auswertung
    {
        public static Task CreateAsync(string path, IReadOnlyList<Projekt> projekte)
        {
            return Task.Run(() => CreateSpreadsheet(path, GetProfitabilitaet(projekte)));
        }

        private static IEnumerable<ProjektProfitabilitaet> GetProfitabilitaet(IReadOnlyList<Projekt> projekte)
        {
            return projekte.Select(x => new ProjektProfitabilitaet(x.Id.Value, x.Bezeichnung, x.Profitabilitaet)).OrderByDescending(x => x.Profitabilitaet);
        }

        private static void CreateSpreadsheet(string path, IEnumerable<ProjektProfitabilitaet> projektProfitabilitaet)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Auswertung");
            worksheet.Cell(1, 1).Value = "Nummer";
            worksheet.Cell(1, 2).Value = "Bezeichnung";
            worksheet.Cell(1, 3).Value = "Profitabilität";

            worksheet.SetHeaderCellStyle(1);
            worksheet.SetHeaderCellStyle(2);
            worksheet.SetHeaderCellStyle(3);

            worksheet.Cell(2, 1).InsertData(projektProfitabilitaet);

            worksheet.Columns(3, 3).Style.NumberFormat.SetNumberFormatId(2);

            worksheet.Columns().AdjustToContents();

            workbook.SaveAs(path);
        }
    }

    public static class WorksheetExtensions
    {
        public static void SetHeaderCellStyle(this IXLWorksheet worksheet, int column)
        {
            worksheet.Cell(1, column).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell(1, column).Style.Font.Bold = true;
        }
    }
}
