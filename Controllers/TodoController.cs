using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTodo.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;

        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }
        public async Task<IActionResult> Index()
        {
            // 从数据库获取 to-do 条目
            var todoItems = await _todoItemService.GetIncompleteItemsAsync();
            // 把条目置于 model 中
            var model = new TodoViewModel()
            {
                Items = todoItems
            };
            // 使用 model 渲染视图
            return View(model);

        }
    }
}