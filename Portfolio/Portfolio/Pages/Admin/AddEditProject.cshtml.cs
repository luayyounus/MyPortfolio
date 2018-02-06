using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;

namespace Portfolio.Pages.Admin
{
    [Authorize]
    public class AddEditProjectModel : PageModel
    {
        [FromRoute]
        public int? Id { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        [BindProperty]
        public Project Project { get; set; }

        private readonly IProjectService ProjectService;

        public AddEditProjectModel(IProjectService ps)
        {
            ProjectService = ps;
        }

        public async Task OnGet()
        {
            Project = await ProjectService.FindAsync(Id.GetValueOrDefault()) ?? new Project();
        }

        public async Task<IActionResult> OnPost()
        {
            var project = await ProjectService.FindAsync(Id.GetValueOrDefault()) ?? new Project();
            project.Name = Project.Name;
            project.Description = Project.Description;
            project.Type = Project.Type;
            project.DateCompleted = Project.DateCompleted;
            project.Technologies = Project.Technologies;
            project.SetImage(Image);

            await ProjectService.SaveAsync(project);

            return RedirectToPage("/project", new { Id = project.Id });
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await ProjectService.DeleteAsync(Id.GetValueOrDefault());
            return RedirectToPage("/index");
        }
    }
}