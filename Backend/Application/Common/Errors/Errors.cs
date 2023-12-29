using ErrorOr;

namespace Application.Common.Errors{
    
    public static class ErrorFactory
    {
        // Read
        public static Error NotFound(string entityName) => Error.NotFound($"{entityName}.NotFound", $"{entityName} not found");

        // Create
        public static Error DuplicateCode(string entityName) => Error.Conflict($"{entityName}.DuplicateCode", $"Duplicate code for {entityName}");

        // Validation errors for Create/Update
        public static Error ValidationFailed(string entityName, string errorMessage) => Error.Validation($"{entityName}.ValidationFailed", $"{entityName} validation failed: {errorMessage}");

        // Authorization error
        public static Error AuthorizationError(string entityName) => Error.Unauthorized($"{entityName}.AuthorizationError", $"Not authorized to perform this operation on {entityName}");

        // Not supported error
        public static Error NotSupportedError(string entityName) => Error.Unexpected($"{entityName}.NotSupported", $"{entityName} operation not supported");

        // Bad request error
        public static Error BadRequestError(string entityName, string errorMessage) => Error.Failure($"{entityName}.BadRequest", $"Bad request for {entityName}: {errorMessage}");
    }
}