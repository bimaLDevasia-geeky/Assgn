// See https://aka.ms/new-console-template for more information

using LibrarySystem.Books;
using LibrarySystem.Members;
using LibrarySystem.Transactions;

Book book = new Book();
Journal journal = new Journal();
Magazine magazine = new Magazine();

Librarian librarian = new Librarian();
Member member = new Member();

BorrowTransaction transaction = new BorrowTransaction();
ReturnTransaction returnTransaction = new ReturnTransaction();

