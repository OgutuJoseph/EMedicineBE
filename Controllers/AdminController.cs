using EMedicineBE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace EMedicineBE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public AdminController(IConfiguration configuration)
		{
			// used to fetch connection string
			_configuration = configuration;
		}

		// Add Medicine Controller
		[HttpPost]
		[Route("addUpdateMedicine")]
		public Response addUpdateMedicine(Medicines medicines)
		{
			DAL dal = new DAL();
			SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
			Response response = dal.addUpdateMedicine(medicines, connection);
			return response;	
		}

		// Get All Users Controller
		[HttpGet]
		[Route("userList")]
		public Response userList()
		{
			DAL dal = new DAL();
			SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
			Response response = dal.userList(connection);
			return response;
		}
	}
}
