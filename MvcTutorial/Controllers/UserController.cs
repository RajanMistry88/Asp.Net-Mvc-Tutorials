using MvcTutorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTutorial.Controllers
{
    public class UserController : Controller
    {
        //-------------------------------------------  Registration   --------------------------------------
        // GET:Display the Registration Page Design 
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        //POST: Add User All Details in to the DataBase 
        [HttpPost]
        public ActionResult Registration(UserRegistrationModelClass modelUserRegister)
        {
            if (ModelState.IsValid)
            {
                using (var database = new MvcTutorialEntities())
                {
                    tblUser dbobjectuser = new tblUser();
                    dbobjectuser.UserName = modelUserRegister.UserName;
                    dbobjectuser.MobileNo = modelUserRegister.MobileNo;
                    dbobjectuser.EmailAddress = modelUserRegister.EmailAddress;
                    dbobjectuser.Password = modelUserRegister.Password;
                    dbobjectuser.Address = modelUserRegister.Address;
                    dbobjectuser.IsActive = true;
                    dbobjectuser.AccountCreateDate = System.DateTime.Now;

                    database.tblUser.Add(dbobjectuser);
                    database.SaveChanges();
                    ViewBag.SuccessMessage = "Account Created Successfully.";
                }
            }
            return RedirectToAction("Login", "User");
        }

        //-------------------------------------------  Login   -----------------------------------------------
        // GET:Display the Login Page Design 
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //POST: Check The User Mobile no and Password is Correct or not
        [HttpPost]
        public ActionResult Login(UserLoginModelClass modeluserLoginData)
        {
            if (ModelState.IsValid)
            {
                using (var database = new MvcTutorialEntities())
                {
                    var Verify = database.tblUser.FirstOrDefault(user => user.MobileNo == modeluserLoginData.MobileNo && user.IsActive == true);
                    if (Verify == null)
                    {
                        ViewBag.Message = "Mobile No does not exist.";
                    }
                    else
                    {
                        if (Verify.Password == modeluserLoginData.Password)
                        {
                            Session["UserID"] = Verify.UserID;
                            Session["UserName"] = Verify.UserName;
                            ViewBag.SuccessMessage = "Login SuccessFully.";
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.Message = "Mobile No or Password is wrong.";

                        }
                    }
                }
            }

            return View();
        }

        //-------------------------------------------  Forgot Password   --------------------------------------
        // GET:Display the Forgot Password Page Design 
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //POST: check User is Register user or not if it is Registerd the Allow to change his/her password
        [HttpPost]
        public ActionResult ForgotPassword(UserForgotPasswordModelClass modelforgotpasswordData)
        {
            if (ModelState.IsValid)
            {
                using (var database = new MvcTutorialEntities())
                {
                    var verify = database.tblUser.FirstOrDefault(user => user.MobileNo == modelforgotpasswordData.MobileNo);
                    if (verify != null)
                    {
                        Session["ForgotPassword"] = verify.UserID;
                        return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);
                        //return RedirectToAction("ChangePassword");
                    }
                    else
                    {
                        ViewBag.Message = "Mobile No is not exisit";
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult CreateNewPassword()
        {
            if (Session["ForgotPassword"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ForgotPassword", "User");
            }
        }

        [HttpPost]
        public ActionResult CreateNewPassword(UserNewPasswordModelClass modelnewpasswordData)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MvcTutorialEntities())
                {
                    var UserID = Convert.ToInt32(Session["ForgotPassword"]);
                    var verify = db.tblUser.Find(UserID);
                    if (verify != null)
                    {
                        verify.Password = modelnewpasswordData.NewPassword;
                        db.SaveChanges();
                        ViewBag.SuccessMessage = "New Password Created Successfully. Please Re-Login with New Password.";
                        Session.RemoveAll();
                        return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);

                        //return RedirectToAction("Login", "User");
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["UserID"] != null || Session["ForgotPassword"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // POST:Change User Password as per Request
        [HttpPost]
        public ActionResult ChangePassword(UserChangePasswordModelClass modelchangerPasswordData)
        {
            if (ModelState.IsValid)
            {
                using (var database = new MvcTutorialEntities())
                {
                    int UserID = Convert.ToInt32(Session["UserID"]);
                    var verify = database.tblUser.FirstOrDefault(user => user.UserID == UserID && user.Password == modelchangerPasswordData.OldPassword && user.IsActive == true);
                    if (verify != null)
                    {
                        verify.Password = modelchangerPasswordData.NewPassword;
                        database.SaveChanges();

                        ViewBag.SuccessMessage = "Password Change Successfully. Please Re-Login With New Password.";
                        Session.RemoveAll();
                        return RedirectToAction("Login", "User");
                    }
                    else
                    {
                        ViewBag.Message = "You have Enter Wrong Old Password.";
                    }
                }
            }

            return View();
        }


        //-------------------------------------------  Update Profile  --------------------------------------
        // GET: Get Data From the database to Display in to user Profile
        [HttpGet]
        public ActionResult ProfileInfo()
        {
            if (Session["UserID"] != null)
            {
                using (var database = new MvcTutorialEntities())
                {
                    int UserID = Convert.ToInt32(Session["UserID"]);
                    var databaseProfileData = database.tblUser.FirstOrDefault(user => user.UserID == UserID && user.IsActive == true);
                    if (databaseProfileData != null)
                    {
                        UserProfileModelClass modelProfile = new UserProfileModelClass();
                        modelProfile.UserID = databaseProfileData.UserID;
                        modelProfile.UserName = databaseProfileData.UserName;
                        modelProfile.EmailAddress = databaseProfileData.EmailAddress;
                        modelProfile.MobileNo = databaseProfileData.MobileNo;
                        modelProfile.Address = databaseProfileData.Address;
                        return View(modelProfile);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // POST: Update Data From Which user have change 
        [HttpPost]
        public ActionResult ProfileInfo(UserProfileModelClass modelprofiledData)
        {
            if (ModelState.IsValid)
            {
                using (var database = new MvcTutorialEntities())
                {
                    int UserID = Convert.ToInt32(Session["UserID"]);
                    var databaseData = database.tblUser.FirstOrDefault(user => user.UserID == UserID && user.IsActive == true);
                    if (databaseData != null)
                    {

                        databaseData.MobileNo = modelprofiledData.MobileNo;
                        databaseData.EmailAddress = modelprofiledData.EmailAddress;
                        databaseData.Address = modelprofiledData.Address;

                        database.SaveChanges();

                        ViewBag.SuccessMessage = "Profile Update Successfully.";
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = "Couldn't Find Your Profile.";
                    }
                }
            }

            return View();
        }

        //-------------------------------------------  Check Remote Validation   ---------------------------
        // GET: Check Email Address Alreday registered or Not
        [HttpGet]
        public JsonResult CheckEmailAddress(string EmailAddress)
        {
            using (var db = new MvcTutorialEntities())
            {
                return Json(!db.tblUser.Any(user => user.EmailAddress == EmailAddress), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult EditCheckEmailAddress(string EmailAddress, int UserID)
        {
            using (var db = new MvcTutorialEntities())
            {
                return Json(!db.tblUser.Any(user => user.EmailAddress == EmailAddress && user.UserID != UserID), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Check Mobile No Alreday registered or Not
        [HttpGet]
        public JsonResult CheckMobileNo(string MobileNo)
        {
            using (var db = new MvcTutorialEntities())
            {
                return Json(!db.tblUser.Any(user => user.MobileNo == MobileNo), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult EditCheckMobileNo(string MobileNo, int UserID)
        {
            using (var db = new MvcTutorialEntities())
            {
                return Json(!db.tblUser.Any(user => user.MobileNo == MobileNo && user.UserID != UserID), JsonRequestBehavior.AllowGet);
            }
        }

        //-------------------------------------------  Update Profile  --------------------------------------
        // GET: Get Data From the database to Display in to user Profile
        [HttpGet]
        public ActionResult DeleteAccount()
        {
            if (Session["UserID"] != null)
            {
                using (var database = new MvcTutorialEntities())
                {
                    int UserID = Convert.ToInt32(Session["UserID"]);
                    var databaseUserAccount = database.tblUser.Find(UserID);
                    if (databaseUserAccount != null)
                    {
                        database.tblUser.Remove(databaseUserAccount);
                        Session.RemoveAll();
                        database.SaveChanges();
                        ViewBag.SuccessMessage = "Your Account is Deleted Successfully.";
                        return RedirectToAction("Registration", "User");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
    }
}