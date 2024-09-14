using get_data_from_excel;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ExcelService
{
	public List<Mymodel> ImportExcelData(string filePath)
	{
		var dataList = new List<Mymodel>();

		using (var package = new ExcelPackage(new FileInfo(filePath)))
		{
			var worksheet = package.Workbook.Worksheets.FirstOrDefault();
			if (worksheet != null)
			{
				int rowCount = worksheet.Dimension.Rows;
				for (int row = 1; row <= rowCount; row++) // Assuming first row is header
				{
					var data = new Mymodel
					{
						Id = int.Parse(worksheet.Cells[row, 1].Text),
						Name = worksheet.Cells[row, 2].Text,
						Price = worksheet.Cells[row, 3].Text
					};
					dataList.Add(data);
				}
			}
		}
		return dataList;
	}
}