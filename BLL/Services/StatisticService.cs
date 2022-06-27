using AutoMapper;
using Services.Models;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    /// <summary>
    /// Statistic service
    /// </summary>
    /// <seealso cref="IStatisticService" />
    public class StatisticService : IStatisticService
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
        /// Initializes a new instance of the <see cref="StatisticService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="mapper">The mapper.</param>
        public StatisticService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the most active users asynchronous.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>
        /// Task&lt;IEnumerable&lt;UserModel&gt;&gt;.
        /// </returns>
        public async Task<IEnumerable<UserModel>> GetMostActiveUsersAsync(int count)
        {
            var mostActive = await _unitOfWork.UserRepository.GetAllAsync();

            mostActive = mostActive.OrderByDescending(x => x.ThreadPosts?.Count ?? 0).ThenByDescending(y => y.Threads?.Count?? 0).Take(count);

            mostActive = mostActive.ToList();

            var ret = _mapper.Map<IEnumerable<UserModel>>(mostActive);

            return ret;
        }

        /// <summary>
        /// Gets the most popular threads asynchronous.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>
        /// Task&lt;IEnumerable&lt;ForumThreadModel&gt;&gt;.
        /// </returns>
        public async Task<IEnumerable<ForumThreadModel>> GetMostPopularThreadsAsync(int count)
        {
            var mostPopular = await _unitOfWork.ForumThreadRepository.GetAllAsync();

            mostPopular = mostPopular.OrderByDescending(x => x.ThreadPosts?.Count ?? 0).ThenBy(y => y.TimeCreated).Take(count).ToList();

            return _mapper.Map<IEnumerable<ForumThreadModel>>(mostPopular);
        }
    }
}
