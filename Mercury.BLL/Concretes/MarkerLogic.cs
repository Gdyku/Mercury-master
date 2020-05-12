using AutoMapper;
using Mercury.BLL.Dtos;
using Mercury.DAL.Entities;
using Mercury.DAL.Repository;
using Mercury.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.BLL.Concretes
{
    public class MarkerLogic : IMarkerLogiccs
    {
        private readonly IMarkerRepository _markerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MarkerLogic(IMarkerRepository markerRepository, IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _markerRepository = markerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MarkerDto> CreateMarker(MarkerDto marker)
        {
            if (marker == null)
            {
                throw new InvalidOperationException();
            }

            var markerInDb = _mapper.Map<MarkerDto, Marker>(marker);

            markerInDb.IsActive = true;

            _markerRepository.Add(markerInDb);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<Marker, MarkerDto>(markerInDb);

        }

        public async Task DeleteMarker(long markerId)
        {
            var markerInDb = await _markerRepository.GetWithThrow(m => m.Id == markerId);

            _markerRepository.Remove(markerInDb);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<MarkerDto> GetMarker(long markerId)
        {
            var marker = await _markerRepository.GetWithThrow(m => m.Id == markerId);

            var mappedMarker = _mapper.Map<Marker, MarkerDto>(marker);

            return mappedMarker;
        }

        public async Task<List<MarkerDto>> GetMarkers()
        {
            var markers = await _markerRepository.GetAll();

            var mappedMarkers =  _mapper.Map<IEnumerable<Marker>, IEnumerable<MarkerDto>>(markers);

            return mappedMarkers.ToList();
        }

        public async Task<MarkerDto> UpdateMarker(MarkerDto marker)
        {
            var markerInDb = await _markerRepository.GetWithThrow(u => u.Id == marker.Id);

            _mapper.Map(marker, markerInDb);

            await _unitOfWork.CompleteAsync();

            var mappedMarker = _mapper.Map<Marker, MarkerDto>(markerInDb);

            return mappedMarker;
        }
    }
}
