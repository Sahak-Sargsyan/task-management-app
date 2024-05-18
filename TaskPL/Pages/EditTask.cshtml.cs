using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskBLL.Interfaces;
using TaskBLL.Models;

namespace TaskPL.Pages
{
    public class EditTaskModel : PageModel
    {
        private readonly ITaskService _taskService;
        [BindProperty]
        public TaskModel Task { get; set; }

        public EditTaskModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Task = await _taskService.GetByIdAsync(id);
            if (Task == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Task.UserId = HttpContext.Session.GetInt32("UserId");

            await _taskService.UpdateAsync(Task);
            return RedirectToPage("./Index");
        }
    }
}
