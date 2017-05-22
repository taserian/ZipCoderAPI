using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using ZipCoderAPI.Models;
using ZipCoderAPI.Interfaces;

namespace ZipCoderAPI.Data
{
    public class PersonXmlDb: IPersonRepository
    {
        public List<Person> persons;
        public IStateMatcher stateMatcher;

        public PersonXmlDb(IStateMatcher matcher) {
            stateMatcher = matcher;
            string pathToXml = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "dataset.xml";
            XDocument xmlData = XDocument.Load(pathToXml);
            persons = xmlData.Descendants("record").Select(x =>
                    new Person {
                        id = Convert.ToInt32(x.Element("id").Value),
                        firstName = x.Element("first_name").Value,
                        lastName = x.Element("last_name").Value,
                        postalCode = x.Element("postal_code").Value
                    }).ToList();
            foreach (var person in persons) {
                person.updateState(stateMatcher);
            }
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