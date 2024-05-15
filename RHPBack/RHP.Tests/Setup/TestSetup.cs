

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RHP.API.Controllers;
using RHP.API.Repositories;
using RHP.API.Services;
using RHP.Data;
using RHP.Entities.Models.Mappers;

namespace RHP.Tests.Setup
{
    public class TestSetup
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public TestSetup() 
        {
            var services = new ServiceCollection();

            services.AddScoped<PlayerService>();
            services.AddScoped<PlayerController>();
            services.AddScoped<PlayerRepository>();
            services.AddScoped<HallService>();
            services.AddScoped<HallRepository>();
            services.AddScoped<UserService>();
            services.AddScoped<UserRepository>();
            services.AddScoped<AuthenticationService>();
            services.AddScoped<AuthenticationController>();

            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<PlayerMapper>())));
            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<UserMapper>())));
            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<HallMapper>())));


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddDbContext<ApplicationDbContext>(options =>
                           options.UseInMemoryDatabase("TestDb"));


            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
