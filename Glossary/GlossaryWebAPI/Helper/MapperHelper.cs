
using AutoMapper;
using GlossaryWebAPI.Models;

namespace GlossaryWebAPI.Helper
{
    public static class MapperHelper
    {
        public static void SetUp()
        {
            Mapper.CreateMap<GlossaryModel, GlossaryDBContext.DBClasses.Glossary>().ReverseMap();
        }
    }
}