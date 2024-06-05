import React, {useState} from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import './features/landing/Login.tsx'
import Login from "./features/landing/Login.tsx";
import Register from "./features/landing/Register.tsx";
import BookList from "./features/books/BookList.tsx";

type FormType = 'login'|'register';
function App() {
    
    const [formType, setFormType] = useState<FormType>('login')
    function toggleFormType(){
        if (formType === 'login') {
            setFormType('register');
        }
        else {
            setFormType('login')
        }
    }
    return (
        <>
            <div>
                <a href="https://vitejs.dev" target="_blank">
                    <img src={viteLogo} className="logo" alt="Vite logo"/>
                </a>
                <a href="https://react.dev" target="_blank">
                    <img src={reactLogo} className="logo react" alt="React logo"/>
                </a>
            </div>
            <h1>Vite + React</h1>
            <div className="card">
                <LandingFrom formType={formType}/>
                <button className="btn btn-sm btn-secondary" onClick={toggleFormType}>Login/Register</button>
            </div>
            <div className="card">
                <BookList/>
            </div>
            <p className="read-the-docs">
                Click on the Vite and React logos to learn more
            </p>
        </>
    )
    
}

const LandingFrom: React.FC<{formType: FormType}> = ({formType}) => {
    switch (formType) {
        case 'register':
            return <Register/>
        case 'login':
        default:
            return <Login/>
    }
}

export default App
