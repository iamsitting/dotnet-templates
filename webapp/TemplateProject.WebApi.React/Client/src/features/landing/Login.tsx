import {init} from './state.ts'

function Login() {
    const state = init();

    return (
        <>
            <div>
                <label className="form-label">Username</label>
                <input type="text" className="form-control" value={state.authFormUsername}
                       onChange={(e) => state.setAuthFormUsername(e.target.value)}></input>
            </div>
            <div>
                <label className="form-label">Password</label>
                <input type="password" className="form-control" value={state.authFormPassword}
                       onChange={(e) => state.setAuthFormPassword(e.target.value)}></input>
            </div>
            <div>
                <button type="button" className="btn btn-primary btn-sm" onClick={() => state.submitLoginForm()}>Submit
                </button>
            </div>
        </>
    )
}

export default Login;
