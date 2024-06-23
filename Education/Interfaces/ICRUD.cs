using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ocean_Home.Models.Domain;

namespace Ocean_Home.Interfaces
{
    public interface ICRUD<T> where T: BaseModel
    {
        Task<bool> ToggleDelete(long Id);
        Task<bool> Update(long Id);
    }
}
