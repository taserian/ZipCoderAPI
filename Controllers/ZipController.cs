using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Linq;
using ZipCoderAPI.Models;
using ZipCoderAPI.Data;
using ZipCoderAPI.Interfaces;
using ZipCoderAPI.Modules;
using Microsoft.Data.Sqlite;

namespace ZipCodeDataWebService.Controllers
{
    
    [Route("api/[controller]")]
    public class ZipController : Controller
    {
        private static StateNamesAndAbbreviations SNAA = new StateNamesAndAbbreviations();
        private static IStateMatcher stateMatcher = new StateIntervalTreeMatcher();
        private IPersonRepository PersonDb;

        [Route("{source}")]
        [HttpGet]
        public IActionResult Get(string source)
        {
            StateList output = new StateList();
            List<StateListItem> stateList = new List<StateListItem>();
            try {
                PersonDb = SetDatabase(source);
                IQueryable<Person> persons = PersonDb.getPersons().AsQueryable();
                foreach (var record in persons.GroupBy(info => info.state)
                                       .Select(group => new {
                                               Metric = group.Key,
                                               Count = group.Count()
                                       })
                                       .OrderBy(x => x.Metric))
                {
                    string abbr = ""; 
                    if (StateNamesAndAbbreviations.state2Abbr.TryGetValue(record.Metric, out abbr)) {
                        stateList.Add(new StateListItem( record.Metric, abbr, record.Count ));
                    };
                }
                output.results = stateList;

            } catch (Exception e) {
                    return new NotFoundObjectResult(e.Message);
            }    
            return new OkObjectResult(output);
        }

        // GET api/zip/csv/ca
        [Route("{source}/{stateAbbr}")]
        [HttpGet("{stateAbbr:alpha:length(2)}")]
        public IActionResult Get(string source, string stateAbbr)
        {
            StatePersonRecordSet stateRecordSet = new StatePersonRecordSet();
            try {
                PersonDb = SetDatabase(source);
                stateRecordSet.stateName = StateNamesAndAbbreviations.abbr2State[stateAbbr.ToUpper()];
                stateRecordSet.stateAbbreviation = StateNamesAndAbbreviations.state2Abbr[stateRecordSet.stateName]; 
                IQueryable<Person> persons = PersonDb.getPersons().AsQueryable();
                stateRecordSet.personRecords = persons.Where(p => p.state == stateRecordSet.stateName)
                                                .OrderBy(p => p.id)
                                                .ToList<Person>();
                                                
            } catch (Exception e) {
                return new NotFoundObjectResult(e.Message);
            }
            return new OkObjectResult(stateRecordSet);
        }

        private IPersonRepository SetDatabase(string source) {
            IPersonRepository Db;
            switch(source.ToUpper()) {
                case "CSV":
                    Db = new PersonCsvDb(stateMatcher);
                    break;
                case "XML":
                    Db = new PersonXmlDb(stateMatcher);
                    break;
                case "XLS":
                    Db = new PersonXlsDb(stateMatcher);
                    break;
                default:
                    throw new Exception("Invalid data source.");
            }
            return Db;
        }
    }
}
