using Amazon.Lambda.Core;
using System.Text.Json.Serialization;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace TravelPackagesSystem.DisableEventPaymentPlans;

public class Function
{
    
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public object FunctionHandler(JobInput input, ILambdaContext context)
    {
        var stepName = input.StepName ?? "UnknownStep";

        context.Logger.LogLine($"Hello World test invoked. StepName={stepName}, ExecutionMode={input.ExecutionMode}, ExecutionId={input.ExecutionId}");

        var expiredPackageCount = string.Equals(stepName, "ExpireAndBlockPackages", StringComparison.OrdinalIgnoreCase)
            ? 1
            : 0;

        return new
        {
            stepName,
            status = "Succeeded",
            message = "Hello World from TravelPackagesSystem placeholder Lambda.",
            jobName = input.JobName,
            environment = input.Environment,
            executionMode = input.ExecutionMode,
            asOfUtc = input.AsOfUtc,
            executionId = input.ExecutionId,
            rowsAffected = 0,
            expiredPackageCount,
            durationMs = 1
        };
    }
}

public sealed class JobInput
{
    [JsonPropertyName("jobName")]
    public string? JobName { get; set; }

    [JsonPropertyName("environment")]
    public string? Environment { get; set; }

    [JsonPropertyName("stepName")]
    public string? StepName { get; set; }

    [JsonPropertyName("executionMode")]
    public string? ExecutionMode { get; set; }

    [JsonPropertyName("asOfUtc")]
    public string? AsOfUtc { get; set; }

    [JsonPropertyName("executionId")]
    public string? ExecutionId { get; set; }

    [JsonPropertyName("triggeredBy")]
    public string? TriggeredBy { get; set; }
}
