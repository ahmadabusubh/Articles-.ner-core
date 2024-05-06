using Articles.viewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Articles.Controllers
{
    public class AdministrationController1 : Controller
    {
        private readonly RoleManager<IdentityRole> roleManage;
        private readonly UserManager<IdentityUser> manager;

        public AdministrationController1(RoleManager<IdentityRole> roleManage,  UserManager<IdentityUser> manager)
        {
            this.roleManage = roleManage;
            this.manager = manager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(createRole model)
        {
            if (ModelState.IsValid) {
                IdentityRole identityRole = new IdentityRole() {
                    Name = model.RoleName
                
                
                };

                IdentityResult result = await roleManage.CreateAsync(identityRole);
                if (result.Succeeded) {

                    return RedirectToAction("ListRoles", "Administration");
                }
                foreach (IdentityError error in result.Errors) {
                    ModelState.AddModelError("",error.Description); 
                
                }

            }
            return View(model);


        }
        [HttpGet]
        public IActionResult ListRoles() {
            var roles = roleManage.Roles;
            return View(roles);
        
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManage.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id={id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in manager.Users)
            {

                if (await manager.IsInRoleAsync(user, role.Name))
                {

                    model.users.Add(user.UserName);
                }

            }
            return View(model);
        }
    }
}
