using AutoMapper;
using Boox.Core.Models.Dtos;
using Boox.Core.Models.Entities;

namespace Boox.Core
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<BookDto, Book>();
        }
    }
}
