using ZipCoderAPI.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZipCoderAPI.Models
{
    public class Person {

        [Required]
        public int id {get; set;} 
        public string firstName {get; set;}
        public string lastName {get; set;}
        public string postalCode {get; set;}

        [ForeignKey("stateName")]
        public string state {get; set;} 

        public void updateState(IStateMatcher stateMatcher) {
            this.state = stateMatcher.LocateState(this.postalCode);
        }
    }
}