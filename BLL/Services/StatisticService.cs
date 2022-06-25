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
    public class StatisticService : IStatisticService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StatisticService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserModel>> GetMostActiveUsersAsync(int count)
        {
            var mostActive = await _unitOfWork.UserRepository.GetAllAsync();

            mostActive = mostActive.OrderByDescending(x => x.ThreadPosts?.Count ?? 0).ThenByDescending(y => y.Threads?.Count?? 0).Take(count);

            mostActive = mostActive.ToList();

            var ret = _mapper.Map<IEnumerable<UserModel>>(mostActive);

            return ret;
        }

        public async Task<IEnumerable<ForumThreadModel>> GetMostPopularThreadsAsync(int count)
        {
            var mostPopular = await _unitOfWork.ForumThreadRepository.GetAllAsync();

            mostPopular = mostPopular.OrderByDescending(x => x.ThreadPosts?.Count ?? 0).ThenBy(y => y.TimeCreated).Take(count).ToList();

            return _mapper.Map<IEnumerable<ForumThreadModel>>(mostPopular);
        }
    }
}
