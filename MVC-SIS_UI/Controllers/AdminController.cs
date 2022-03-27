using MVC_SIS_Data;
using MVC_SIS_Models;
using MVC_SIS_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_SIS_UI.Controllers
{
    public class AdminController : Controller
    {

        [HttpGet]
        public ActionResult Majors()
        {
            var model = MajorRepository.GetAll();
            return View(model.ToList());
        }

        // 2) Create a method in the Admin Controller for States. This method should return a view to lists all the existing states
        [HttpGet]
        public ActionResult States()
        {
            var model = StateRepository.GetAll();
            return View(model.ToList());
        }

        // 5a) Create a GET method in the Admin controller for adding states (AddState). It should return a view with an empty state model(AddEditStateVM) as a parameter.
        [HttpGet]
        public ActionResult AddState()
        {
            return View(new AddEditStateVM());
        }

        // 5b) Create the POST method in the Admin controller for adding states (AddState)
        [HttpPost]
        public ActionResult AddState(AddEditStateVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            viewModel.currentState.StateAbbreviation.ToUpper();
            StateRepository.Add(viewModel.currentState);
            return RedirectToAction("States");
        }

        // 7a) Add a GET method in the Admin controller for DeleteState. The method should take a string parameter for StateAbbreviation
        [HttpGet]
        public ActionResult DeleteState(string id)
        {
            DeleteStateVM viewModel = new DeleteStateVM();
            viewModel.currentState = StateRepository.Get(id);
            return View(viewModel);
        }

        // 7b) Create the POST method in the Admin controller for deleting states (DeleteState)
        // The method should redirect the user to the States list view upon the successful completion of the Delete operation
        [HttpPost]
        public ActionResult DeleteState(DeleteStateVM viewModel)
        {
            StateRepository.Delete(viewModel.currentState.StateAbbreviation);
            return RedirectToAction("States");
        }

        // Chose to create edit functionality
        [HttpGet]
        public ActionResult EditState(string id)
        {
            AddEditStateVM viewmodel = new AddEditStateVM();
            viewmodel.currentState = StateRepository.Get(id);
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult EditState(AddEditStateVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            StateRepository.Edit(viewModel.currentState);
            return RedirectToAction("States");
        }

        [HttpGet]
        public ActionResult AddMajor()
        {
            return View(new AddEditMajorVM());
        }

        [HttpPost]
        public ActionResult AddMajor(AddEditMajorVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            MajorRepository.Add(viewModel.currentMajor.MajorName);
            return RedirectToAction("Majors");
        }

        [HttpGet]
        public ActionResult EditMajor(int id)
        {
            AddEditMajorVM viewmodel = new AddEditMajorVM();
            viewmodel.currentMajor = MajorRepository.Get(id);
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult EditMajor(AddEditMajorVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            MajorRepository.Edit(viewModel.currentMajor);
            return RedirectToAction("Majors");
        }

        [HttpGet]
        public ActionResult DeleteMajor(int id)
        {
            DeleteMajorVM viewModel = new DeleteMajorVM();
            viewModel.currentMajor = MajorRepository.Get(id);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult DeleteMajor(DeleteMajorVM viewModel)
        {
            MajorRepository.Delete(viewModel.currentMajor.MajorId);
            return RedirectToAction("Majors");
        }

    }
}