using Library.Data;
using Library.Models;
using Library.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class MemberController : Controller
    {
        private MemberRepository memberRepository;

        public MemberController(ApplicationDbContext dbContext)
        {
            memberRepository = new MemberRepository(dbContext);
        }

		// GET: MemberController
		[Authorize(Roles = "Admin")]
		public ActionResult Index()
        {
            var members = memberRepository.GetAllMembers();
            return View(members);
        }

		// GET: MemberController/Details/5
		[Authorize(Roles = "User, Admin")]
		public ActionResult Details(Guid id)
        {
            var model = memberRepository.GetMemberByID(id);
            return View(model);
        }

        [Authorize(Roles = "User, Admin")]
		public ActionResult MemberData()
        {
	        var memberModel = memberRepository.GetMemberByEmail(User.Identity.Name);
	        return View("Details", memberModel);
	        // return RedirectToAction(nameof(Details), memberModel.Idmember);
		}

		// GET: MemberController/Create
		[Authorize(Roles = "User, Admin")]
		public ActionResult Create()
        {
            var model = new MemberModel();
            if (User.Identity.IsAuthenticated)
            {
                model.Email = User.Identity.Name;
            }
            return View(model);
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Admin")]
		public ActionResult Create(IFormCollection collection)
        {
            try
            {
                MemberModel model = new MemberModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();

                if (task.Result)
                {
                    memberRepository.InsertMember(model);
                }

                return RedirectToAction(nameof(Details));
            }
            catch
            {
                return View();
            }
        }

		// GET: MemberController/Edit/5
		[Authorize(Roles = "User, Admin")]
		public ActionResult Edit(Guid id)
        {
            var model = memberRepository.GetMemberByID(id);
            return View(model);
        }
        
        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Admin")]
		public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new MemberModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();

                if (task.Result)
                {
                    memberRepository.UpdateMember(model);
                    if (User.IsInRole("Admin"))
                    {
	                    return RedirectToAction(nameof(Index));
					}
                    else
                    {
	                    return RedirectToAction("MemberData");
                    }
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

		// GET: MemberController/Delete/5
		[Authorize(Roles = "Admin")]
		public ActionResult Delete(Guid id)
        {
            var model = memberRepository.GetMemberByID(id);
            return View(model);
        }

        // POST: MemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
		public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                memberRepository.DeleteMember(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}