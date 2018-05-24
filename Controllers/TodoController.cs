using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTodo.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoController(ITodoItemService todoItemService, UserManager<ApplicationUser> userManager)
        {
            _todoItemService = todoItemService;
            _userManager = userManager;
        }
       
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) {
                return Challenge();
            }
            // 从数据库获取 to-do 条目
            var todoItems = await _todoItemService.GetIncompleteItemsAsync(user);
            // 把条目置于 model 中
            var model = new TodoViewModel()
            {
                Items = todoItems
            };
            // 使用 model 渲染视图
            return View(model);

        }

        public async Task<ActionResult> AddItem(NewTodoItem newItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var successful = await _todoItemService.AddItemAsync(newItem,user);
            if (!successful)
            {
                return BadRequest(new { error = "Could not add item" });
            }

            return Ok();
        }

        public async Task<ActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty) {
                return BadRequest();
            }
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Unauthorized();
            var successful = await _todoItemService.MarkDoneAsync(id, currentUser);
            if (!successful) return BadRequest();

            return Ok();
        }
    }
}