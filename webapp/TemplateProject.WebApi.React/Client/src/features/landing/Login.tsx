import {init} from './state.ts'

function Login() {
    const state = init();

    return (
        <>
            <div>
                <label className="form-label">Username</label>
                <input type="text" className="form-control" value={state.authForm.username}
                       onChange={(e) => state.setUsername(e.target.value)}></input>
            </div>
            <div>
                <label className="form-label">Password</label>
                <input type="password" className="form-control" value={state.authForm.password}
                       onChange={(e) => state.setPassword(e.target.value)}></input>
            </div>
            <div>
                <button type="button" className="btn btn-primary btn-sm" onClick={() => state.authenticate()}>Submit
                </button>
            </div>
        </>
    )
}

export default Login;
