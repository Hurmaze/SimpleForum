using AutoMapper;
using Services.Interfaces;
using Services.Models;
using Services.Validation;
using DAL.Entities.Forum;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Services.Validation.Exceptions;

namespace Services.Services
{
    /// <summary>
    /// Post service
    /// </summary>
    /// <seealso cref="IPostService" />
    public class PostService : IPostService
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<PostService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        public PostService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PostService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Adds TModel the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Task&lt;PostModel&gt;
        /// </returns>
        public async Task<PostModel> AddAsync(PostModel model)
        {
            var post = _mapper.Map<Post>(model);

            await _unitOfWork.PostRepository.AddAsync(post);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Added a new post by user with id {id} in thread id {thread}", post.AuthorId, post.ThreadId);

            var postView = _mapper.Map<PostModel>(post);
            return postView;
        }

        /// <summary>
        /// Deletes the Post by identifier asynchronous.
        /// </summary>
        /// <param name="modelId">The model identifier.</param>
        /// <exception cref="NotFoundException"></exception>
        public async Task DeleteByIdAsync(int modelId)
        {
            var entity = await _unitOfWork.PostRepository.DeleteByIdAsync(modelId);

            if(entity == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(Post).Name, "Id", modelId.ToString()));
            }

           await  _unitOfWork.SaveAsync();
            _logger.LogInformation("Post with an id {id} has been deleted.", modelId);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// Task&lt;IEnumerable&lt;PostModel&gt;&gt;.
        /// </returns>
        public async Task<IEnumerable<PostModel>> GetAllAsync()
        {
            var posts = await _unitOfWork.PostRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<PostModel>>(posts);
        }

        /// <summary>
        /// Gets the TModel by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Task&lt;PostModel&gt;
        /// </returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<PostModel> GetByIdAsync(int id)
        {
            var post = await _unitOfWork.PostRepository.GetByIdAsync(id);

            if (post == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(Post).Name, "Id", id.ToString()));
            }

            return _mapper.Map<PostModel>(post);
        }

        /// <summary>
        /// Gets the posts by user identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Task&lt;IEnumerable&lt;PostModel&gt;&gt;.
        /// </returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<IEnumerable<PostModel>> GetPostsByUserIdAsync(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(Post).Name, "Id", userId.ToString()));
            }

            return _mapper.Map<IEnumerable<PostModel>>(user.ThreadPosts);
        }

        /// <summary>
        /// Updates the PostModel asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <exception cref="NotFoundException"></exception>
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
