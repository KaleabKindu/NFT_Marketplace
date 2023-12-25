using ErrorOr;

namespace Application.Common{
    public static class CommonError
    {
        public static Error ErrorSavingChanges => Error.Failure("Error while saving changes");

    }
}