using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;

public class EmailSender
{
    /// <summary>
    /// 发送模板邮件
    /// </summary>
    /// <param name="templatePath">模板文件路径</param>
    /// <param name="templateData">模板数据字典</param>
    /// <param name="smtpConfig">SMTP配置</param>
    public static void SendTemplateEmail(
        string templatePath,
        Dictionary<string, string> templateData,
        SmtpConfig smtpConfig)
    {
        // 1. 加载邮件模板
        string emailTemplate = File.ReadAllText(templatePath);

        // 2. 替换模板中的动态内容
        string emailBody = ReplaceTemplatePlaceholders(emailTemplate, templateData);

        // 3. 创建邮件对象
        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress(smtpConfig.SenderEmail, smtpConfig.SenderName);
            mail.To.Add(smtpConfig.RecipientEmail);
            mail.Subject = smtpConfig.Subject;
            mail.Body = emailBody;
            mail.IsBodyHtml = true; // 设置为HTML格式邮件

            // 4. 配置SMTP客户端
            using (SmtpClient smtpClient = new SmtpClient(smtpConfig.Host, smtpConfig.Port))
            {
                smtpClient.Credentials = new NetworkCredential(
                    smtpConfig.Username,
                    smtpConfig.Password
                );
                smtpClient.EnableSsl = smtpConfig.EnableSSL;

                try
                {
                    // 5. 发送邮件
                    smtpClient.Send(mail);
                    Console.WriteLine("邮件发送成功！");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine($"SMTP错误: {ex.StatusCode}");
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"发送失败: {ex.Message}");
                    throw;
                }
            }
        }
    }

    /// <summary>
    /// 替换模板中的占位符
    /// </summary>
    private static string ReplaceTemplatePlaceholders(
        string template,
        Dictionary<string, string> data)
    {
        foreach (var item in data)
        {
            // 使用双花括号作为占位符，例如：{{UserName}}
            string placeholder = $"{{{{{item.Key}}}}}";
            template = template.Replace(placeholder, item.Value);
        }
        return template;
    }
}

/// <summary>
/// SMTP服务器配置模型
/// </summary>
public class SmtpConfig
{
    public string Host { get; set; }        // SMTP服务器地址
    public int Port { get; set; }           // SMTP端口（默认587）
    public string Username { get; set; }    // 邮箱账号
    public string Password { get; set; }    // 邮箱密码/授权码
    public bool EnableSSL { get; set; }     // 启用SSL加密
    public string SenderEmail { get; set; } // 发件人邮箱
    public string SenderName { get; set; }  // 发件人显示名称
    public string RecipientEmail { get; set; } // 收件人邮箱
    public string Subject { get; set; }      // 邮件主题
}

// 使用示例
class ProgramTest
{
    static void Main()
    {
        // 配置SMTP参数（以QQ邮箱为例）
        var smtpConfig = new SmtpConfig
        {
            Host = "smtp.qq.com",
            Port = 587,
            Username = "your_email@qq.com",
            Password = "your_authorization_code", // 邮箱授权码，非登录密码
            EnableSSL = true,
            SenderEmail = "your_email@qq.com",
            SenderName = "系统邮件",
            RecipientEmail = "recipient@example.com",
            Subject = "您的订单确认信息"
        };

        // 准备模板数据
        var templateData = new Dictionary<string, string>
        {
            { "UserName", "张三" },
            { "OrderNumber", "ORD20230625001" },
            { "OrderDate", DateTime.Now.ToString("yyyy-MM-dd") },
            { "TotalAmount", "¥1288.00" }
        };

        // 发送邮件
        try
        {
            EmailSender.SendTemplateEmail(
                templatePath: "email_template.html",
                templateData: templateData,
                smtpConfig: smtpConfig
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"邮件发送异常: {ex.Message}");
        }
    }
}
