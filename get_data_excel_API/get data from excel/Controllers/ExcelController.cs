using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace get_data_from_excel.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExcelController : ControllerBase
	{
		private readonly ExcelService _excelService;
		private readonly ExcelService2 _excelService2;
		public ExcelController(ExcelService excelService, ExcelService2 excelService2)
		{
			_excelService = excelService;
			_excelService2 = excelService2;
			
		}

		[HttpPost]
		[Route("upload")]
		[SwaggerOperation(Summary = "Upload an Excel file", Description = "Uploads an Excel file and returns the data.")]
		public async Task<ActionResult<List<Mymodel>>> UploadExcel(IFormFile file)
		{
			if (file == null || file.Length == 0)
				return BadRequest("Please upload a valid Excel file.");

			// Save the uploaded file to a temporary path
			var tempFilePath = Path.GetTempFileName();
			using (var stream = new FileStream(tempFilePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			// Process the Excel file using ExcelService
			var data = _excelService.ImportExcelData(tempFilePath);

			// Clean up the temporary file after processing
			System.IO.File.Delete(tempFilePath);

			return Ok(data);
		}

		[HttpGet]
		[Route("import/{id}")]
		public ActionResult<List<Mymodel>> ImportExcel(int id, string vek)
		{
			return Ok();
		}

		[HttpGet]
		[Route("Get all")]
		public ActionResult<List<Mymodel>> GetData()
		{
			_excelService2.ImportExcelData("Your path is here.xlsx");
			var data = _excelService2.GetData();
			if (data == null || !data.Any())
			{
				return NotFound("No data found.");
			}
			return Ok(data);
		}

		[HttpGet]
		[Route("data/{id}")]
		public ActionResult<Mymodel> GetDataById(int id)
		{
			var data = _excelService2.GetDataById(id);
			if (data == null)
			{
				return NotFound($"Data with ID {id} not found.");
			}
			return Ok(data);
		}
	}
}
