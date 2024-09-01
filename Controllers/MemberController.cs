using Library.Data;
using Library.Models;
using Library.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Authorize(Roles = "User")]
    public class MemberController : Controller
    {
        private MemberRepository memberRepository;

        public MemberController(ApplicationDbContext dbContext)
        {
            memberRepository = new MemberRepository(dbContext);
        }

        // GET: MemberController
        public ActionResult Index()
        {
            var members = memberRepository.GetAllMembers();
            return View(members);
        }

        // GET: MemberController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = memberRepository.GetMemberByID(id);
            return View(model);
        }

        // GET: MemberController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: MemberController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = memberRepository.GetMemberByID(id);
            return View(model);
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    return RedirectToAction(nameof(Index));
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
        public ActionResult Delete(Guid id)
        {
            var model = memberRepository.GetMemberByID(id);
            return View(model);
        }

        // POST: MemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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