using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskBLL.Interfaces;  // Assuming you have a service layer
using TaskBLL.Models;

namespace TaskPL.Pages
{
    public class AddTaskModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<AddTaskModel> _logger;
        public List<SelectListItem> Categories { get; private set; }
        public bool ShowNewCategoryInput { get; set; } = false;

        public AddTaskModel(ITaskService taskService, ILogger<AddTaskModel> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [BindProperty]
        public TaskModel Task { get; set; }

        public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                // If UserId is not in session, redirect to Login page
                return RedirectToPage("/Login");
            }

            // Continue with any other logic you might need here
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                // If UserId is not in session, redirect to Login page
                return RedirectToPage("/Login");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Task.UserId = HttpContext.Session.GetInt32("UserId");
            if (!Task.UserId.HasValue)
            {
                ModelState.AddModelError("", "User session is not set.");
                return Page();
            }

            await _taskService.AddAsync(Task);

            return RedirectToPage("./Index");  // Redirect to the task list page
        }
    }
}
