namespace FormulaOne.Api.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using FormulaOne.AirlineService.Services;
using RestSharp;
using FormulaOne.Entities.Dtos.Responses;
using Polly.Retry;
using Polly;
using System.Net;
using Polly.CircuitBreaker;

public class FlightService : IFlightService
{

    // retry policy
    // private static readonly AsyncRetryPolicy<RestResponse> RetryPolicy = Policy
    //     .HandleResult<RestResponse>(resp => resp.StatusCode == HttpStatusCode.TooManyRequests
    //     || (int)resp.StatusCode >= 500)
    //     .WaitAndRetryAsync(4, retryAttempt => {
    //         var timeToWait = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
    //         Console.WriteLine($"Waiting {timeToWait.Seconds} seconds");
    //         return timeToWait;
    //     }, (exception, timeSpan, retryCount, context) => {
    //         Console.WriteLine($"Retry {retryCount} after {timeSpan.Seconds} seconds");
    //     });

    // retry policy
    private static readonly AsyncRetryPolicy<RestResponse> RetryPolicy =
    Policy.HandleResult<RestResponse>(resp =>
        resp.StatusCode == HttpStatusCode.TooManyRequests || (int)resp.StatusCode >= 500)
        .WaitAndRetryAsync(4, retryAttempt =>
        {
            Console.WriteLine($"Attempt {retryAttempt}- Retrying due to server error");
            return TimeSpan.FromSeconds(5 + retryAttempt);
        });


    // circuit breaker policy
    private static readonly AsyncCircuitBreakerPolicy<RestResponse> CBPolicy = Policy
        .HandleResult<RestResponse>(
            resp => (int)resp.StatusCode >= 500)
        .CircuitBreakerAsync(4, TimeSpan.FromSeconds(30));

    // Advanced circuit breaker policy
    private static readonly AsyncCircuitBreakerPolicy<RestResponse> AdvancedCBPolicy = Policy
   .HandleResult<RestResponse>(
       resp => (int)resp.StatusCode >= 500)
   .AdvancedCircuitBreakerAsync(0.5,  // failure threshold = 50%
   TimeSpan.FromSeconds(30), // sampling duration = 30 seconds
    10, // minimum number of consecutive exceptions before breaking circuit
     TimeSpan.FromSeconds(30) // time circuit opened before retrying
     );


    public async Task<List<FlightDto>> GetAllAvailableFlights()
    {
        try
        {
            if (CBPolicy.CircuitState == CircuitState.Open)
            {
                throw new Exception("Service not available, thrown intentionally");
            }
            const string url = "http://localhost:5199/api/FlightsCalendar";
            var client = new RestClient();
            var request = new RestRequest(url);

            // Normal execution
            //  var response = await client.ExecuteAsync(request);

            // Retry policy execution
            // var response = await RetryPolicy.ExecuteAsync(async () => await client.ExecuteAsync(request));

            // Circuit breaker policy execution
            // var response = await CBPolicy.ExecuteAsync(async () => await client.ExecuteAsync(request));

            // Retry and circuit breaker policy execution
            // var response = await CBPolicy.ExecuteAsync(async () =>
            //     await RetryPolicy.ExecuteAsync(async () =>
            //     await client.ExecuteAsync(request)));

            // Advanced circuit breaker policy execution
            var response = await AdvancedCBPolicy.ExecuteAsync(async () =>
                await RetryPolicy.ExecuteAsync(async () =>
                await client.ExecuteAsync(request)));

            if (!response.IsSuccessful)
            {
                throw new Exception("Service not available, thrown intentionally");
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var flights = JsonSerializer.Deserialize<List<FlightDto>>(response!.Content!, options);

            return flights!;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);

        }
    }
}


