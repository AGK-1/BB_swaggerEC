using System.Collections.Generic;
using System.Linq;
using get_data_from_excel;
using OfficeOpenXml;

public class ExcelService2
{
	private List<Mymodel> _data; // This should be populated by your ImportExcelData method

	public ExcelService2()
	{
		_data = new List<Mymodel>();
	}

	public void ImportExcelData(string filePath)
	{
		// Example code to import data
		var dataList = new List<Mymodel>();
		using (var package = new ExcelPackage(new FileInfo(filePath)))
		{
			var worksheet = package.Workbook.Worksheets[0];
			for (int row = 1; row <= worksheet.Dimension.End.Row; row++) // Assuming the first row is headers
			{
				var model = new Mymodel
				{
					// Assuming columns are in the order: Id, Name, Email
					Id = int.Parse(worksheet.Cells[row, 1].Text),
					Name = worksheet.Cells[row, 2].Text,
					Price = worksheet.Cells[row, 3].Text
				};
				_data.Add(model);
			}
		}
	}

	public List<Mymodel> GetData()
	{
		return _data; // Return the data that was imported
	}

	public Mymodel GetDataById(int id)
	{
		return _data.FirstOrDefault(d => d.Id == id);
	}
}