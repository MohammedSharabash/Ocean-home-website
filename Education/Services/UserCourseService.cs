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
    public class UserCourseService : IUserCourse
    {
        private readonly AppDbContext _context;
        public UserCourseService(AppDbContext context)
        {
            _context = context;
        }

        //public IEnumerable<UserCourse> GetAllUserCourse()
        //{
        //    return _context.UserCourses.Include(x => x.Course).Include(x => x.User).ToList();
        //}

        //public IEnumerable<UserCourse> GetUserCourse(Expression<Func<UserCourse, bool>> expression)
        //{
        //    return _context.UserCourses.Where(expression).Include(x => x.Course).Include(x => x.User).ToList();
        //}
    }
}
