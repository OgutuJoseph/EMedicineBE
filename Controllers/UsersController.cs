using EMedicineBE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace EMedicineBE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
			// used to fetch connection string
            _configuration = configuration;
        }

		// User Registration Controller
		[HttpPost]
		[Route("registration")]
		public Response register(Users users)
		{
			Response response = new Response();
			DAL dal = new DAL();
			SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
			response = dal.register(users, connection);

			return response;
		}

		// User Login Controller
		[HttpPost]
		[Route("login")]
		public Response login(Users users)
		{
			DAL dal	= new DAL();
			SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
			Response response = dal.login(users, connection);
			return response;
		}
    }
}
