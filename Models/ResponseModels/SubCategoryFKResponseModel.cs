namespace EkartAPI.Models.ResponseModels
{
    public class SubCategoryFKResponseModel
    {
        public List<SubCategoryFK> data { get; set; }
        public int total { get; set; }
    }

    public class SubCategoryFK
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int ParentCategoryId { get; set; }

    }
}
