namespace API.Model
{
    public class ItemDTO
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public decimal Price { get; set; }

        public string Location { get; set; } 

        public UserDto Owner { get; set; }

    }
}