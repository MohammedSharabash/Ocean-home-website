using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ocean_Home.Interfaces;
using Ocean_Home.Models.data;
using Ocean_Home.Models.Domain;

namespace Ocean_Home.Services
{
    public class CRUDSerivce<T> : ICRUD<T> where T:BaseModel
    {
        private readonly AppDbContext _context;
        public CRUDSerivce(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ToggleDelete(long Id)
        {
            var obj = _context.Set<T>().FirstOrDefault(x => x.Id == Id);
            obj.IsDeleted = !obj.IsDeleted;
            obj.DeletedOn = DateTime.Now.ToUniversalTime();
            return await Save();
        }

        public async Task<bool> Update(long Id)
        {
            var obj = _context.Set<T>().FirstOrDefault(x => x.Id == Id);
            obj.IsModified = true;
            obj.ModifiedOn = DateTime.Now.ToUniversalTime();
            return await Save();
        }
        private async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
