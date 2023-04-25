using Library.LMS;
using Library.LMS.Models;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using UWP.CueLMS.Dialogs;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels
{
    public class InstructorViewModel : ObservableObject
    {
        public InstructorViewModel(CourseService courseservice, PersonService personservice)
        {
            semester = 1; //default semester is spring
            courseService = courseservice;
            personService = personservice;
            NavigatedCourses = new List<Course>();
            NavigatedPeople = new List<Person>();
            if(Courses.Count == 0) //in case the list has nothing from the start, it wont run list navigation
            {
                courseCollection = new ObservableCollection<Course>(Courses);
            }
            else
            {
                CourseNavigator = new ListNavigator<Course>(Courses);
                CourseListNavigation(); //sets current page for navigated course list
                courseCollection = new ObservableCollection<Course>(NavigatedCourses);
                
            }
            if(People.Count == 0)
            {
                Collection = new ObservableCollection<Person>(People);
            }
            else
            {
                PersonNavigator = new ListNavigator<Person>(People);
                PersonListNavigation();
                Collection = new ObservableCollection<Person>(NavigatedPeople); //collection for person list
            }
        }
        private ListNavigator<Course> CourseNavigator { get; set; }
        private ListNavigator<Person> PersonNavigator { get; set; }
        public Person SelectedPerson { get; set; }
        public Course SelectedCourse { get; set; }
        public CourseService courseService { get; set; }
        public PersonService personService { get; set; }
        public List<Course> Courses { 
            get 
            {
                if (semester == 1)
                {
                    return courseService.SpringList;
                }
                else if (semester == 2)
                {
                    return courseService.SummerList;
                }
                else
                {
                    return courseService.FallList;
                }
            } 
        }
        public List<Person> People { get { return personService.personList; } }
        private List<Course> NavigatedCourses { get; set; } //navigation list
        private List<Person> NavigatedPeople { get; set; } //navigation list
        public ObservableCollection<Person> Collection { get; set; } //person collection display
        public ObservableCollection<Course> courseCollection { get; set; } //course collection display
        private void CourseListNavigation()
        {
            var dictionary = CourseNavigator.GetCurrentPage();
            NavigatedCourses.Clear();
            foreach (var item in dictionary)
            {
                NavigatedCourses.Add(item.Value);
            }
        }
        public void CourseNavigationBack()
        {
            if (CourseNavigator.HasPreviousPage == true)
            {
                CourseNavigator.GoBackward();
                CourseListNavigation();
                courseCollection.Clear();
                foreach (var course in NavigatedCourses)
                {
                    courseCollection.Add(course);
                }
            }
        }
        public void CourseNavigationForward()
        {
            if (CourseNavigator.HasNextPage == true)
            {
                CourseNavigator.GoForward();
                CourseListNavigation();
                courseCollection.Clear();
                foreach (var course in NavigatedCourses)
                {
                    courseCollection.Add(course);
                }
            }
        }
        public void PersonListNavigation()
        {
            var dictionary = PersonNavigator.GetCurrentPage();
            NavigatedPeople.Clear();
            foreach (var item in dictionary)
            {
                NavigatedPeople.Add(item.Value);
            }
        }
        public void PersonNavigationForward()
        {
            if (PersonNavigator.HasNextPage == true)
            {
                PersonNavigator.GoForward();
                PersonListNavigation();
                Collection.Clear();
                foreach (var person in NavigatedPeople)
                {
                    Collection.Add(person);
                }
            }
        }
        public void PersonNavigationBack()
        {
            if (PersonNavigator.HasPreviousPage == true)
            {
                PersonNavigator.GoBackward();
                PersonListNavigation();
                Collection.Clear();
                foreach (var person in NavigatedPeople)
                {
                    Collection.Add(person);
                }
            }
        }
        public async void AddStudent()
        {
            var dialog = new StudentDialog(personService);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Query = ""; //refresh
            SearchPeople();
        }
        public async void AddTeachingAssistant()
        {
            var dialog = new TeachingAssistantDialog(personService);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Query = "";
            SearchPeople();
        }
        public async void AddInstructor()
        {
            var dialog = new InstructorDialog(personService);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Query = "";
            SearchPeople();
        }
        public void SearchPeople()
        {
            var searchResults = People.Where(p => p.Name.Contains(Query)).ToList();
            if (searchResults.Count == 0) //if count is 0, then it won't call list navigator
            {
                Collection.Clear();
                foreach (var person in searchResults)
                {
                    Collection.Add(person);
                }
            }
            else //list navigator called
            {
                PersonNavigator = new ListNavigator<Person>(searchResults);
                PersonListNavigation(); //adds new navigated list items to NavigatedPeople list
                Collection.Clear();
                foreach (var person in NavigatedPeople)
                {
                    Collection.Add(person);
                }
            }
        }
        public string Query { get; set; }
        public string Search { get; set; }
        public string NewName { private get; set; }
        public string NewId { get { return newid; } set { newid = value.ToUpper(); } }
        private string newid { get ; set; }
        private string classification { get; set; }
        public void Classification(string s)
        {
            var student = (Student)SelectedPerson;
            if (s == "f")
            {
                classification = "Freshman";
            }
            else if (s == "s")
            {
                classification = "Sophomore";
            }
            else if (s == "j")
            {
                classification = "Junior";
            }
            else if (s == "e")
            {
                classification = "Senior";
            }
        }
        public async void UpdateId()
        {
            var oldId = SelectedPerson.ID;
            if (NewId != null)
            {
                SelectedPerson.ID = NewId;
                personService.IDDictionary.Remove(oldId); //removes old Id from Id check
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/Person", SelectedPerson, HttpMethod.Post);
            }
            NotifyPropertyChanged(nameof(DisplayPerson));
        }
        public bool CheckId()
        {
            if (personService.CheckID(NewId) == true) //if it doesnt exist
            {
                return true;
            }
            else //if it does
            {
                return false;
            }
        }
        public async void UpdateName()
        {
            if (NewName != null)
            {
                SelectedPerson.Name = NewName;
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/Person", SelectedPerson, HttpMethod.Post);
            }
            NotifyPropertyChanged(nameof(DisplayPerson));
        }
        public async void UpdateClassification()
        {
            if (classification != null)
            {
                Student selected = (Student)SelectedPerson;
                selected.Classification = classification;
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/Person", SelectedPerson, HttpMethod.Post);
            }
            NotifyPropertyChanged(nameof(DisplayPerson));
        }
        public string DisplayPerson
        {
            get
            {
                if (SelectedPerson != null)
                {
                    return SelectedPerson.Display;
                }
                else { return "No Person Selected"; }
            }
        }
        public void SemesterSelect(int choice)
        {
            if (choice == 1) //spring list
            {
                semester = 1;
            }
            else if (choice == 2) //summer list
            {
                semester = 2;
            }
            else if (choice == 3) //fall list
            {
                semester = 3;
            }
        }
        public int semester { get; set; }
        public async void SemesterDialog()
        {
            var dialog = new SemesterDialog(this);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Search = ""; //refresh
            SearchCourses();
        }
        public async void AddCourse()
        {
            var dialog = new CourseDialog(courseService, semester);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Search = ""; //refresh
            SearchCourses();
        }
        public void SearchCourses()
        {
            var searchResults = Courses.Where(p => p.Name.Contains(Search) || p.Description.Contains(Search)).ToList();
            if (searchResults.Count == 0) //if the results are empty it won't use list navigator
            {
                courseCollection.Clear();
                foreach (var course in searchResults)
                {
                    courseCollection.Add(course);
                }
            }
            else //calls list navigator
            {
                CourseNavigator = new ListNavigator<Course>(searchResults);
                CourseListNavigation(); //adds new navigated list items to NavigatedCourses list
                courseCollection.Clear();
                foreach (var course in NavigatedCourses)
                {
                    courseCollection.Add(course);
                }
            }
        }
        public void DeleteCourse()
        {
            var oldCode = SelectedCourse.Code;
            Courses.Remove(SelectedCourse);
            courseService.Codes.Remove(oldCode); //clears up so old code can be used again
            Search = ""; //refresh
            SearchCourses();
        }
        //course update stuff
        public string NewCourseCode { get { return newcode; } set { newcode = value.ToUpper(); } }
        private string newcode { get; set; }
        public bool CheckCode() //checks if code doesnt exist
        {
            if (courseService.CheckCode(NewCourseCode) == true) //if it doesnt exist
            {
                return true;
            }
            else //if it does
            {
                return false;
            }
        }
        public string NewCourseName { get; set; }
        public string NewCourseDescription { get; set; }
        public int NewCourseHours { private get; set; }
        public string NewCourseRoom { get; set; }
        public string CourseCode
        {
            get
            {
                if (SelectedCourse != null)
                {
                    return SelectedCourse.Code;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string CourseName
        {
            get
            {
                if (SelectedCourse != null)
                {
                    return SelectedCourse.Name;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string CourseDescription
        {
            get
            {
                if (SelectedCourse != null)
                {
                    return SelectedCourse.Description;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public int CourseHours
        {
            get
            {
                if (SelectedCourse != null)
                {
                    return SelectedCourse.CreditHours;
                }
                else
                {
                    return 0;
                }
            }
        }
        public string CourseRoom
        {
            get
            {
                if (SelectedCourse != null)
                {
                    return SelectedCourse.RoomLocation;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public async void PostUpdateCourse() //updates course to server
        {
            if (semester == 1) //send update to spring
            {
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/SpringCourse", SelectedCourse, HttpMethod.Post);
            }
            else if (semester == 2) //send update to summer
            {
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/SummerCourse", SelectedCourse, HttpMethod.Post);
            }
            else if (semester == 3) //send update to fall
            {
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/FallCourse", SelectedCourse, HttpMethod.Post);
            }
            
        }
        public void UpdateCourseCode()
        {
            var oldCode = SelectedCourse.Code;
            if (NewCourseCode != null)
            {
                SelectedCourse.Code = NewCourseCode;
                courseService.Codes.Remove(oldCode);
                PostUpdateCourse();
            }
            NotifyPropertyChanged(nameof(CourseCode));
        }
        public void UpdateCourseName()
        {
            if (NewCourseName != null)
            {
                SelectedCourse.Name = NewCourseName;
                PostUpdateCourse();
            }
            NotifyPropertyChanged(nameof(CourseName));
        }
        public void UpdateCourseDescription()
        {
            if (NewCourseDescription != null)
            {
                SelectedCourse.Description = NewCourseDescription;
                PostUpdateCourse();
            }
            NotifyPropertyChanged(nameof(CourseDescription));
        }
        public void UpdateCourseCreditHours()
        {
            SelectedCourse.CreditHours = NewCourseHours;
            NotifyPropertyChanged(nameof(CourseHours));
            PostUpdateCourse();
        }
        public void UpdateCourseRoom()
        {
            if (NewCourseRoom != null)
            {
                SelectedCourse.RoomLocation = NewCourseRoom;
                PostUpdateCourse();
            }
            NotifyPropertyChanged(nameof(CourseRoom));
        }

    }
}
