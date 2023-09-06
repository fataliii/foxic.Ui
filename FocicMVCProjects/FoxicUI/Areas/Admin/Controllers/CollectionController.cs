using System.Collections.ObjectModel;
using AutoMapper;
using Foxic.Buisness.Services.Interfaces;
using Foxic.Buisness.ViewModels.AreasViewModels.CollectionVM;
using Foxic.Core.Entities;
using Foxic.DataAccess.contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FoxicUI.Areas.Admin.Controllers;
[Area("Admin")]
    public class CollectionController : Controller
    {
	private readonly AppDbContext _context;
	private readonly IMapper _mapper;
	private readonly IWebHostEnvironment _webEnv;
	private readonly IFileService _fileService;
	public CollectionController(AppDbContext context,
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
		Collection? collection = _context.Collections.FirstOrDefault(x => x.Id == Id);
		List<Collection> collections = _context.Collections.ToList();
		return View(collections);
	}
	public async Task<IActionResult> Details(int id)
	{
		Collection? collection = await _context.Collections.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
		if (collection == null)
		{
			return NotFound();
		}
		return View(collection);

	}
	public IActionResult Create()
	{
		return View();
	}
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(CollectionCreateVM collection)
	{
		string filename = string.Empty;

		Collection collection1 = new()
		{
			CollectionName = collection.CollectionName
		};
		filename = await _fileService.UploadFile(collection.ImageUrl, _webEnv.WebRootPath, 300, "assets", "images", "slider");
		collection1.Image = filename;
        await _context.Collections.AddAsync(collection1);
		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}
    public async Task<IActionResult> Delete(int id)
    {
        Collection? collection = await _context.Collections.FindAsync(id);
        if (collection == null) return NotFound();
        return View(collection);
    }
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Collection? collection = await _context.Collections.FindAsync(id);
        if (collection == null) return NotFound();
        _context.Collections.Remove(collection);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
}
