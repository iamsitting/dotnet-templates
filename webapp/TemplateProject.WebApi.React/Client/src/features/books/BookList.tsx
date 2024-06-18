import {Link} from "react-router-dom";
import {useBooksState} from "./store.ts";


function BookList() {
    const store = useBooksState();
    
    return (
        <>
            <button className="btn btn-primary" onClick={() => store.loadBooks()}>Get Books</button>
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
                {store.state.books.map(book => (
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
                <input type="text" value={store.state.addBookForm.title} onChange={(e) => store.setTitle(e.target.value)} className="form-control"/>
                <label className="form-label">Author</label>
                <input type="text" value={store.state.addBookForm.author} onChange={(e) => store.setAuthor(e.target.value)} className="form-control"/>
                <label className="form-label">Year</label>
                <input type="number" value={store.state.addBookForm.year} onChange={(e) => store.setYear(parseInt(e.target.value))} className="form-control"/>
                <button className="btn btn-sm" onClick={() => store.addNewBook()}>Add</button>
            </div>
            <Link to={"/"}>Go Back</Link>
        </>
    )
}

export default BookList;