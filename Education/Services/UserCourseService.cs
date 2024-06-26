using Ocean_Home.Interfaces;
using Ocean_Home.Models.data;
using Ocean_Home.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ocean_Home.Services
{
    public class UserProjectService : IUserProject
    {
        private readonly AppDbContext _context;
        public UserProjectService(AppDbContext context)
        {
            _context = context;
        }

        //public IEnumerable<UserProject> GetAllUserProject()
        //{
        //    return _context.UserProjects.Include(x => x.Project).Include(x => x.User).ToList();
        //}

        //public IEnumerable<UserProject> GetUserProject(Expression<Func<UserProject, bool>> expression)
        //{
        //    return _context.UserProjects.Where(expression).Include(x => x.Project).Include(x => x.User).ToList();
        //}
    }
}
