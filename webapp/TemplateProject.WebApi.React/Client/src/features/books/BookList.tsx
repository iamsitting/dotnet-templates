import {useState} from "react";
import {getBooks} from "./controllers.ts";


function BookList() {
    const [books, setBooks] = useState<Book[]>([]);
    
    async function loadBooks(){
        const data = await getBooks();
        setBooks(data);
    }
    
    return (
        <>
            <button className="btn btn-primary" onClick={() => loadBooks()}>Get Books</button>
            <table className="table table-striped">
                <thead>
                <tr>
                    <th>ID</th>
                    <th>Title</th>
                    <th>Author</th>
                    <th>Year</th>
                </tr>
                </thead>
                <tbody>
                {books.map(book => (
                    <tr key={book.id}>
                        <td>{book.id}</td>
                        <td>{book.title}</td>
                        <td>{book.author}</td>
                        <td>{book.year}</td>
                    </tr>
                ))}
                </tbody>
            </table>
        </>
    )
}

export default BookList;