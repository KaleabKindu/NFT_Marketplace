namespace Application.Features.Categories.Dtos
{
    public class CategoryDtO
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public List<ItemDTO> Items { get; set; } = new List<ItemDTO>();
    }

}
