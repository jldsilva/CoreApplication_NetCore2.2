using AutoMapper;
using Core.Application.ViewModels;
using Core.Domain.Entities;

namespace Core.Application.Mappers
{
	public class ViewModelToDomainMappingProfile : Profile
	{
		public ViewModelToDomainMappingProfile()
		{
			//CreateMap<EntityViewModel, Entity>();
			CreateMap<UserViewModel, User>();
		}
	}
}
