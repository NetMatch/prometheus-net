using System;
using Microsoft.AspNetCore.Http;

namespace Prometheus.HttpMetrics
{
    public sealed class HttpMiddlewareExporterOptions
    {
        public HttpInProgressOptions InProgress { get; set; } = new HttpInProgressOptions();
        public HttpRequestCountOptions RequestCount { get; set; } = new HttpRequestCountOptions();
        public HttpRequestDurationOptions RequestDuration { get; set; } = new HttpRequestDurationOptions();

        /// <summary>
        /// Whether to capture metrics for queries to the /metrics endpoint (where metrics are exported by default). Defaults to false.
        /// This matches against URLs starting with the /metrics string specifically - if you use a custom metrics endpoint, this will not match.
        /// </summary>
        public bool CaptureMetricsUrl { get; set; }

        /// <summary>
        /// Adds an additional route parameter to all the HTTP metrics.
        ///
        /// Helper method to avoid manually adding it to each one.
        /// </summary>
        public void AddRouteParameter(HttpRouteParameterMapping mapping)
        {
            InProgress.AdditionalRouteParameters.Add(mapping);
            RequestCount.AdditionalRouteParameters.Add(mapping);
            RequestDuration.AdditionalRouteParameters.Add(mapping);
        }

        /// <summary>
        /// Adds an additional label to all the HTTP metrics.
        ///
        /// Helper method to avoid manually adding it to each one.
        /// </summary>
        public void AddLabel(string labelName, Func<HttpContext, string> valueProvider)
        {
            Collector.ValidateLabelName(labelName);

            InProgress.AdditionalLabels.Add(labelName, valueProvider);
            RequestCount.AdditionalLabels.Add(labelName, valueProvider);
            RequestDuration.AdditionalLabels.Add(labelName, valueProvider);
        }
    }
}