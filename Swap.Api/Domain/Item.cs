namespace Swap.Api.Models
{
	public class Item
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int UserId { get; set; }
		public ApplicationUser User { get; set; }


	}
}
