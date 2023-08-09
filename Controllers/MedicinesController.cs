using EMedicineBE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace EMedicineBE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MedicinesController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public MedicinesController(IConfiguration configuration)
		{
			// used to fetch connection string
			_configuration = configuration;
		}

		// Add To Cart Controller
		[HttpPost]
		[Route("addToCart")]
		public Response addToCart(Cart cart)
		{
			DAL dal = new DAL();
			SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
			Response response = dal.addToCart(cart, connection);
			return response;
		}
	}
}
