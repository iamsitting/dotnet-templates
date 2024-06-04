import {AuthForm, TokenResponse} from "./models";
import Cookies from "universal-cookie";

export async function sendCredentials(payload: AuthForm){
    const res = await fetch("/api/Auth/login", {
        method: "post",
        body: JSON.stringify({...payload})
    });
    if(res.ok) {
        const data: TokenResponse = await res.json()
        const cookies = new Cookies();
        cookies.set(data.token, '', );
    }
}