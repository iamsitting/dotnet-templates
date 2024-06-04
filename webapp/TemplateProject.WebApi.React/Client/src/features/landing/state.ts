import {useState} from "react";
import {DefaultLandingState, LandingState} from "./models";
import {registerUser, sendCredentials} from "./controllers.ts";

export function init(){
    const [state, setState] = useState<LandingState>({
        ...DefaultLandingState
    });
    
    
    function setAuthFormUsername(username: string) {
        const newState = {...state};
        newState.authForm.username = username;
        setState(newState);
    }

    function setAuthFormPassword(password: string) {
        const newState = {...state};
        newState.authForm.password = password;
        setState(newState);
    }

    async function submitLoginForm(){
        const payload = {...state.authForm};
        setState({...DefaultLandingState});
        await sendCredentials(payload)
    }

    function setRegFormUsername(username: string) {
        const newState = {...state};
        newState.regForm.username = username;
        setState(newState);
    }

    function setRegFormPassword(password: string) {
        const newState = {...state};
        newState.regForm.password = password;
        setState(newState);
    }

    function setRegFormConfirmPassword(password: string) {
        const newState = {...state};
        newState.regForm.confirmPassword = password;
        setState(newState);
    }
    
    async function submitRegisterForm() {
        const payload = {...state.regForm};
        setState({...DefaultLandingState});
        await registerUser(payload);
    }

    return  {
        authFormUsername: state.authForm.username,
        authFormPassword: state.authForm.password,
        setAuthFormUsername,
        setAuthFormPassword,
        
        regFormUsername: state.regForm.username,
        regFormPassword: state.regForm.password,
        RegFormConfirmPassword: state.regForm.confirmPassword,
        setRegFormUsername,
        setRegFormPassword,
        setRegFormConfirmPassword,
        
        submitLoginForm,
        submitRegisterForm
    }
}
