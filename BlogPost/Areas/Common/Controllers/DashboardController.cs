using BlogPost.DataAccess.Repository.IRepository;
using BlogPost.Models;
using BlogPost.Models.ViewModels;
using BlogPost.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlogPost.Areas.Common
{
    [Area("Common")]
    public class DashboardController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public DashboardController(IWebHostEnvironment env,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _env = env;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            try
            {
                var id = _userManager.GetUserId(User); // Get user id:
                var StoryList = _unitOfWork.Story.GetAll(includeProperties: "ApplicationUser").Where(s => s.IsPublished == true);
                List<DashBoardViewModel> dashList = new List<DashBoardViewModel>();
                if (StoryList != null && StoryList.Count() > 0)
                {
                    dashList.AddRange(StoryList.Select(i => new DashBoardViewModel()
                    {
                        StoryId = i.Id,
                        Title = i.Title,
                        StoryFirstPara = CommonUtility.GetFirstParaGrapthInHTML(i.StoryDetail, _env),
                        StoryFirstImage = CommonUtility.GetFirstImagesInHTML(i.StoryDetail, _env),
                        CreatedBy = i.ApplicationUser.FirstName + " " + i.ApplicationUser.LastName,
                        CreatedOn = i.CreatedDate.ToString("MMMM dd, yyyy"),
                        UserImage = i.ApplicationUser.UserImage
                    }));
                }
                return View(dashList);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View("Error");
            }
        }

        public IActionResult PublicFullStory(int Id)
        {
            return View();
        }

        
    }
}
