﻿using AspNetCoreTodo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync(ApplicationUser user);
        Task<bool> AddItemAsync(NewTodoItem newItem,ApplicationUser user);
        Task<bool> MarkDoneAsync(Guid id,ApplicationUser user);
    }
}
