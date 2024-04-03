
using AutoMapper;
using LyncasSales.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LyncasSales.Services
{
    public class BaseService<TEntity, TDto>
        where TEntity : class
        where TDto : class 
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public async Task<TDto> CreateAsync(TDto dto)
        {
            try
            {
                var mapEntity = _mapper.Map<TEntity>(dto);
                await _baseRepository.Create(mapEntity);
                await _baseRepository.SaveChangesAsync();
                return dto;
            }
            catch (Exception ex)
            {
          
                throw new Exception("Error occurred while creating entity.", ex);
            }
        }

        public async Task<TDto> UpdateAsync(int id,TDto dto)
        {
             var getById = await _baseRepository.GetById(id);
             _mapper.Map(dto, getById);
             await  _baseRepository.Update(getById);
             await _baseRepository.SaveChangesAsync();
             return dto;

        }
        
        public async Task<bool> Delete(int id)
        {
            var getbyId = await _baseRepository.GetById(id);
            var delete = _mapper.Map<TEntity>(getbyId);
            await  _baseRepository.Delete(delete);
            return await _baseRepository.SaveChangesAsync();
        }

        public async Task<List<TDto>> GetAll()
        {
            var entities = await _baseRepository.GetAll().ToListAsync();
            return _mapper.Map<List<TDto>>(entities);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var getId = await _baseRepository.GetById(id);
            var get = _mapper.Map<TEntity>(getId);
            return get;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _baseRepository.SaveChangesAsync();
        }

    }
        

        

    
}
