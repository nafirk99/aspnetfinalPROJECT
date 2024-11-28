using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.IO;


namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LogController : Controller
    {
        private readonly string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");

        public IActionResult Index()
        {
            var logData = new StringBuilder();

            // Read each log file in the Logs directory
            foreach (var logFile in Directory.GetFiles(logFilePath, "*.log"))
            {
                logData.AppendLine($"Log file: {Path.GetFileName(logFile)}");
                logData.AppendLine("-----------------------------------------");

                try
                {
                    // Open the file with shared read access
                    using (var fileStream = new FileStream(logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var reader = new StreamReader(fileStream))
                    {
                        logData.AppendLine(reader.ReadToEnd());
                    }
                }
                catch (IOException ex)
                {
                    // Handle file access exceptions (file locked)
                    logData.AppendLine($"Unable to read {Path.GetFileName(logFile)} because it is being used by another process.");
                }
                logData.AppendLine();
            }

            // Pass the logs to the view
            ViewBag.LogContent = logData.ToString();
            return View();
        }
    }
}
