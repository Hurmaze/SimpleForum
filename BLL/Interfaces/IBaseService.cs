using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBaseService<TModel> where TModel : class
    {
        Task<TModel> AddAsync(TModel model);
        Task<IEnumerable<TModel>> GetAllAsync();

        Task<TModel> GetByIdAsync(int id);

        Task UpdateAsync(TModel model);

        Task DeleteByIdAsync(int modelId);
    }
}
