using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;

namespace MVC.Controllers
{
    public class PlayerController : MvcController
    {
        // Service injections:
        private readonly IPlayerService _playerService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public PlayerController(
			IPlayerService playerService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _playerService = playerService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Player
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _playerService.Query().ToList();
            return View(list);
        }

        // GET: Player/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _playerService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Player/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PlayerModel player)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _playerService.Create(player.Record);
                if (result.IsSuccess)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = player.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(player);
        }

        // GET: Player/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _playerService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Player/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PlayerModel player)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _playerService.Update(player.Record);
                if (result.IsSuccess)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = player.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(player);
        }

        // GET: Player/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _playerService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Player/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _playerService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
