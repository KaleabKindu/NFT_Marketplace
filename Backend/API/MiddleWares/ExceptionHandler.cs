using MediatR;
using System.Net;
using System.Text.Json;
using Application.Common.Exceptions;
using ErrorOr;


namespace API.MiddleWares
{
    public class ExceptionHandler
    {

        public readonly RequestDelegate _next;
        public readonly IHostEnvironment _env;
        public readonly ILogger<ExceptionHandler> _logger;
        public static JsonSerializerOptions options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        private ErrorOr<Unit> response { get; set; }
   
        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger, IHostEnvironment env)
        {
            _env = env;
            _next = next;
            _logger = logger;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            bool failed = false;
            
            try
            {
                await _next(context);
            }
            catch (OwnershipVerificationException ex)
            {
                Console.WriteLine("OwnershipVerificationException.............");
                failed = true;
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                _logger.LogError(ex, ex.Message);
                response = Error.Failure("OwnershipVerificationException", ex.Message);
                
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine("InsufficientFundsException.............");
                failed = true;
                context.Response.StatusCode = (int)HttpStatusCode.PreconditionFailed;
                _logger.LogError(ex, ex.Message);
                response = Error.Validation("Issufficient Funds",ex.Message);
            }
            catch (TransactionFailureException ex)
            {
                Console.WriteLine("TransactionFailureException.............");
                failed = true;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _logger.LogError(ex, ex.Message);
                response = Error.Failure("TransactionFailureException", ex.Message);
            }
            catch (MetadataValidationException ex)
            {
                Console.WriteLine("MetadataValidationException.............");
                failed = true;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _logger.LogError(ex, ex.Message);
                response = Error.Validation("MetadataValidationException",ex.Message);
            }
            catch (ContractDeploymentException ex)
            {
                Console.WriteLine("ContractDeploymentException.............");
                failed = true;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _logger.LogError(ex, ex.Message);
                response = Error.Failure("ContractDeploymentException", ex.Message);
            }
            catch (BlockchainConnectivityException ex)
            {
                Console.WriteLine("BlockchainConnectivityException.............");
                failed = true;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _logger.LogError(ex, ex.Message);
                response = Error.Failure("BlockchainConnectivityException", ex.Message);
            }
            catch(AppException ex)
            {
                Console.WriteLine("AppException.............");
                failed = true;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _logger.LogError(ex, ex.Message);
                response = Error.Failure("AppException", ex.Message);

            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception....................");
                failed = true;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _logger.LogError(ex, ex.Message);
                response = Error.Failure("UnkNown", ex.Message);
            }
            finally
            {
                if (failed)
                {
                    context.Response.ContentType = "application/json";
                    var json = JsonSerializer.Serialize(response, options);
                    await context.Response.WriteAsync(json);

                }
            }
        }
    }
}



