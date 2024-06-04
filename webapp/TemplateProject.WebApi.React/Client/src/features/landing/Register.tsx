import {init} from './state.ts'

function Login() {
    const state = init();

    return (
        <>
            <div>
                <label className="form-label">Username</label>
                <input type="text" className="form-control" value={state.regFormUsername}
                       onChange={(e) => state.setRegFormUsername(e.target.value)}></input>
            </div>
            <div>
                <label className="form-label">Password</label>
                <input type="password" className="form-control" value={state.regFormPassword}
                       onChange={(e) => state.setRegFormPassword(e.target.value)}></input>
            </div>
            <div>
                <label className="form-label">Confirm Password</label>
                <input type="password" className="form-control" value={state.RegFormConfirmPassword}
                       onChange={(e) => state.setRegFormConfirmPassword(e.target.value)}></input>
            </div>
            <div>
                <button type="button" className="btn btn-primary btn-sm" onClick={() => state.submitRegisterForm()}>Submit
                </button>
            </div>
        </>
    )
}

export default Login;
