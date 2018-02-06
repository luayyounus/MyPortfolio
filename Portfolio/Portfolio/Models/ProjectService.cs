using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portfolio.Data;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Models
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectDbContext _context;
        public ProjectService(ProjectDbContext context)
        {
            context.EnsureSeedData();
            _context = context;
        }
        public async Task DeleteAsync(int id)
        {
            Project existingProject = _context.Projects.FirstOrDefault(p => p.Id == id);
            if(existingProject != null)
            {
                _context.Projects.Remove(existingProject);
                await _context.SaveChangesAsync();
            }
        }

        public Project FindProject(int id) => _context.Projects.FirstOrDefault(x => x.Id == id);

        public Task<Project> FindAsync(int id) => _context.Projects.FirstOrDefaultAsync(x => x.Id == id);

        public IQueryable<Project> GetAll(int? count = null, int? page = null)
        {
            var actualCount = count.GetValueOrDefault(10);
            return _context.Projects
                    .Skip(actualCount * page.GetValueOrDefault(0))
                    .Take(actualCount);
        }

        public Task<Project[]> GetAllAsync(int? count = null, int? page = null) => GetAll(count, page).ToArrayAsync();
        
        public async Task SaveAsync(Project project)
        {
            var doesItExist = project.Id == default(int);

            _context.Entry(project).State = doesItExist ? EntityState.Added : EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
