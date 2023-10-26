using Domain;
using DomainServices;

namespace GraphQLServer.GraphQL;

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
