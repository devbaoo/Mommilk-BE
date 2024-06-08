using AutoMapper;
using Data;
using Data.Repositories.Interfaces;

namespace Application.Services
{
    public class BaseService
    {
        protected IMapper _mapper;
        protected IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public BaseService(IAccountRepository accountRepository, IMapper mapper)
        {
        }
    }
}
