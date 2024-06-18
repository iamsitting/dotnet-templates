export type Book = {
    title: string;
    author: string;
    id: string|null;
    year: number;
}

export type BooksState = {
    books: Book[];
    addBookForm: Book;
}

export const BookState: BooksState = {
    books: [],
    addBookForm: {
        title: '',
        author: '',
        id: null,
        year: 1901
    }
}