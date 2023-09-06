using AutoMapper;
using Foxic.Buisness.Services.Interfaces;
using Foxic.Buisness.Services.Implementations;
using Foxic.Buisness.Services.Interfaces;
using Foxic.Buisness.ViewModels.AreasViewModels.ColorVM;
using Foxic.Core.Entities;
using Foxic.DataAccess.contexts;
using Foxic.DataAccess.contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Logging;
using Microsoft.EntityFrameworkCore;

namespace Foxic.UI.Areas.Admin.Controllers;

[Area("Admin")]
public class ColorController : Controller
{
	private readonly AppDbContext _context;
	private readonly IMapper _mapper;
	private readonly IWebHostEnvironment _webEnv;
	private readonly IFileService _fileService;
	public ColorController(AppDbContext context,
						   IMapper mapper,
						   IWebHostEnvironment webEnv,
						   IFileService fileService)
	{
		_context = context;
		_mapper = mapper;
		_webEnv = webEnv;
		_fileService = fileService;
	}
	public IActionResult Index(int Id)
	{
		Color color = _context.Colors.FirstOrDefault(x => x.Id == Id);
		List<Color> colors = _context.Colors.ToList();
		return View(colors);
	}
	public async Task<IActionResult> Details(int id)
        {
		Color? color = await _context.Colors.AsNoTracking().FirstOrDefaultAsync(s => s.Id== id);
            if (color == null)
            {
			return NotFound();
            }
		return View(color);

        }
	public IActionResult Create()
	{
		return View();
	}
	[HttpPost]
	[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ColorCreateVM color)
	{
		string filename = string.Empty;

		Color color1 = new()
		{
			Name = color.ColorName
		};
		filename = await _fileService.UploadFile(color.ImageUrl, _webEnv.WebRootPath, 300, "assets", "images", "slider");
		color1.Image = filename;
		await _context.Colors.AddAsync(color1);
		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}
    public async Task<IActionResult> Delete(int id)
    {
        Color? color = await _context.Colors.FindAsync(id);
        if (color == null) return NotFound();
        return View(color);
    }
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Color? color = await _context.Colors.FindAsync(id);
        if (color == null) return NotFound();
        _context.Colors.Remove(color);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

}
