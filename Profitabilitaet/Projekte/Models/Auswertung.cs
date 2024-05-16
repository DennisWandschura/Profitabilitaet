using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using Profitabilitaet.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace Profitabilitaet.Projekte.Models
{
    internal readonly record struct ProjektProfitabilitaet(string Bezeichung, decimal Profitabilitaet)
    {

    }

    public class Auswertung(string path, IReadOnlyList<Database.Entities.Projekt> projekte)
    {
        private readonly string _path = path;
        private readonly IReadOnlyList<Projekt> _projekte = projekte;

        public Task CreateAsync()
        {
            return Task.Run(() => CreateSpreadsheet(_path, GetProfitabilitaet()));
        }

        private IEnumerable<ProjektProfitabilitaet> GetProfitabilitaet()
        {
            return _projekte.Select(x => new ProjektProfitabilitaet(x.Bezeichnung, x.Profitabilitaet)).OrderByDescending(x => x.Profitabilitaet);
        }

        private static void CreateSpreadsheet(string path, IEnumerable<ProjektProfitabilitaet> projektProfitabilitaet)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sample Sheet");
                worksheet.Cell(1, 1).Value = "Bezeichnung";
                worksheet.Cell(1, 2).Value = "Profitabilität";

                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Cell(1, 2).Style.Font.Bold = true;

                //worksheet.Row(1).

                worksheet.Cell(2, 1).InsertData(projektProfitabilitaet);

                worksheet.Columns(2,2).Style.NumberFormat.SetNumberFormatId(2);

                worksheet.Columns().AdjustToContents();

                workbook.SaveAs(path);
            }
        }
    }
}
