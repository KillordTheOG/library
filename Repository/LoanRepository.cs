using Library.Data;
using Library.Models.DBObjects;
using Library.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Library.Repository
{
    public class LoanRepository
    {
        private ApplicationDbContext dbContext;
        private MemberRepository memberRepository;
        private BookRepository bookRepository;

        // Default constructor
        public LoanRepository()
        {
            this.dbContext = new ApplicationDbContext();
            this.memberRepository = new MemberRepository();
            this.bookRepository = new BookRepository();
        }

        // Constructor with dependency injection
        public LoanRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.memberRepository = new MemberRepository(dbContext);
            this.bookRepository = new BookRepository(dbContext);
        }

        // Get all loans
        public List<LoanModel> GetAllLoans()
        {
            List<LoanModel> loans = new List<LoanModel>();

            foreach (Loan loan in dbContext.Loans)
            {
                loans.Add(MapDbObjectToModel(loan));
            }

            return loans;
        }

        // Get a loan by ID
        public LoanModel GetLoanByID(Guid ID)
        {
            return MapDbObjectToModel(dbContext.Loans.FirstOrDefault(x => x.Idloan == ID));
        }

        public List<BookModel> getLoansByEmail(string email)
        {
            Guid memberId = memberRepository.GetIdByEmail(email);
            List<BookModel> loanedBooks = new List<BookModel>();

            foreach (Loan loan in dbContext.Loans)
            {
                if (loan.Idmember == memberId)
                {
                    loanedBooks.Add(bookRepository.GetBookByID(loan.Idbook));
                }
            }

            return loanedBooks;
        }

        public List<BookModel> GetUnloanedBooks(string email)
        {
	        List<BookModel> loanedBooks = getLoansByEmail(email);
            List<BookModel> unloanedBooks = new List<BookModel>();

            // Extract the IDs of loaned books to improve lookup performance
            HashSet<Guid> loanedBookIds = new HashSet<Guid>(loanedBooks.Select(b => b.Idbook));

			foreach (Book book in dbContext.Books)
            {
				// Check if the current book's ID is not in the set of loaned book IDs
				if (!loanedBookIds.Contains(book.Idbook))
				{
					// The book is not loaned, add it to the unloanedBooks list
					unloanedBooks.Add(bookRepository.GetBookByID(book.Idbook));
				}
			}

            return unloanedBooks;
        }

		public void LoanBook(Guid id, string memberEmail)
        {
            var memberId = memberRepository.GetIdByEmail(memberEmail);

            LoanModel loanModel = new LoanModel();
            loanModel.Idloan = Guid.NewGuid();
            loanModel.Idbook = id;
            loanModel.Idmember = memberId;

            dbContext.Loans.Add(MapModelToDbObject(loanModel));
            dbContext.SaveChanges();
        }

        // Insert a new loan
        public void InsertLoan(LoanModel loanModel)
        {
            loanModel.Idloan = Guid.NewGuid();

            dbContext.Loans.Add(MapModelToDbObject(loanModel));
            dbContext.SaveChanges();
        }

        // Update an existing loan
        public void UpdateLoan(LoanModel loanModel)
        {
            Loan existingLoan = dbContext.Loans.FirstOrDefault(x => x.Idloan == loanModel.Idloan);

            if (existingLoan != null)
            {
                existingLoan.Idloan = loanModel.Idloan;
                existingLoan.Idmember = loanModel.Idmember;
                existingLoan.Idbook = loanModel.Idbook;
                dbContext.SaveChanges();
            }
        }

        // Delete a loan
        public void DeleteLoan(Guid id)
        {
            Loan existingLoan = dbContext.Loans.FirstOrDefault(x => x.Idloan == id);

            if (existingLoan != null)
            {
                dbContext.Loans.Remove(existingLoan);
                dbContext.SaveChanges();
            }
        }

        public void DeleteLoan(Guid idBook, string email)
        {
            Guid memberId = memberRepository.GetIdByEmail(email);
            Loan existingLoan = dbContext.Loans.FirstOrDefault(x => x.Idbook == idBook && x.Idmember == memberId);

            if (existingLoan != null)
            {
                dbContext.Loans.Remove(existingLoan);
                dbContext.SaveChanges();
            }
        }

        // Map database object to model
        private LoanModel MapDbObjectToModel(Loan dbLoan)
        {
            LoanModel loanModel = new LoanModel();

            if (dbLoan != null)
            {
                loanModel.Idloan = dbLoan.Idloan;
                loanModel.Idmember = dbLoan.Idmember;
                loanModel.Idbook = dbLoan.Idbook;
                loanModel.Member = memberRepository.GetMemberByID(loanModel.Idmember);
                loanModel.Book = bookRepository.GetBookByID(loanModel.Idbook);
            }

            return loanModel;
        }

        // Map model to database object
        private Loan MapModelToDbObject(LoanModel loanModel)
        {
            Loan loan = new Loan();

            if (loanModel != null)
            {
                loan.Idloan = loanModel.Idloan;
                loan.Idmember = loanModel.Idmember;
                loan.Idbook = loanModel.Idbook;
            }

            return loan;
        }
	}
}