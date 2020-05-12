using Mercury.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.BLL.Dtos
{
    public interface IMarkerLogiccs
    {
        Task<List<MarkerDto>> GetMarkers();
        Task<MarkerDto> GetMarker(long markerId);
        Task<MarkerDto> CreateMarker(MarkerDto marker);
        Task<MarkerDto> UpdateMarker(MarkerDto marker);
        Task DeleteMarker(long markerId);
    }
}
