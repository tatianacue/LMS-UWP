using Library.LMS;
using Library.LMS.Models;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.CueLMS.Dialogs;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels
{
    public class InstructorViewModel : ObservableObject
    {
        public InstructorViewModel(CourseService courseservice, PersonService personservice)
        {
            courseService = courseservice;
            personService = personservice;
            Courses = courseService.SpringList;
            People = personService.personList;
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
        public List<Course> Courses { get; set; }
        public List<Person> People { get; set; }
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
        public void UpdateId()
        {
            var oldId = SelectedPerson.ID;
            if (NewId != null)
            {
                SelectedPerson.ID = NewId;
                personService.IDDictionary.Remove(oldId); //removes old Id from Id check
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
        public void UpdateName()
        {
            if (NewName != null)
            {
                SelectedPerson.Name = NewName;
            }
            NotifyPropertyChanged(nameof(DisplayPerson));
        }
        public void UpdateClassification()
        {
            if (classification != null)
            {
                Student selected = (Student)SelectedPerson;
                selected.Classification = classification;
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
                Courses = courseService.SpringList;
            }
            else if (choice == 2) //summer list
            {
                Courses = courseService.SummerList;
            }
            else if (choice == 3) //fall list
            {
                Courses = courseService.FallList;
            }
        }
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
            var dialog = new CourseDialog(Courses, courseService);
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
        public void UpdateCourseCode()
        {
            var oldCode = SelectedCourse.Code;
            if (NewCourseCode != null)
            {
                SelectedCourse.Code = NewCourseCode;
                courseService.Codes.Remove(oldCode);
            }
            NotifyPropertyChanged(nameof(CourseCode));
        }
        public void UpdateCourseName()
        {
            if (NewCourseName != null)
            {
                SelectedCourse.Name = NewCourseName;
            }
            NotifyPropertyChanged(nameof(CourseName));
        }
        public void UpdateCourseDescription()
        {
            if (NewCourseDescription != null)
            {
                SelectedCourse.Description = NewCourseDescription;
            }
            NotifyPropertyChanged(nameof(CourseDescription));
        }
        public void UpdateCourseCreditHours()
        {
            SelectedCourse.CreditHours = NewCourseHours;
            NotifyPropertyChanged(nameof(CourseHours));
        }
        public void UpdateCourseRoom()
        {
            if (NewCourseRoom != null)
            {
                SelectedCourse.RoomLocation = NewCourseRoom;
            }
            NotifyPropertyChanged(nameof(CourseRoom));
        }

    }
}
