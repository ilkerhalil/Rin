﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Rin.Core.Record
{
    public class HttpRequestRecordInfo
    {
        public string Id { get; set; }
        public string ParentId { get; set; }

        public bool IsHttps { get; set; }
        public HostString Host { get; set; }
        public QueryString QueryString { get; set; }
        public PathString Path { get; set; }
        public string Method { get; set; }
        public int ResponseStatusCode { get; set; }
        public IPAddress RemoteIpAddress { get; set; }

        public DateTimeOffset RequestReceivedAt { get; set; }
        public DateTimeOffset TransferringCompletedAt { get; set; }

        public bool IsCompleted => TransferringCompletedAt != default(DateTimeOffset);

        public static HttpRequestRecordInfo CreateFromRecord(HttpRequestRecordInfo record)
        {
            return new HttpRequestRecordInfo
            {
                Id = record.Id,
                ParentId = record.Id,
                IsHttps = record.IsHttps,
                Host = record.Host,
                QueryString = record.QueryString,
                Path = record.Path,
                Method = record.Method,
                ResponseStatusCode = record.ResponseStatusCode,
                RemoteIpAddress = record.RemoteIpAddress,
                RequestReceivedAt = record.RequestReceivedAt,
                TransferringCompletedAt = record.TransferringCompletedAt,
            };
        }
    }

    public class HttpRequestRecord : HttpRequestRecordInfo
    {
        public IDictionary<string, StringValues> RequestHeaders { get; set; }
        public IDictionary<string, StringValues> ResponseHeaders { get; set; }

        public Exception Exception { get; set; }
        public ITimelineScope Timeline { get; set; }

        internal ITimelineScope Processing { get; set; }
        internal ITimelineScope Transferring { get; set; }
    }
}
