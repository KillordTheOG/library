using Library.Data;
using Library.Models.DBObjects;
using Library.Models;

namespace Library.Repository
{
    public class LoanRepository
    {
        private ApplicationDbContext dbContext;

        // Default constructor
        public LoanRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }

        // Constructor with dependency injection
        public LoanRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get all loans
        public List<LoanModel> GetAllLoans()
        {
            List<LoanModel> loanList = new List<LoanModel>();

            foreach (Loan dbLoan in dbContext.Loans)
            {
                loanList.Add(MapDbObjectToModel(dbLoan));
            }

            return loanList;
        }

        // Get a loan by ID
        public LoanModel GetLoanByID(Guid ID)
        {
            return MapDbObjectToModel(dbContext.Loans.FirstOrDefault(x => x.Idloan == ID));
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
        public void DeleteLoan(LoanModel loanModel)
        {
            Loan existingLoan = dbContext.Loans.FirstOrDefault(x => x.Idloan == loanModel.Idloan);

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
