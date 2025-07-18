using System.Collections.Generic;
using System;

namespace Institute
{
  class Course
  {
    public void AddCourseDetails(string name, int fee)
    {
      if (Program.CourseDetails.ContainsKey(name))
      {
        Console.WriteLine("Course already exists. Updating the fee.");
        Program.CourseDetails[name] = fee;
      }
      else
      {
        Program.CourseDetails.Add(name, fee);
        Console.WriteLine("Course added Successfully...");
      }
    }
    public void RemoveCourseDetails(string name)
    {
      if (!Program.CourseDetails.ContainsKey(name))
      {
        Console.WriteLine("Course not found..");
      }
      else
      {
        Program.CourseDetails.Remove(name);
        Console.WriteLine("Course removed Successfully..");
      }
    }
    public Dictionary<string, int> SortCourseByFee()
    {
      List<string> CourseName = new List<string>(Program.CourseDetails.Keys);
      for (int i = 0; i < CourseName.Count; i++)
      {
        for (int j = i + 1; j < CourseName.Count; j++)
        {
          if (Program.CourseDetails[CourseName[i]] > Program.CourseDetails[CourseName[j]])
          {
            string temp = CourseName[i];
            CourseName[i] = CourseName[j];
            CourseName[j] = temp;
          }
        }
      }

      Dictionary<string, int> sortedCourse = new Dictionary<string, int>();
      foreach (string name in CourseName)
      {
        sortedCourse.Add(name, Program.CourseDetails[name]);
      }
      return sortedCourse;
    }
  }
}