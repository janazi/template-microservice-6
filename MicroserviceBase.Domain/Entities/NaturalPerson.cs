using Jnz.RedisRepository.Interfaces;

namespace MicroserviceBase.Controllers.V1;

public class NaturalPerson : IRedisCacheable
{
    public NaturalPerson()
    {
        
    }
    public NaturalPerson(string identifierNumber, string fullname, DateTime birthday, char gender)
    {
        IdentifierNumber = identifierNumber;
        Fullname = fullname;
        Birthday = birthday;
        Gender = gender;
        CreatedAt = DateTime.Now;
        PersonId = Guid.NewGuid();
    }

    public Guid PersonId { get; set; }
    public string IdentifierNumber { get; set; }
    public string Fullname { get; set; }
    public DateTime Birthday { get; set; }
    public char Gender { get; set; }
    public DateTime CreatedAt { get; set; }
    public string GetKey()
    {
        return "NaturalPerson";
    }

    public string GetIndex()
    {
        return PersonId.ToString();
    }

    public int GetDatabaseNumber()
    {
        return 0;
    }

    public TimeSpan? GetExpiration()
    {
        return TimeSpan.FromDays(10);
    }
}