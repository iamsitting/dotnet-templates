import {Book, BooksState, BookState} from "./models";
import {useEffect, useState} from "react";
import {addBook, getBooks} from "./controllers";

let globalState: BooksState = {...BookState};
let listeners: Function[] = [];

const setGlobalState = (newState: BooksState) => {
    globalState = newState;
    listeners.forEach(listener => listener(globalState));
};

export function useBooksState(){
    const [state, setLocalState] = useState(globalState);

    useEffect(() => {
        const listener = (newState: BooksState) => setLocalState(newState);
        listeners.push(listener);
        return () => {
            listeners = listeners.filter(l => l !== listener);
        };
    }, []);
    
    const loadBooks = async () =>{
        const result = await getBooks();
        setBooks(result);
    }
    
    const addNewBook = async () => {
        const payload = globalState.addBookForm;
        const res = await addBook(payload)
        if(res) {
            setBooks([...globalState.books, res]);
            setTitle('');
            setAuthor('');
            setYear(1901)
        }
        return res;
    }
    
    const setBooks = (books: Book[]) => {
        setGlobalState({
            ...globalState,
            books: [...books]
        });
    }
    
    const setTitle = (title: string) => {
        setGlobalState({
            ...globalState,
            addBookForm: {
                ...globalState.addBookForm,
                title: title
            },
        })
    }
    
    const setAuthor = (author: string) => {
        setGlobalState({
            ...globalState,
            addBookForm: {
                ...globalState.addBookForm,
                author: author
            }
        })
    }
    
    const setYear = (year: number) => {
        setGlobalState({
            ...globalState,
            addBookForm: {
                ...globalState.addBookForm,
                year: year
            }
        })
    }
    
    
    
    return {
        state,
        loadBooks,
        setTitle,
        setAuthor,
        setYear,
        addNewBook,
    }
}