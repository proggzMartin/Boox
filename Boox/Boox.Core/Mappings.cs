using AutoMapper;
using Boox.Core.Models.Dtos;
using Boox.Core.Models.Entities;

namespace Boox.Core
{
    /// <summary>
    /// Automapper mappings
    /// </summary>
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<BookDto, Book>();
        }
    }
}
