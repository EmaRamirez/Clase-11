using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clase_11.Controllers;

public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _rolManager;
    public RolesController(RoleManager<IdentityRole> rolManager)
    {
        this._rolManager = rolManager;
    }


    public IActionResult Index()
    {

        var roles = _rolManager.Roles.ToList();
        return View(roles);
    }

    public IActionResult Create()
    {
        return View();

    }

    [HttpPost]
    public IActionResult Create(IdentityRole role)
    {
        if (string.IsNullOrEmpty(role.Name))
        {
            return View();
        }
        _rolManager.CreateAsync(role);
        return RedirectToAction("Index");
    }

}