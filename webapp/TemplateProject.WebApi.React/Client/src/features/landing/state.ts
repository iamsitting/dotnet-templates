import {useState} from "react";
import {AuthForm} from "./models";
import {sendCredentials} from "./controllers.ts";

export function init(){
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

    async function authenticate(){
        const payload = {...authForm};
        setAuthForm({username: '', password: ''});
        await sendCredentials(payload)
    }

    return  {
        setUsername,
        setPassword,
        authForm,
        authenticate
    }
}
