import {useState} from "react";
import {addBook, getBooks} from "./controllers.ts";
import {Link} from "react-router-dom";


function BookList() {
    const [books, setBooks] = useState<Book[]>([]);
    const [title, setTitle] = useState('');
    const [author, setAuthor] = useState('');
    const [year, setYear] = useState(1901);
    const [publisher, setPublisher] = useState('');
    
    async function loadBooks(){
        const data = await getBooks();
        setBooks(data);
    }
    
    async function addNewBook() {
        const payload: Book = {
            title,
            author,
            year,
            id: null,
            publisher,
        }
        const data = await addBook(payload)
        if(data) {
            setBooks([...books, data]);
            setTitle('');
            setAuthor('');
            setYear(1901);
            setPublisher('');
        }
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
            <div>
                <label className="form-label">Title</label>
                <input type="text" value={title} onChange={(e) => setTitle(e.target.value)} className="form-control"/>
                <label className="form-label">Author</label>
                <input type="text" value={author} onChange={(e) => setAuthor(e.target.value)} className="form-control"/>
                <label className="form-label">Year</label>
                <input type="number" value={year} onChange={(e) => setYear(parseInt(e.target.value))} className="form-control"/>
                <button className="btn btn-sm" onClick={() => addNewBook()}>Add</button>
                <label className="form-label">Publisher</label>
                <input type="text" value={publisher} className="form-control" onChange={(e) => setPublisher(e.target.value)}/>
            </div>
            <Link to={"/"}>Go Back</Link>
        </>
    )
}

export default BookList;