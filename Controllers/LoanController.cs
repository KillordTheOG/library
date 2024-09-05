using Library.Data;
using Library.Models;
using Library.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class LoanController : Controller
    {

        private LoanRepository loanRepository;
        private BookRepository bookRepository;

        public LoanController(ApplicationDbContext dbContext)
        {
            this.loanRepository = new LoanRepository(dbContext);
            this.bookRepository = new BookRepository(dbContext);
        }

        // GET: LoanController
        [Authorize(Roles = "User, Admin")]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var allLoans = loanRepository.GetAllLoans();
                return View(allLoans);
            }

            var loans = loanRepository.getLoansByEmail(User.Identity.Name);
            return View("IndexForUser",loans);
        }

        [Authorize(Roles = "User, Admin")]
        public ActionResult Loan(Guid id)
        {
            loanRepository.LoanBook(id, User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }

        // GET: LoanController/Create
        [Authorize(Roles = "User, Admin")]
        public ActionResult Create()
        {
            var unloanedBooks = loanRepository.GetUnloanedBooks(User.Identity.Name);
            return View(unloanedBooks);
        }

        // GET: LoanController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid idLoan)
        {
	        var model = loanRepository.GetLoanByID(idLoan);
	        return View(model);
        }

        // POST: LoanController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid idLoan, IFormCollection collection)
        {
            try
            {
                loanRepository.DeleteLoan(idLoan);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "User")]
        public ActionResult DeleteLoanForUser(Guid idBook)
        {
            BookModel model = bookRepository.GetBookByID(idBook);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult DeleteLoanForUser(Guid idBook, IFormCollection collection)
        {
	        try
	        {
                loanRepository.DeleteLoan(idBook, User.Identity.Name);
		        return RedirectToAction(nameof(Index));
	        }
	        catch
	        {
		        return View();
	        }
        }
	}
}
