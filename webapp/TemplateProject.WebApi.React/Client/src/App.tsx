import {useState} from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import './features/landing/Login.tsx'
import Login from "./features/landing/Login.tsx";
import Register from "./features/landing/Register.tsx";

type FormType = 'login'|'register';
function App() {
    
    const [count, setCount] = useState(0)
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
                {GetForm(formType)}
                <button className="btn btn-sm btn-secondary" onClick={toggleFormType}>Login/Register</button>
            </div>
            <div className="card">
                <button onClick={() => setCount((count) => count + 1)}>
                    count is {count}
                </button>
                <p>
                    Edit <code>src/App.tsx</code> and save to test HMR
                </p>
            </div>
            <p className="read-the-docs">
                Click on the Vite and React logos to learn more
            </p>
        </>
    )
    
    
    function GetForm(formType: FormType) {
        switch (formType) {
            case 'register':
                return <Register/>
            case 'login':
            default:
                return <Login/>
        }
    }
}

export default App
