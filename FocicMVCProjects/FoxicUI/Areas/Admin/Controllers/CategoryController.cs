using AutoMapper;
using Foxic.Buisness.Services.Interfaces;
using Foxic.Buisness.ViewModels.AreasViewModels.CategoryVM;
using Foxic.Core.Entities;
using Foxic.DataAccess.contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoxicUI.Areas.Admin.Controllers;
[Area("Admin")]

public class CategoryController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webEnv;
    private readonly IFileService _fileservice;
    public CategoryController(AppDbContext context,
                            IMapper mapper,
                            IWebHostEnvironment webEnv,
                            IFileService fileservice)
    {
        _context = context;
        _webEnv = webEnv;
        _fileservice = fileservice;
    }
    public async Task<IActionResult> Details(int id)
    {
        Category? category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        if (category == null) return NotFound();
        return View(category);
    }
    public IActionResult Index(int Id)
    {
        Category category = _context.Categories.FirstOrDefault(x => x.Id == Id);
        List<Category> categories = _context.Categories.ToList();
        return View(categories);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryCreateVM category)
    {
        //return Json(brand);
        string filename = string.Empty;

        Category category1 = new()
        {
            CategoryName = category.CategoryName
        };
        await _context.Categories.AddAsync(category1);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
