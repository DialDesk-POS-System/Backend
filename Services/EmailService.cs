using System.Net;
using System.Net.Mail;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;

namespace DialDesk.Server.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration config, ILogger<EmailService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task SendInvoiceEmailAsync(string toEmail, string customerName, string invoiceNo, decimal totalAmount, List<SaleItem> items)
        {
            try
            {
                var smtpHost = _config["Smtp:Host"] ?? "smtp.gmail.com";
                var smtpPort = int.Parse(_config["Smtp:Port"] ?? "587");
                var smtpUser = _config["Smtp:Username"] ?? "";
                var smtpPass = _config["Smtp:Password"] ?? "";
                var fromEmail = _config["Smtp:FromEmail"] ?? smtpUser;
                var fromName = _config["Smtp:FromName"] ?? "Chronos Watch POS";

                if (string.IsNullOrWhiteSpace(smtpUser) || string.IsNullOrWhiteSpace(smtpPass))
                {
                    _logger.LogWarning("SMTP credentials not configured. Skipping email for invoice {InvoiceNo}", invoiceNo);
                    return;
                }

                var itemsHtml = "";
                if (items != null && items.Count > 0)
                {
                    itemsHtml = @"
                    <h3 style=""margin-top: 24px; color: #111827; font-size: 16px;"">Purchased Items</h3>
                    <table style=""width: 100%; border-collapse: collapse; margin-top: 12px; font-size: 14px;"">
                        <thead>
                            <tr style=""border-bottom: 2px solid #e5e7eb; color: #6b7280; text-align: left;"">
                                <th style=""padding: 8px 0;"">Item</th>
                                <th style=""padding: 8px 0; text-align: center;"">Qty</th>
                                <th style=""padding: 8px 0; text-align: right;"">Price</th>
                            </tr>
                        </thead>
                        <tbody>";

                    foreach (var item in items)
                    {
                        var watchName = item.Watch?.Model?.ModelName ?? $"Watch #{item.WatchId}";
                        itemsHtml += $@"
                            <tr style=""border-bottom: 1px solid #e5e7eb;"">
                                <td style=""padding: 12px 0; color: #374151;"">{watchName}</td>
                                <td style=""padding: 12px 0; text-align: center; color: #6b7280;"">1</td>
                                <td style=""padding: 12px 0; text-align: right; color: #111827; font-weight: 500;"">${item.LineTotal:N2}</td>
                            </tr>";
                    }

                    itemsHtml += "</tbody></table>";
                }

                var subject = $"Your Invoice {invoiceNo} from DialDesk";
                var body = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: 'Segoe UI', Arial, sans-serif; background: #f4f7f6; margin: 0; padding: 0; }}
        .container {{ max-width: 600px; margin: 40px auto; background: #fff; border-radius: 16px; overflow: hidden; box-shadow: 0 4px 24px rgba(0,0,0,0.08); }}
        .header {{ background: linear-gradient(135deg, #10b981, #059669); padding: 32px; text-align: center; }}
        .header h1 {{ color: #fff; margin: 0; font-size: 24px; }}
        .header p {{ color: rgba(255,255,255,0.85); margin: 8px 0 0; font-size: 14px; }}
        .body {{ padding: 32px; }}
        .detail {{ display: flex; justify-content: space-between; padding: 12px 0; border-bottom: 1px solid #e5e7eb; font-size: 14px; }}
        .detail:last-child {{ border-bottom: none; }}
        .label {{ color: #6b7280; }}
        .value {{ font-weight: 600; color: #111827; }}
        .total {{ font-size: 28px; font-weight: 700; color: #10b981; text-align: center; margin: 24px 0; }}
        .footer {{ background: #f9fafb; padding: 20px 32px; text-align: center; font-size: 12px; color: #9ca3af; }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>⌚ Chronos Watch POS</h1>
            <p>Invoice Confirmation</p>
        </div>
        <div class=""body"">
            <p style=""font-size: 16px; color: #374151;"">Dear <strong>{customerName ?? "Valued Customer"}</strong>,</p>
            <p style=""font-size: 14px; color: #6b7280; line-height: 1.6;"">
                Thank you for your purchase! Here is a summary of your transaction.
            </p>

            <div style=""background: #f9fafb; border-radius: 12px; padding: 20px; margin: 24px 0;"">
                <div class=""detail"">
                    <span class=""label"">Invoice No</span>
                    <span class=""value"">{invoiceNo}</span>
                </div>
                <div class=""detail"">
                    <span class=""label"">Date</span>
                    <span class=""value"">{DateTime.UtcNow:MMMM dd, yyyy}</span>
                </div>
            </div>

            {itemsHtml}

            <div class=""total"">Total: ${totalAmount:N2}</div>

            <p style=""font-size: 13px; color: #9ca3af; text-align: center;"">
                If you have any questions, please contact our store.
            </p>
        </div>
        <div class=""footer"">
            © {DateTime.UtcNow.Year} DialDesk Watch POS. All rights reserved.
        </div>
    </div>
</body>
</html>";

                using var client = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUser, smtpPass),
                    EnableSsl = true
                };

                var message = new MailMessage
                {
                    From = new MailAddress(fromEmail, fromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                message.To.Add(new MailAddress(toEmail));

                await client.SendMailAsync(message);
                _logger.LogInformation("Invoice email sent to {Email} for invoice {InvoiceNo}", toEmail, invoiceNo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send invoice email to {Email} for invoice {InvoiceNo}", toEmail, invoiceNo);
                throw;
            }
        }
    }
}
