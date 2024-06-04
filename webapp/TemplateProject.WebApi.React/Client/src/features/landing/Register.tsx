import {useLandingStore} from './store.ts'

function Login() {
    const store = useLandingStore();

    return (
        <>
            <div>
                <label className="form-label">Username</label>
                <input type="text" className="form-control" value={store.state.regForm.username}
                       onChange={(e) => store.setRegFormUsername(e.target.value)}></input>
            </div>
            <div>
                <label className="form-label">Password</label>
                <input type="password" className="form-control" value={store.state.regForm.password}
                       onChange={(e) => store.setRegFormPassword(e.target.value)}></input>
            </div>
            <div>
                <label className="form-label">Confirm Password</label>
                <input type="password" className="form-control" value={store.state.regForm.confirmPassword}
                       onChange={(e) => store.setRegFormConfirmPassword(e.target.value)}></input>
            </div>
            <div>
                <button type="button" className="btn btn-primary btn-sm" onClick={() => store.submitRegisterForm()}>Submit
                </button>
            </div>
        </>
    )
}

export default Login;
