using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Entities.Forum;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class ForumThreadService : IForumThreadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ForumThreadService> _logger;

        public ForumThreadService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ForumThreadService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ForumThreadModel> AddAsync(ForumThreadModel model)
        {
            var forumThread = _mapper.Map<ForumThread>(model);

            await _unitOfWork.ForumThreadRepository.AddAsync(forumThread);

            _logger.LogInformation("Added a new thread by user {email}", forumThread.Author.Email);

            var forumThreadView = _mapper.Map<ForumThreadModel>(forumThread);
            return forumThreadView;
        }

        public async Task<ThemeModel> AddNewThemeAsync(ThemeModel model)
        {
            var themes = await _unitOfWork.ThemeRepository.GetAllAsync();

            var isExist = themes.Any(t => t.ThemeName == model.Name);

            if (isExist)
            {
                throw new AlreadyExistException(String.Format(ExceptionMessages.AlreadyExists, typeof(Theme).Name, "RoleName", model.Name));
            }

            var theme = _mapper.Map<Theme>(model);

            await _unitOfWork.ThemeRepository.AddAsync(theme);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Added a new theme {theme}", model.Name);

            var themeView = _mapper.Map<ThemeModel>(theme);
            return themeView;
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            var forumThread = await _unitOfWork.ForumThreadRepository.DeleteByIdAsync(modelId);

            if (forumThread == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(ForumThread).Name, "Id", modelId.ToString()));
            }

            _logger.LogInformation("ForumThread with an id {id} has been deleted.", modelId);
        }

        public async Task DeleteThemeByIdAsync(int id)
        {
            var theme = await _unitOfWork.ThemeRepository.DeleteByIdAsync(id);

            if (theme == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(Theme).Name, "Id", id.ToString()));
            }

            _logger.LogInformation("Theme with an id {id} has been deleted.", id);
        }

        public async Task<IEnumerable<ForumThreadModel>> GetAllAsync()
        {
            var forumThreads = await _unitOfWork.ForumThreadRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ForumThreadModel>>(forumThreads);
        }

        public async Task<ForumThreadModel> GetByIdAsync(int id)
        {
            var forumThread = await _unitOfWork.ForumThreadRepository.GetAllAsync();

            return _mapper.Map<ForumThreadModel>(forumThread);
        }

        public async Task<IEnumerable<PostModel>> GetThreadPostsAsync(int id)
        {
            var forumThread = await _unitOfWork.ForumThreadRepository.GetByIdAsync(id);

            if(forumThread == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(ForumThread).Name, "Id", id.ToString()));
            }

            return _mapper.Map<IEnumerable<PostModel>>(forumThread.ThreadPosts);
        }

        public async Task<IEnumerable<ForumThreadModel>> GetThreadsByUserIdAsync(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            if(user == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Id", userId.ToString()));
            }

            return _mapper.Map<IEnumerable<ForumThreadModel>>(user.ThreadPosts);
        }

        public async Task UpdateAsync(ForumThreadModel model)
        {
            var forumThread = await _unitOfWork.PostRepository.GetByIdAsync(model.Id);

            if (forumThread == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(ForumThread).Name, "Id", model.Id.ToString()));
            }

            forumThread = _mapper.Map(model, forumThread);

            _unitOfWork.PostRepository.Update(forumThread);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Post with id {id} has been updated.", forumThread.Id);
        }
    }
}
