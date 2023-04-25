using Library.LMS.Models;
using Library.LMS.Services;
using System.Collections.Generic;
using System.Net.Http;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels
{
    public class PersonViewModel
    {
        private List<Person> people;
        public Dictionary<string, int> IDDictionary { get; set; } //ID checker
        public PersonViewModel(PersonService service)
        {
            Student = new Student();
            TeachingAssistant = new TeachingAssistant();
            Instructor = new Instructor();

            Service = service;
            people = Service.personList;
        }
        private Student Student { get; set; }
        private PersonService Service { get; set; }
        private TeachingAssistant TeachingAssistant { get; set; }
        private Instructor Instructor { get; set; }
        public string Name { 
            get { return Student.Name; } 
            set { Student.Name = value; } 
        }
        private string id { get; set; }
        public string Id
        { private get { return id; } set { id = value.ToUpper(); }  }
        public void Classification(string s)
        {
            if (s == "f")
            {
                Student.Classification = "Freshman";
            }
            else if (s == "s") 
            {
                Student.Classification = "Sophomore";
            }
            else if (s == "j")
            {
                Student.Classification = "Junior";
            }
            else if (s == "e")
            {
                Student.Classification = "Senior";
            }
        }
        public async void AddStudent()
        {
            Student.ID = Id; //sets Id
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Person", Student, HttpMethod.Post);
        }
        public string TAName
        {
            get { return TeachingAssistant.Name; }
            set { TeachingAssistant.Name = value; }
        }
        public async void AddTA()
        {
            TeachingAssistant.ID = Id; //sets Id
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Person", TeachingAssistant, HttpMethod.Post);
        }
        public string InstructorName
        {
            get { return Instructor.Name; }
            set { Instructor.Name = value; }
        }
        public async void AddInstructor()
        {
            Instructor.ID = Id; //sets Id
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Person", Instructor, HttpMethod.Post);
        }
        public bool CheckId() //checks if Id doesnt exist
        {
            if (Service.CheckID(Id) == true) //if it doesnt exist
            {
                return true;
            }
            else //if it does
            {
                return false;
            }
        }

    }
}
