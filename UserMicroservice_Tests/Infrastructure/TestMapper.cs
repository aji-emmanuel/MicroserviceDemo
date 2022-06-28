using AutoMapper;
using UserMicroservice.Utility;

namespace UserMicroservice_Tests.Infrastructure
{
    public static class TestMapper
    {
        public static IMapper GetMapper()
        {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                return mapper;
        }
    }
}
