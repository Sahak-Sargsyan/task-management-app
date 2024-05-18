using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskBLL.Interfaces;
using TaskBLL.Models;

namespace TaskPL.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITaskService _taskService;
        public IEnumerable<TaskModel> Tasks { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
            Tasks = new List<TaskModel>();
        }

        public bool isUserLoggedIn;

        public async Task OnGetAsync()
        {
            try
            {
                int? userId = HttpContext.Session.GetInt32("UserId");
                if(userId != null)
                {
                    Tasks = await _taskService.GetAllByUserId(userId.Value);
                    isUserLoggedIn = true;
                }
            }
            catch (Exception ex)
            {
                _logger.BeginScope(ex.Message);
            }
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear(); // Clears the session
            return RedirectToPage("/Login"); // Redirects to the login page
        }

        public async Task<IActionResult> OnPostDeleteAsync(int taskId)
        {
            await _taskService.DeleteAsync(taskId);
            return RedirectToPage();  // Refresh the page to show updated task list
        }
    }
}
