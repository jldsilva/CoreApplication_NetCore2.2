using AutoMapper;
using Core.Application.ViewModels;
using Core.Domain.Entities;

namespace Core.Application.Mappers
{
	public class DomainToViewModelMappingProfile : Profile
	{
		public DomainToViewModelMappingProfile()
		{
			//CreateMap<Entity, EntityViewModel>();
			CreateMap<User, UserViewModel>();
		}
	}
}
