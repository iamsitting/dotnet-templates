import {useLandingStore} from './store.ts'

function Login() {
    const store = useLandingStore();

    return (
        <>
            <div>
                <label className="form-label">Username</label>
                <input type="text" className="form-control" value={store.state.authForm.username}
                       onChange={(e) => store.setAuthFormUsername(e.target.value)}></input>
            </div>
            <div>
                <label className="form-label">Password</label>
                <input type="password" className="form-control" value={store.state.authForm.password}
                       onChange={(e) => store.setAuthFormPassword(e.target.value)}></input>
            </div>
            <div>
                <button type="button" className="btn btn-primary btn-sm" onClick={() => store.submitLoginForm()}>Submit
                </button>
            </div>
        </>
    )
}

export default Login;
