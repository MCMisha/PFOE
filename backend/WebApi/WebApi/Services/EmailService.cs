﻿using System.Globalization;
using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using WebApi.Enums;

namespace WebApi.Services;

public static class EmailService
{
    private static Dictionary<string, List<string>> parametersByEmailType = new ();
    private static Dictionary<string, string> subjectByEmailType = new();
    private static readonly TransactionalEmailsApi apiIstance;
    private static readonly string senderName;
    private static readonly string senderEmail;
    static EmailService()
    {
        if (apiIstance == null)
        {
            apiIstance = new TransactionalEmailsApi();    
        }
        senderName = "Platforma do organizacji wydarzeń";
        senderEmail = "pfoe.mfii@proton.me";

        InitEmailTypes();
    }

    private static void InitEmailTypes()
    {
        TextInfo textInfo = new CultureInfo("en-GB").TextInfo;
        var paramsForRegistrationAndBlocked = new List<string>
        {
            "login"
        };
        var paramsForSignForEvent = new List<string>
        {
            "eventTitle"
        };
        parametersByEmailType.Add(textInfo.ToTitleCase(nameof(EmailType.REGISTRATION).ToLower()).Replace("_", ""), paramsForRegistrationAndBlocked);
        subjectByEmailType.Add(textInfo.ToTitleCase(nameof(EmailType.REGISTRATION).ToLower()).Replace("_", ""), "Rejestracja");
        parametersByEmailType.Add(textInfo.ToTitleCase(nameof(EmailType.BLOCKED_USER).ToLower()).Replace("_", ""), paramsForRegistrationAndBlocked);
        subjectByEmailType.Add(textInfo.ToTitleCase(nameof(EmailType.BLOCKED_USER).ToLower()).Replace("_", ""), "Zablokowane konto");
    }

    public static void SendEmailByType(string email, string name, string emailType, params string[] paramValues)
    {
        SendSmtpEmailSender emailSender = new(senderName, senderEmail);
        JObject Headers = new()
        {
            { "content-type", "text/html"}
        };
        
        SendSmtpEmailTo smtpEmailTo = new(email, name);
        List<SendSmtpEmailTo> To = new()
        {
            smtpEmailTo
        };           
        
        string content = File.ReadAllText($"EmailTemplates/{emailType}.html");

        JObject Params = new JObject();
        var parameters = parametersByEmailType[emailType];
        for(int i = 0; i < paramValues.Length; i++)
        {
            Params.Add(parameters[i], paramValues[i]);
        }

        SendSmtpEmailTo1 smtpEmailTo1 = new(email, name);
        List<SendSmtpEmailTo1> sendList = new()
        {
            smtpEmailTo1
        };
        string? TextContent = null;
        Dictionary<string, object> _params = new()
        {
            { "params", Params }
        };
        string Subject = subjectByEmailType[emailType];
        SendSmtpEmailMessageVersions messageVersion =
            new(sendList, _params, null, null, null, Subject);
        List<SendSmtpEmailMessageVersions> messageVersiopns = new()
        {
            messageVersion
        };
        try
        {
            var sendSmtpEmail = new SendSmtpEmail(emailSender, To, null, null, content, TextContent, Subject, null,
                null, Headers, null, Params, messageVersiopns, null);

            CreateSmtpEmail result = apiIstance.SendTransacEmail(sendSmtpEmail);              
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}