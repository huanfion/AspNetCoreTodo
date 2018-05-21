﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;
        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync()
        {
            var items = await _context.Items
                .Where(a => a.IsDone == false).ToArrayAsync();
            return items;
        }
    }
}
