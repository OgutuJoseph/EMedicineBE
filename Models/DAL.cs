using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;

namespace EMedicineBE.Models
{
	public class DAL
	{
		// User - Registration DAL
		public Response register(Users users, SqlConnection connection)
		{
			Response response = new Response();
			SqlCommand cmd = new SqlCommand("sp_register", connection);

			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
			cmd.Parameters.AddWithValue("@LastName", users.LastName);
			cmd.Parameters.AddWithValue("@Password", users.Password);
			cmd.Parameters.AddWithValue("@Email", users.Email);
			cmd.Parameters.AddWithValue("@Fund", 0);
			cmd.Parameters.AddWithValue("@Type", "Users");
			cmd.Parameters.AddWithValue("@Status", "Pending");

			connection.Open();
			int i = cmd.ExecuteNonQuery();
			connection.Close();

			if(i > 0)
			{
				response.StatusCode = 200;
				response.StatusMessage = "User registered successfully.";
			}
			else
			{
				response.StatusCode = 100;
				response.StatusMessage = "User registration failed";
			}

			return response;
		}

		// User - Login DAL
		public Response login(Users users, SqlConnection connection)
		{
			SqlDataAdapter da = new SqlDataAdapter("sp_login", connection);

			da.SelectCommand.CommandType = CommandType.StoredProcedure;
			da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
			da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);

			DataTable dt = new DataTable();
			da.Fill(dt);
			Response response = new Response();
			Users user = new Users(); 

			if(dt.Rows.Count > 0)
			{
				user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
				user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
				user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
				user.Email = Convert.ToString(dt.Rows[0]["Email"]);
				user.Type = Convert.ToString(dt.Rows[0]["Type"]);
				response.StatusCode = 200;
				response.StatusMessage = "User is valid.";
				response.user = user;
			}
			else
			{
				response.StatusCode = 100;
				response.StatusMessage = "User is invalid.";
				response.user = null;
			}

			return response;
		}

		// User - View User Info DAL
		public Response viewUser(Users users, SqlConnection connection)
		{
			SqlDataAdapter da = new SqlDataAdapter("p_viewUser", connection);
			da.SelectCommand.CommandType = CommandType.StoredProcedure;
			da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);

			DataTable dt = new DataTable();
			da.Fill(dt);
			Response response = new Response();
			Users user = new Users();

			if(dt.Rows.Count > 0)
			{
				user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
				user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
				user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
				user.Email = Convert.ToString(dt.Rows[0]["Email"]);
				user.Type = Convert.ToString(dt.Rows[0]["Type"]);
				user.Fund = Convert.ToDecimal(dt.Rows[0]["Fund"]);
				user.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
				user.Password = Convert.ToString(dt.Rows[0]["Password"]);
				response.StatusCode = 200;
				response.StatusMessage = "User exists.";
				response.user = user;
			}
			else
			{
				response.StatusCode = 100;
				response.StatusMessage = "User does not exist.";
				response.user = null;
			}

			return response;
		}

		// User - Update User Info DAL
		public Response updateProfile(Users users, SqlConnection connection)
		{
			Response response = new Response();
			SqlCommand cmd = new SqlCommand("sp_updateprofile", connection);

			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
			cmd.Parameters.AddWithValue("@LastName", users.LastName);
			cmd.Parameters.AddWithValue("@Password", users.Password);
			cmd.Parameters.AddWithValue("@Email", users.Email);

			connection.Open();
			int i = cmd.ExecuteNonQuery();
			connection.Close();

			if(i > 0)
			{
				response.StatusCode = 200;
				response.StatusMessage = "Record updated successfully.";
			}
            else
            {
                response.StatusCode = 100;
				response.StatusMessage = "Some error occured. Try after some time.";
            }

            return response;
		}

		// User - Add to Cart DAL
		public Response addToCart(Cart cart, SqlConnection connection)
		{
			Response response = new Response();
			SqlCommand cmd = new SqlCommand("sp_AddToCart", connection);

			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@UserId", cart.UserId);
			cmd.Parameters.AddWithValue("@MedicineId", cart.MedicineId);
			cmd.Parameters.AddWithValue("@UnitPrice", cart.UnitPrice);
			cmd.Parameters.AddWithValue("@Discount", cart.Discount);
			cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
			cmd.Parameters.AddWithValue("@TotalPrice", cart.TotalPrice); 

			connection.Open();
			int i = cmd.ExecuteNonQuery();
			connection.Close();

			if(i > 0 )
			{
				response.StatusCode = 200;
				response.StatusMessage = "Item added successfully.";
			}
			else
			{
				response.StatusCode = 100;
				response.StatusMessage = "Failed to add item.";
			}

			return response;
		}

		// User - Place Order DAL
		public Response placeOrder(Users users, SqlConnection connection)
		{
			Response response = new Response();
			SqlCommand cmd = new SqlCommand("sp_PlaceOrder", connection);

			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@ID", users.ID);

			connection.Open() ;
			int i = cmd.ExecuteNonQuery();
			connection.Close();

			if (i > 0)
			{
				response.StatusCode = 200;
				response.StatusMessage = "Order placed successfully.";
			}
			else
			{
				response.StatusCode = 100;
				response.StatusMessage = "Failed to place order.";
			}

			return response;
		}

		// User (Admin) - View Orders DAL
		public Response orderList(Users users, SqlConnection connection)
		{
			Response response = new Response();
			List<Orders> listOrder = new List<Orders>();
			SqlDataAdapter da = new SqlDataAdapter("sp_OrderList", connection);

			da.SelectCommand.CommandType = CommandType.StoredProcedure;
			da.SelectCommand.Parameters.AddWithValue("@Type", users.Type);
			da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);
			DataTable dt = new DataTable();
			da.Fill(dt);

			if (dt.Rows.Count > 0)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					Orders order = new Orders();
					order.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
					order.OrderNo = Convert.ToString(dt.Rows[i]["OrderNo"]);
					order.OrderTotal = Convert.ToDecimal(dt.Rows[i]["OrderTotal"]);
					order.OrderStatus = Convert.ToString(dt.Rows[i]["OrderStatus"]);
					listOrder.Add(order);
				}
				if (listOrder.Count > 0)
				{
					response.StatusCode = 200;
					response.StatusMessage = "Order details fetched successfully.";
					response.listOrders = listOrder;
				}
				else
				{
					response.StatusCode = 100;
					response.StatusMessage = "Failed to fetch Order details.";
					response.listOrders = null;
				}
			}
			else
			{
				response.StatusCode = 100;
				response.StatusMessage = "Order details not avaialble.";
				response.listOrders = null;
			}

			return response;
		}


		// Admin - Add / Update Medicines DAL
		public Response addUpdateMedicine(Medicines medicines, SqlConnection connection)
		{
			Response response = new Response();
			SqlCommand cmd = new SqlCommand("sp_AddUpdateMedicine", connection);

			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Name", medicines.Name);
			cmd.Parameters.AddWithValue("@Manufacturer", medicines.Manufacturer);
			cmd.Parameters.AddWithValue("@UnitPrice", medicines.UnitPrice);
			cmd.Parameters.AddWithValue("@Discount", medicines.Discount);
			cmd.Parameters.AddWithValue("@Quantity", medicines.Quantity);
			cmd.Parameters.AddWithValue("@ExpDate", medicines.ExpDate);
			cmd.Parameters.AddWithValue("@ImageURL", medicines.ImageURL);
			cmd.Parameters.AddWithValue("@Status", medicines.Status);
			cmd.Parameters.AddWithValue("@Type", medicines.Type);

			connection.Open();
			int i = cmd.ExecuteNonQuery();
			connection.Close();

			if (i > 0)
			{
				response.StatusCode = 200;
				response.StatusMessage = "Medicine added successfully.";
			}
			else
			{
				response.StatusCode = 100;
				response.StatusMessage = "Failed to add medicine";
			}

			return response;
		}

		// Admin - View All Users Info DAL
		public Response userList(SqlConnection connection)
		{
			Response response = new Response();
			List<Users> listUsers = new List<Users>();
			SqlDataAdapter da = new SqlDataAdapter("sp_UserList", connection);

			da.SelectCommand.CommandType = CommandType.StoredProcedure;
			DataTable dt = new DataTable();
			da.Fill(dt);

			if (dt.Rows.Count > 0)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					Users user = new Users();
					user.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
					user.FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
					user.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
					user.Password = Convert.ToString(dt.Rows[i]["Password"]);
					user.Email = Convert.ToString(dt.Rows[i]["Email"]);
					user.Fund = Convert.ToDecimal(dt.Rows[i]["Fund"]);
					user.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
					user.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
					listUsers.Add(user);
				}
				if (listUsers.Count > 0)
				{
					response.StatusCode = 200;
					response.StatusMessage = "Users fetched successfully.";
					response.listUsers = listUsers;
				}
				else
				{
					response.StatusCode = 100;
					response.StatusMessage = "Failed to fetch Users.";
					response.listUsers = null;
				}
			}
			else
			{
				response.StatusCode = 100;
				response.StatusMessage = "Users not avaialble.";
				response.listOrders = null;
			}

			return response;
		}
	}
}
