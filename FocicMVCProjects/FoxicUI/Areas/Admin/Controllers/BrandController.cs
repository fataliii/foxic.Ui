using AutoMapper;
using Foxic.Buisness.Services.Interfaces;
using Foxic.Buisness.ViewModels.AreasViewModels.BrandVM;
using Foxic.Core.Entities;
using Foxic.DataAccess.contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoxicUI.Areas.Admin.Controllers;
[Area("Admin")]

public class BrandController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webEnv;
    private readonly IFileService _fileservice;
    public BrandController(AppDbContext context,
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
        Brand? brand = await _context.Brands.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        if (brand == null) return NotFound();
        return View(brand);
    }
    public IActionResult Index(int Id)
    {
        Brand brand = _context.Brands.FirstOrDefault(x => x.Id == Id);
        List<Brand> brands = _context.Brands.ToList();
        return View(brands);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BrandPostVM brand)
    {
        //return Json(brand);
        string filename = string.Empty;

        Brand brand1 = new()
        {
            BrandName = brand.Name
        };
        filename = await _fileservice.UploadFile(brand.ImageUrl, _webEnv.WebRootPath, 300, "assets", "images", "slider");
        brand1.Image = filename;
        await _context.Brands.AddAsync(brand1);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        Brand? brand = await _context.Brands.FindAsync(id);
        if (brand == null) return NotFound();
        return View(brand);
    }
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Brand?  brand = await _context.Brands.FindAsync(id);
        if (brand == null) return NotFound();
        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
}
