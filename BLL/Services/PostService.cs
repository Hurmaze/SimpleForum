using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Entities.Forum;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PostService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PostModel> AddAsync(PostModel model)
        {
            var post = _mapper.Map<Post>(model);

            await _unitOfWork.PostRepository.AddAsync(post);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Added a new post by user {email} in thread id {thread}", post.Author.Email, post.Thread.Id);

            var postView = _mapper.Map<PostModel>(post);
            return postView;
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            var entity = await _unitOfWork.PostRepository.DeleteByIdAsync(modelId);

            if(entity == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(Post).Name, "Id", modelId.ToString()));
            }

            _logger.LogInformation("Post with an id {id} has been deleted.", modelId);
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
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(Post).Name, "Id", userId.ToString()));
            }

            return _mapper.Map<IEnumerable<PostModel>>(user.ThreadPosts);
        }

        public async Task UpdateAsync(PostModel model)
        {
            var post = await _unitOfWork.PostRepository.GetByIdAsync(model.Id);

            if (post == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(Post).Name, "Id", model.Id.ToString())); 
            }

            post = _mapper.Map(model, post);

            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Post with id {id} has been updated.", post.Id);
        }
    }
}
