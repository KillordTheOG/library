using Library.Data;
using Library.Models.DBObjects;
using Library.Models;

namespace Library.Repository
{
    public class BookRepository
    {
        private ApplicationDbContext dbContext;

        // Default constructor
        public BookRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }

        // Constructor with dependency injection
        public BookRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get all books
        public List<BookModel> GetAllBooks()
        {
            List<BookModel> bookList = new List<BookModel>();

            foreach (Book dbBook in dbContext.Books)
            {
                bookList.Add(MapDbObjectToModel(dbBook));
            }

            return bookList;
        }

        // Get a book by ID
        public BookModel GetBookByID(Guid ID)
        {
            return MapDbObjectToModel(dbContext.Books.FirstOrDefault(x => x.Idbook == ID));
        }

        // Insert a new book
        public void InsertBook(BookModel bookModel)
        {
            bookModel.Idbook = Guid.NewGuid();

            dbContext.Books.Add(MapModelToDbObject(bookModel));
            dbContext.SaveChanges();
        }

        // Update an existing book
        public void UpdateBook(BookModel bookModel)
        {
            Book existingBook = dbContext.Books.FirstOrDefault(x => x.Idbook == bookModel.Idbook);

            if (existingBook != null)
            {
                existingBook.Title = bookModel.Title;
                existingBook.Author = bookModel.Author;
                existingBook.Description = bookModel.Description;
                dbContext.SaveChanges();
            }
        }



        // Delete a book
        public void DeleteBook(Guid id)
        {
            Book existingBook = dbContext.Books.FirstOrDefault(x => x.Idbook == id);

            if (existingBook != null)
            {
                dbContext.Books.Remove(existingBook);
                dbContext.SaveChanges();
            }
        }

        // Map database object to model
        private BookModel MapDbObjectToModel(Book dbBook)
        {
            BookModel bookModel = new BookModel();

            if (dbBook != null)
            {
                bookModel.Idbook = dbBook.Idbook;
                bookModel.Title = dbBook.Title;
                bookModel.Author = dbBook.Author;
                bookModel.Description = dbBook.Description;
            }

            return bookModel;
        }

        // Map model to database object
        private Book MapModelToDbObject(BookModel bookModel)
        {
            Book book = new Book();

            if (bookModel != null)
            {
                book.Idbook = bookModel.Idbook;
                book.Title = bookModel.Title;
                book.Author = bookModel.Author;
                book.Description = bookModel.Description;
            }

            return book;
        }
	}
}
