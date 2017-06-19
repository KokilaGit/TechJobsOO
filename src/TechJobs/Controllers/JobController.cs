using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;
        //private object jobdata;
        //private static JobFieldData<Employer> employerData = new JobFieldData<Employer>();

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
            SearchJobsViewModel jobById = new SearchJobsViewModel();
            jobById.Jobs = new List<Job>();
            jobById.Jobs.Add(jobData.Find(id));
            //Models.Job jobById = jobData.Find(id);
            return View(jobById);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.
            if(ModelState.IsValid)
            {
                Models.Job newJob = new Models.Job()
               {
                    Name = newJobViewModel.Name,
                    Employer = jobData.Employers.Find(newJobViewModel.EmployerID),
                    Location = jobData.Locations.Find(newJobViewModel.LocationID),
                    CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID),
                    PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionID),

                };
                jobData.Jobs.Add(newJob);
                SearchJobsViewModel jobById = new SearchJobsViewModel();
                jobById.Jobs = new List<Job>();
                jobById.Jobs.Add(newJob);
                return View("Index",jobById);
            }
            else
            {
                return View(newJobViewModel);
            }
            
        }
    }
}
