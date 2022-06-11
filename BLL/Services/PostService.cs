using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Entities.Forum;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserAccountService> _logger;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserAccountService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public Task AddAsync(PostModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PostModel>> GetAllAsync()
        {
            var posts = await _unitOfWork.PostRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<PostModel>>(posts);
        }

        public async Task<PostModel> GetByIdAsync(int id)
        {
            var post = await _unitOfWork.PostRepository.GetByIdAsync(id);

            return _mapper.Map<PostModel>(post);
        }

        public async Task<IEnumerable<PostModel>> GetPostsByUserIdAsync(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user == null)
            {
                _logger.LogWarning(String.Format(ExceptionMessages.NotFound, typeof(User), "Id", userId.ToString()));
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User), "Id", userId.ToString()));
            }

            return _mapper.Map<IEnumerable<PostModel>>(user.ThreadPosts);
        }

        public Task UpdateAsync(PostModel model)
        {
            throw new NotImplementedException();
        }
    }
}
