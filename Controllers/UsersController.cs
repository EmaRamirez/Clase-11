using System.Diagnostics.CodeAnalysis;
using Clase_11.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clase_11.Controllers;

public class UsersController : Controller
{

    private readonly ILogger<UsersController> _logger;

    private readonly UserManager<IdentityUser> _usermanager;
    private readonly RoleManager<IdentityRole> _rolesmanager;
    public UsersController(ILogger<UsersController> logger,
    UserManager<IdentityUser> usermanager,
    RoleManager<IdentityRole> rolesmanager)
    {
        this._logger = logger;
        this._usermanager = usermanager;
        this._rolesmanager = rolesmanager;
    }

    public IActionResult Index()
    {
        var users = _usermanager.Users.ToList();
        //Listar todos los usuarios
        return View(users);
    }

    public async Task<IActionResult> Edit(string userId)
    {
        var user = await _usermanager.FindByIdAsync(userId);

        var model = new UserEditViewModel();
        model.UserName = user.UserName ?? string.Empty;
        model.Email = user.Email ?? string.Empty;
        model.Roles = new SelectList(_rolesmanager.Roles.ToList());
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UserEditViewModel obj)
    {
        var user = await _usermanager.FindByNameAsync(obj.UserName);
        if (user != null)
        {
            await _usermanager.AddToRoleAsync(user, obj.Role);
        }


        return RedirectToAction("Index");
    }


}