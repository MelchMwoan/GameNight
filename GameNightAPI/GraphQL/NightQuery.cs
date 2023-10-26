using Domain;
using DomainServices;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLServer.GraphQL;

[ApiExplorerSettings(IgnoreApi = true)]
public class NightQuery
{
	private readonly INightRepository _nightRepository;

	public NightQuery(INightRepository nightRepository)
	{
		_nightRepository = nightRepository;
	}
	public IEnumerable<Night> GetAllNights()
	{
		return _nightRepository.getNights().ToArray();
	}

    public Night GetNightById(int id)
    {
        return _nightRepository.getNightById(id).Night;
    }

    
}
