using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using ZipCoderAPI.Models;
using ZipCoderAPI.Interfaces;

namespace ZipCoderAPI.Data
{
    public class PersonXlsDb: IPersonRepository
    {
        public List<Person> persons;
        public IStateMatcher stateMatcher;

        public PersonXlsDb(IStateMatcher matcher) {
            stateMatcher = matcher;
            string pathToCsv = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "postalcode_practical.csv";
            persons = File.ReadAllLines(pathToCsv)
                                        .Skip(1)
                                        .Select(v => FromCSV(v) )
                                        .ToList();
        }

        public Person FromCSV(string csvLine){
            string[] values = csvLine.Split(',');
            Person onePerson = new Person();
            onePerson.id = Convert.ToInt32(values[0]);
            onePerson.firstName = values[1].Trim('\"');
            onePerson.lastName = values[2].Trim('\"');
            onePerson.postalCode = values[3].Trim('\"').PadLeft(5, '0');
            onePerson.updateState(stateMatcher);

            return onePerson;
        }

        public List<Person> getPersons() {
            return persons;
        }

        public Person getPerson(long id) {
            IQueryable<Person> p = persons.AsQueryable();
            return p.Where(unitPerson => unitPerson.id == id).FirstOrDefault();
        }

        public List<Person> getPersonsByState(StateData state) {
            IQueryable<Person> p = persons.AsQueryable();
            return p.Where(unitPerson => unitPerson.state == state.stateName).ToList();
        }
    }
}