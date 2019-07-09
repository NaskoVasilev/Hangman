using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;

namespace Hangman.Mappings
{
	public static class AuomapperExtensions
	{
		public static IQueryable<TDestination> To<TDestination>(this IQueryable source)
		{
			return source.ProjectTo<TDestination>(); 
		}

		public static TDestination To<TDestination>(this object source)
		{
			var mappedObject = (TDestination)Mapper.Map(source, source.GetType(), typeof(TDestination));
			return mappedObject;
		}
	}
}
