using Library.LMS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.CueLMS.ViewModels
{
    public class StudentViewModel
    {
        private ObservableCollection<Person> people;
        public Dictionary<string, int> IDDictionary { get; set; } //ID checker
        public StudentViewModel(ObservableCollection<Person> persons) 
        {
            Student = new Student();
            this.people = persons;
            IDDictionary = new Dictionary<string, int>();
        }
        private Student Student { get; set; }
        public string Name { 
            get { return Student.Name; } 
            set { Student.Name = value; } 
        }
        public string Id
        {
            get { return Student.ID; }
            set { Student.ID = value.ToUpper(); }
        }
        public string Classification
        {
            get { return Student.Classification; }
            set
            {
                if (value.Equals("f"))
                {
                    Student.Classification = "Freshman";
                }
                else if (value.Equals("s"))
                {
                    Student.Classification = "Sophomore";
                }
                else if (value.Equals("j"))
                {
                    Student.Classification = "Junior";
                }
                else if (value.Equals("e"))
                {
                    Student.Classification = "Senior";
                }
                else
                {
                    Student.Classification = "N/A";
                }
            }
        }
        public void AddStudent()
        {
            people.Add(Student);
        }
        public bool CheckID(string ID)
        {
            var result = new ArgumentException();
            try { IDDictionary.Add(ID, 0); }
            catch (ArgumentException r)
            {
                result = r;
            }
            if (result.Source == null) //if key already exists
            {
                return true;
            }
            else { return false; }
        }
    }
}
