using System.Collections.Concurrent;
using Microsoft.Extensions.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ImageResizingWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ConcurrentQueue<string> _imageQueue;

        public Worker(ILogger<Worker> logger, ConcurrentQueue<string> imageQueue)
        {
            _logger = logger;
            _imageQueue = imageQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Image Resizing Worker started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_imageQueue.TryDequeue(out var imagePath))
                {
                    try
                    {
                        ResizeImage(imagePath);
                        _logger.LogInformation($"Image resized: {imagePath}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error resizing image: {ex.Message}");
                    }
                }

                await Task.Delay(1000, stoppingToken);
            }
        }

        private void ResizeImage(string imagePath)
        {
            using (var image = Image.Load(imagePath))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(300, 300), // Example: Resize to 300x300
                    Mode = ResizeMode.Max
                }));
                var resizedPath = Path.Combine(Path.GetDirectoryName(imagePath)!, "Resized_" + Path.GetFileName(imagePath));
                image.Save(resizedPath);
            }
        }
    }
}
