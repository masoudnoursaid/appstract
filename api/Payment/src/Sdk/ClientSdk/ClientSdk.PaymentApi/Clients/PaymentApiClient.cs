﻿using System.Text;
using Microsoft.Extensions.Logging;
using Payment.Common.SDK.Consts;
using Payment.Common.SDK.Models;
using Payment.Sdk.Common.Model.Configuration;
using Payment.Sdk.Common.Utils;

// ReSharper disable CheckNamespace
// ReSharper disable UnusedParameterInPartialMethod

namespace ClientSdk.PaymentApi.V1;

public partial class PaymentApiClient
{
    private readonly PaymentConfiguration _configuration;
    private readonly ILogger<PaymentApiClient> _logger;

    public PaymentApiClient(ILogger<PaymentApiClient> logger
        , PaymentConfiguration configuration
        , HttpClient client) : this(client)
    {
        _logger = logger;
        _configuration = configuration;
    }


    partial void PrepareRequest(HttpClient client, HttpRequestMessage request,
        StringBuilder urlBuilder)
    {
        var info =
            new HmacInfoObject(_configuration.ConnectionConfiguration.Uri, HttpMethod.Put);

        var hmacHash = SecurityUtils.ComputeHmacSha256(info, _configuration.SecurityConfiguration.ApiSecret!).Result;

        _logger?.LogDebug($"PAYMENT.CLIENT --- Hmac generated by value : {hmacHash}");

        List<(string name, string value)> headers = new()
        {
            (HmacAuthentication.SIGNATURE_HEADER, hmacHash),
            (HmacAuthentication.API_KEY_HEADER, _configuration.SecurityConfiguration.ApiKey!),
            (HmacAuthentication.DATE_HEADER, info.Ticks.ToString()),
            (HmacAuthentication.NONCE_HEADER, info.Nonce)
        };
        headers.ForEach(h =>
        {
            client.DefaultRequestHeaders.Remove(h.name);
            client.DefaultRequestHeaders.Add(h.name, h.value);
        });
    }
}