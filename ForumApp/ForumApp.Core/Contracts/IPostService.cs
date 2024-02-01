using ForumApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Core.Contracts
{
    public interface IPostService
    {
        Task AddPostAsync(PostModel model);
        Task EditPostAsync(PostModel model);

        Task<PostModel?> GetPostById(int id);
        Task<IEnumerable<PostModel>> GetAllPostsAsync();
        Task DeletePostAsync(int id);
    }
}
