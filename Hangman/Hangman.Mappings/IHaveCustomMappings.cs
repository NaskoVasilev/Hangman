using AutoMapper;

namespace Hangman.Mappings
{
	public interface IHaveCustomMappings
	{
		void CreateMappings(IProfileExpression configuration);
	}
}
