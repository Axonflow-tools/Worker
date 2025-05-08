using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Analysis;
using Worker.Services;

[ApiController]
[Route("[controller]")]
public class DataPreviewController : ControllerBase
{
    [HttpGet("columns", Name = "GetColumnNames")]
    public IActionResult GetColumnNames()
    {
        try
        {
            var df = DataFrame.LoadCsv(FileService.CurrentFile);
            return Ok(df.Columns.Select(col => col.Name).ToList());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error loading data: {ex.Message}");
        }
    }

    [HttpGet("head", Name = "GetHeadData")]
    public IActionResult GetHeadData([FromQuery] int rows = 5)
    {
        try
        {
            var df = DataFrame.LoadCsv(FileService.CurrentFile);
            var head = df.Head(rows);
            
            // Now only returning the data rows without column names
            return Ok(GetRowValues(head));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error processing data: {ex.Message}");
        }
    }
    
    [HttpGet("tail", Name = "GetTailData")]
    public IActionResult GetTailData([FromQuery] int rows = 5)
    {
        try
        {
            var df = DataFrame.LoadCsv(FileService.CurrentFile);
            var head = df.Tail(rows);
            
            // Now only returning the data rows without column names
            return Ok(GetRowValues(head));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error processing data: {ex.Message}");
        }
    }

    private static List<List<object>> GetRowValues(DataFrame df)
    {
        var rows = new List<List<object>>();
    
        for (long i = 0; i < df.Rows.Count; i++)
        {
            var rowValues = new List<object>();
            foreach (var column in df.Columns)
            {
                rowValues.Add(column[i] is float floatVal ? (object)floatVal : 
                    column[i] is int intVal ? intVal :
                    column[i]?.ToString() ?? null);
            }
            rows.Add(rowValues);
        }
    
        return rows;
    }
}