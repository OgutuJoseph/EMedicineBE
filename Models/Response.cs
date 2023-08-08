using System.Collections.Generic;

namespace EMedicineBE.Models
{
	public class Response
	{
		public int StatusCode { get; set; }
		public string StatusMessage { get; set; }

		// Users
		public List<Users> listUsers { get; set; }
		public Users user { get; set; }

		// Medicines
		public List<Medicines> listMedicines { get; set; }
		public Medicines medicine { get; set; }

		// Cart
		public List<Cart> listCart { get; set; }

		// Orders
		public List<Orders> listOrders { get; set; }
		public Orders order { get; set; }

		// Order Items
		public List <OrderItems> listItems { get; set; }
		public OrderItems orderItem { get; set; }

	}
}
