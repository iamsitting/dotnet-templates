import {useState} from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import Cookies from 'universal-cookie'
type AuthForm = {
    username: string;
    password: string;
}

type TokenResponse = {
    token: string;
}
function App() {
    
    const [count, setCount] = useState(0)

    const [authForm, setAuthForm] = useState<AuthForm>({
        username: "",
        password: ""
    });
    function setUsername(username: string) {
        setAuthForm({...authForm, username})
    }
    
    function setPassword(password: string) {
        setAuthForm({...authForm, password})
    }
    async function sendCredentials(){
        const res = await fetch("/api/Auth/login", {
            method: "post",
            body: JSON.stringify({...authForm})
        });
        if(res.ok) {
            const data: TokenResponse = await res.json()
            const cookies = new Cookies();
            cookies.set(data.token, '', );
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
                <div>
                    <label className="form-label">Username</label>
                    <input type="text" className="form-control" value={authForm.username} onChange={(e) => setUsername(e.target.value)}></input>
                </div>
                <div>
                    <label className="form-label">Password</label>
                    <input type="password" className="form-control" value={authForm.password} onChange={(e) => setPassword(e.target.value)}></input>
                </div>
                <div>
                    <button type="button" className="btn btn-primary btn-sm" onClick={() => sendCredentials()}>Submit</button>
                </div>
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
}

export default App
