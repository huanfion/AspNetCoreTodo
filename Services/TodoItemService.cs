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

        public async Task<bool> AddItemAsync(NewTodoItem newItem,ApplicationUser user)
        {
            var entity = new TodoItem
            {
                Id = Guid.NewGuid(),
                IsDone = false,
                Title = newItem.Title,
                DueAt = DateTimeOffset.Now.AddDays(3),
                OwnerId=user.Id
            };

            _context.Items.Add(entity);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync(ApplicationUser user)
        {
            var items = await _context.Items
                .Where(a => a.IsDone == false&&a.OwnerId == user.Id).ToArrayAsync();
            return items;
        }

        public async Task<bool> MarkDoneAsync(Guid id,ApplicationUser user)
        {
            var item = await _context.Items.Where(a => a.Id == id&&a.OwnerId==user.Id).SingleOrDefaultAsync();
            if (item == null)
            {
                return false;
            }
            else
            {
                item.IsDone = true;
            }
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
