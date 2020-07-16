namespace Swap.Api.Models
{
	public class Exchange
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int UserOfferId { get; set; }
		public int ItemId { get; set; }
		public int ItemOfferId { get; set; }
		public bool IsAccept { get; set; }
		public ApplicationUser User{ get; set; }
		public ApplicationUser UserOffer { get; set; }
		public Item Item { get; set; }
		public Item ItemOffer { get; set; }

	}
}
