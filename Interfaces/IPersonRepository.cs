using ZipCoderAPI.Models;
using System.Collections.Generic;

namespace ZipCoderAPI.Interfaces
{
    public interface IPersonRepository
    {
         Person getPerson(long id);
         List<Person> getPersons();
         List<Person> getPersonsByState(StateData specificState);
    }
}