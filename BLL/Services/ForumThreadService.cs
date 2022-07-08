using AutoMapper;
using Services.Interfaces;
using Services.Models;
using Services.Validation;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Services.Validation.Exceptions;
using DAL.Entities;

namespace Services.Services
{
    /// <summary>
    /// ForumThread service
    /// </summary>
    /// <seealso cref="IForumThreadService" />
    public class ForumThreadService : IForumThreadService
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
        private readonly ILogger<ForumThreadService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ForumThreadService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        public ForumThreadService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ForumThreadService> logger)
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
        /// Task&lt;ForumThreadModel&gt;
        /// </returns>
        public async Task<ForumThreadModel> AddAsync(ForumThreadRequest model)
        {
            var forumThread = _mapper.Map<ForumThread>(model);

            if (forumThread.ThemeId == 0)
            {
                forumThread.ThemeId = null;
            }
            if (forumThread.AuthorId == 0)
            {
                forumThread.AuthorId = null;
            }

            var thread = await _unitOfWork.ForumThreadRepository.AddAsync(forumThread);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Added a new thread with id {id}", thread.Id);

            var forumThreadView = _mapper.Map<ForumThreadModel>(forumThread);
            return forumThreadView;
        }

        /// <summary>
        /// Adds the theme asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Task&lt;ThemeModel&gt;
        /// </returns>
        /// <exception cref="AlreadyExistException"></exception>
        public async Task<ThemeModel> AddThemeAsync(ThemeModel model)
        {
            var themes = await _unitOfWork.ThemeRepository.GetAllAsync();

            if(themes != null)
            {
                var isExist = themes.Any(t => t.ThemeName == model.ThemeName);

                if (isExist)
                {
                    throw new AlreadyExistException(String.Format(ExceptionMessages.AlreadyExists, typeof(Theme).Name, "RoleName", model.ThemeName));
                }
            }

            var theme = _mapper.Map<Theme>(model);

            await _unitOfWork.ThemeRepository.AddAsync(theme);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Added a new theme {theme}", model?.ThemeName);

            var themeView = _mapper.Map<ThemeModel>(theme);
            return themeView;
        }

        /// <summary>
        /// Deletes the ForumThread by identifier asynchronous.
        /// </summary>
        /// <param name="modelId">The model identifier.</param>
        /// <exception cref="NotFoundException"></exception>
        public async Task DeleteByIdAsync(int modelId)
        {
            var forumThread = await _unitOfWork.ForumThreadRepository.DeleteByIdAsync(modelId);

            if (forumThread == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(ForumThread).Name, "Id", modelId.ToString()));
            }

            await _unitOfWork.SaveAsync();

            _logger.LogInformation("ForumThread with an id {id} has been deleted.", modelId);
        }

        /// <summary>
        /// Deletes the theme by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="NotFoundException"></exception>
        public async Task DeleteThemeByIdAsync(int id)
        {
            var theme = await _unitOfWork.ThemeRepository.DeleteByIdAsync(id);

            if (theme == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(Theme).Name, "Id", id.ToString()));
            }

            await _unitOfWork.SaveAsync();
            _logger.LogInformation("Theme with an id {id} has been deleted.", id);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// Task&lt;IEnumerable&lt;ForumThreadModel&gt;&gt;.
        /// </returns>
        public async Task<IEnumerable<ForumThreadModel>> GetAllAsync()
        {
            var forumThreads = await _unitOfWork.ForumThreadRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ForumThreadModel>>(forumThreads);
        }

        /// <summary>
        /// Gets all themes a synchronize.
        /// </summary>
        /// <returns>
        /// Task&lt;IEnumerable&lt;ThemeModel&gt;&gt;.
        /// </returns>
        public async Task<IEnumerable<ThemeModel>> GetAllThemesAsync()
        {
            var themes = await _unitOfWork.ThemeRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ThemeModel>>(themes);
        }

        /// <summary>
        /// Gets the TModel by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Task&lt;ForumThreadModel&gt;
        /// </returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<ForumThreadModel> GetByIdAsync(int id)
        {
            var forumThread = await _unitOfWork.ForumThreadRepository.GetByIdAsync(id);

            if(forumThread == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(ForumThread).Name, "Id", id.ToString()));
            }

            return _mapper.Map<ForumThreadModel>(forumThread);
        }

        /// <summary>
        /// Gets the thread posts asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Task&lt;IEnumerable&lt;PostModel&gt;&gt;.
        /// </returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<IEnumerable<PostModel>> GetThreadPostsAsync(int id)
        {
            var forumThread = await _unitOfWork.ForumThreadRepository.GetByIdAsync(id);

            if(forumThread == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(ForumThread).Name, "Id", id.ToString()));
            }

            return _mapper.Map<IEnumerable<PostModel>>(forumThread.ThreadPosts);
        }

        /// <summary>
        /// Gets the threads by user identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Task&lt;IEnumerable&lt;ForumThreadModel&gt;&gt;.
        /// </returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<IEnumerable<ForumThreadModel>> GetThreadsByUserIdAsync(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            if(user == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Id", userId.ToString()));
            }

            return _mapper.Map<IEnumerable<ForumThreadModel>>(user.ThreadPosts);
        }

        /// <summary>
        /// Updates the ForumThread asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <exception cref="NotFoundException"></exception>
        public async Task UpdateAsync(int id, ForumThreadRequest model)
        {
            var forumThread = await _unitOfWork.ForumThreadRepository.GetByIdAsync(id);

            if (forumThread == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(ForumThread).Name, "Id", id.ToString()));
            }

            if (forumThread.ThemeId == 0)
            {
                forumThread.ThemeId = null;
            }

            if (forumThread.AuthorId == 0)
            {
                forumThread.AuthorId = null;
            }

            forumThread = _mapper.Map(model, forumThread);

            _unitOfWork.ForumThreadRepository.Update(forumThread);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Post with id {id} has been updated.", forumThread.Id);
        }
    }
}
