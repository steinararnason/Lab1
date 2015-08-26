﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Web.Http.Description;
using Vefthjonustur.Models;
using System.Diagnostics;

namespace Lab1.Controllers
{
 
    [RoutePrefix("api/courses")]
    public class CoursesController : ApiController
    {
        private static List<Course> _courses;
        private static List<Student> _students;
        private static int ID_counter;

        #region Constructor
        public CoursesController()
        {
            if (_courses == null)
            {
                ID_counter = 3;
                _courses = new List<Course>
                {
                    new Course
                    {
                        ID         = 1,
                        Name       = "Web services",
                        TemplateID = "T-514-VEFT",
                        StartDate  = DateTime.Now,
                        EndDate    = DateTime.Now.AddMonths(3)
                    },
                    new Course
                    {
                        ID         = 2,
                        Name       = "Computer Networking",
                        TemplateID = "T-409-TSAM",
                        StartDate  = DateTime.Now,
                        EndDate    = DateTime.Now.AddMonths(3)
                    }
                };
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        [Route("")]
        public List<Course> getCourses()
        {
            return _courses;
        }
        
        /// <summary>
        /// Gets course by id
        /// Example data :
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("id/{id:int}", Name ="byId")]
        public Course getCourseById(int id)
        {
            for(int i = 0; i < _courses.Count; i++)
            {
                if(_courses[i].ID == id)
                {
                    return _courses[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param>id = ID of course, name = Name of the course, templateID = TemplateID of the course</param>
        /// <returns>BadRequest if the input data is incorrect, else it returns you to the newly created Course</returns>
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddCourse(Course c)
        {
            c.ID = ID_counter;
            Debug.WriteLine(ID_counter);
            if(c.Name.Length < 1|| c.TemplateID.Length < 1)
            {
                Debug.WriteLine("Going to send bad request");
                return BadRequest();
            }
            Debug.WriteLine("Everything okay");
            ID_counter++;
            _courses.Add(c);
            string location = Url.Link("byId", new { id = c.ID });
            return Created(location,c);
        }
    }
}