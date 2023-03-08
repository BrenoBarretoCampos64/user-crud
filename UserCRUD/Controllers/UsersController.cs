using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserCRUD.Data;
using UserCRUD.Models.Domain;
using UserCRUD.Models.Utility;

namespace UserCRUD.Controllers
{
    public class UsersController : Controller
	{
		private readonly UserDbContext _userDbContext;

        public UsersController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public UserDbContext UserDbContext { get; }

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var users = await _userDbContext.Users.ToListAsync();
			return View(users);
		}

        [HttpGet]
		public IActionResult Add()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Add(AddUserViewModel addUserRequest)
        {
            User user = new User()
            {
                Username = addUserRequest.Username,
                Email = addUserRequest.Email,
                Role = addUserRequest.Role,
                BirthDate = addUserRequest.BirthDate
            };

            await _userDbContext.Users.AddAsync(user);
            await _userDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var user = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

			if (user == null)
			{
				return RedirectToAction("Index");
			}

			var updatedUser = new UpdateUserViewModel()
			{
				Username = user.Username,
				Email = user.Email,
				Role = user.Role,
				BirthDate = user.BirthDate
			};

			return View(updatedUser);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(UpdateUserViewModel updatedUserData)
		{
            var user = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == updatedUserData.Id);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            user.Id = updatedUserData.Id;
			user.Username = updatedUserData.Username;
			user.Email = updatedUserData.Email;
			user.Role = updatedUserData.Role;
			user.BirthDate = updatedUserData.BirthDate;

			await _userDbContext.SaveChangesAsync();

			return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateUserViewModel userToBeDeleted)
        {
            var user = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == userToBeDeleted.Id);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            _userDbContext.Users.Remove(user);
            await _userDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

