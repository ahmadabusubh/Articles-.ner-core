using Articles.Data;
using Articles.viewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Articles.Controllers
{
    [Authorize ]


    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(createRole model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id={id} cannot be found";
                return View("NotFound");
            }
            else
            {
                EditRoleViewModel modelVM = new EditRoleViewModel();
                modelVM.Id = id;
                modelVM.RoleName = role.Name;

                // Get users assigned to the role
                var usersInRole = await userManager.GetUsersInRoleAsync(role.Name);
                //var allUsers = await userManager.Users.ToListAsync();

              //  modelVM.users = allUsers.Select(u => u.UserName).ToList();

                return View(modelVM);
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id={model.Id} cannot be found";
                return View("NotFound");
            }
            else {

                role.Name = model.RoleName;
       var result     =  await  roleManager.UpdateAsync(role);
                if (result.Succeeded) {

                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);


            }

         }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId) {


             ViewBag.roleId= roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id={roleId} cannot be found";
                return View("NotFound");
            }
            var users = userManager.Users.ToList(); // Materialize the users into a list

            var Model = new List<UserRoleViewModel>();

            foreach (var user in users) {

                var UserRoleViewModel = new UserRoleViewModel()
                {

                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = await userManager.IsInRoleAsync(user, role.Name)

                    //  IsSelected = await userManager.IsInRoleAsync(user, role.Name)


                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {

                    UserRoleViewModel.IsSelected = true;


                }
                else
                {

                    UserRoleViewModel.IsSelected = false;

                }

                Model.Add(UserRoleViewModel);



            }
            return View(Model);

        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id={roleId} cannot be found";
                return View("NotFound");
            }

            for ( int i=0;i<model.Count;i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
               

                if (model[i].IsSelected)
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);


                }
                else if (await userManager.IsInRoleAsync(user, role.Name))
                {

                    result = await userManager.RemoveFromRoleAsync(user, role.Name);

                }
                else {

                    continue;
                
                
                }

                if (result.Errors.Count() > 0  || !result.Succeeded)
                {

                    foreach (var error in result.Errors)
                    {
                        // Log or display each error message
                        Console.WriteLine($"Error: {error.Code}, {error.Description}");
                    }

                    // Handle the error appropriately
                    ModelState.AddModelError("", "Failed to update user roles.");
                    return View("ErrorView"); 
                                    // Log the error or handle it appropriately
                                    ModelState.AddModelError("", "Failed to update user roles.");
                    return View("ErrorView"); // You should replace this with your error handling approach
                }

                //if (i == model.Count - 1)
                //{
                    // Return RedirectToAction only after processing all users
                  return RedirectToAction("EditRole", new { id = roleId });
                //}

            }

            return RedirectToAction("EditRole", new { id = roleId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id={roleId} cannot be found";
                return View("NotFound");
            }

            var model = new DeleteRoleViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoleConfirmed(string RoleId)
        {
            var role = await roleManager.FindByIdAsync(RoleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id={RoleId} cannot be found";
                return View("NotFound");
            }

            var result = await roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return RedirectToAction("ListRoles");
        }






    }
}
