using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using ReportService.Dto;
using RabbitMQ.Client;
using ReportService.Rabbit;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading.Tasks; // Eksik olan using ekledik.

namespace ReportService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly EntityContext _context;
        private readonly RabbitMQConfig _rabbitMQConfig;

        public ReportController(EntityContext context, IOptions<RabbitMQConfig> rabbitMQConfig)
        {
            _context = context;
            _rabbitMQConfig = rabbitMQConfig.Value;
        }
        
        // GET: api/Report/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var report = _context.Reports.FirstOrDefault(p => p.Id == id);

            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        // POST: api/Report
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReportRequest request)
        {
            if (request == null)
            {
                return BadRequest("Geçersiz veri.");
            }

            var report = new Report()
            {
                UserId = request.UserId,
                Status = "preparing",
                Path = "path",
                CreatedAt = DateTime.UtcNow
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            Task.Run(() => ProcessReport(report)); // İş parçacığını başlat

            return CreatedAtAction(nameof(Get), new { id = report.Id }, report);
        }

        private void ProcessReport(Report report)
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory
                {
                    HostName = _rabbitMQConfig.HostName,
                    Port = _rabbitMQConfig.Port,
                    UserName = _rabbitMQConfig.UserName,
                    Password = _rabbitMQConfig.Password
                };

                using (IConnection connection = factory.CreateConnection())
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "report_status", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var message = "Rapor tamamlandı: " + report.Id;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "report_status", basicProperties: null, body: body);

                    Console.WriteLine(" [x] Sent {0}", message);
                }

                // Rapor durumunu güncelle
                report.Status = "completed";
                _context.SaveChanges(); // Veritabanına kaydet
            }
            catch (Exception ex)
            {
                // Hata ayıklama veya loglama için hata mesajını kaydedebilirsiniz
                Console.WriteLine("Hata: " + ex.Message);
            }
        }
    }
}
