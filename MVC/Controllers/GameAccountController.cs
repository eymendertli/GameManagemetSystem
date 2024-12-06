using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;

namespace MVC.Controllers
{
    public class GameAccountController : MvcController
    {
        // Service injections:
        private readonly IGameAccountService _gameAccountService;
        private readonly IPlayerService _playerService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public GameAccountController(
			IGameAccountService gameAccountService
            , IPlayerService playerService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _gameAccountService = gameAccountService;
            _playerService = playerService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: GameAccount
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _gameAccountService.Query().ToList();
            return View(list);
        }

        // GET: GameAccount/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _gameAccountService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["PlayerId"] = new SelectList(_playerService.Query().ToList(), "Record.Id", "UserName");
            
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: GameAccount/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: GameAccount/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GameAccountModel gameAccount)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _gameAccountService.Create(gameAccount.Record);
                if (result.IsSuccess)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = gameAccount.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(gameAccount);
        }

        // GET: GameAccount/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _gameAccountService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: GameAccount/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GameAccountModel gameAccount)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _gameAccountService.Update(gameAccount.Record);
                if (result.IsSuccess)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = gameAccount.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(gameAccount);
        }

        // GET: GameAccount/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _gameAccountService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: GameAccount/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _gameAccountService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
