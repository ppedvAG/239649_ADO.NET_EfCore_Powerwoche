using ClosedXML.Excel;
using SuperDbApp.Model;

namespace SuperDbApp.Data
{
    internal class ExcelManager
    {
        public void SaveProductsToExcel(IEnumerable<Product> products, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products");

                // Überschriften
                worksheet.Cell("A1").Value = "Product ID";
                worksheet.Cell("B1").Value = "Product Name";
                worksheet.Cell("C1").Value = "Material";
                worksheet.Cell("D1").Value = "Price";
                worksheet.Cell("E1").Value = "Factory ID";
                worksheet.Cell("F1").Value = "Factory Address";
                worksheet.Cell("G1").Value = "Factory Country";

                int currentRow = 2; // Starten Sie in der zweiten Zeile, da die erste die Überschriften enthält

                foreach (var product in products)
                {
                    worksheet.Cell(currentRow, 1).Value = product.Id;
                    worksheet.Cell(currentRow, 2).Value = product.Name;
                    worksheet.Cell(currentRow, 3).Value = product.Material;
                    worksheet.Cell(currentRow, 4).Value = product.Price;
                    worksheet.Cell(currentRow, 5).Value = product.Factory.Id;
                    worksheet.Cell(currentRow, 6).Value = product.Factory.Adress;
                    worksheet.Cell(currentRow, 7).Value = product.Factory.Country;

                    currentRow++;
                }

                // Optional: Formatieren der Tabelle
                worksheet.Columns().AdjustToContents(); // Spaltenbreite automatisch anpassen

                workbook.SaveAs(filePath);
            }
        }
    }
}
