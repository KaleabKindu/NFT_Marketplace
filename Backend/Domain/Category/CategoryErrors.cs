using ErrorOr;

namespace Domain.Category
{

    public static class CategoryError
    {
        public static Error NotFound => Error.NotFound("Category.NotFound", "Category not found");

        public static Error DuplicateCode => Error.Conflict("Activity.DuplicateCode", "Duplicate code");
    }
}