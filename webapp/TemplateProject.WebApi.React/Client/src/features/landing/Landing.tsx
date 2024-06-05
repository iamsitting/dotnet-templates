import {useLandingStore} from "./store";
import React from "react";
import Register from "./Register";
import Login from "./Login";
import {FormType} from "./models";

function Landing(){
    const store = useLandingStore();
    return (
        <>
            <div className="card">
                <LandingForm formType={store.state.formType}/>
                <button className="btn btn-sm btn-secondary" onClick={store.toggleFormType}>Login/Register</button>
            </div>
        </>
    )
}

export default Landing;

const LandingForm: React.FC<{formType: FormType}> = ({formType}) => {
    switch (formType) {
        case 'register':
            return <Register/>
        case 'login':
        default:
            return <Login/>
    }
}
